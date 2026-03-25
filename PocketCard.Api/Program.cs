using System.Data;
using System.Text;
using Dapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using PocketCard.Infrastructure;
using PocketCard.Infrastructure.Repositories;
using PocketCard.UseCases;
using ProjectManagementAPI.src.Infrastructure;
using ProjectManagementAPI.src.UseCases;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

var databasePath = builder.Configuration.GetSection("Database")["Path"] ?? "/app/data/pocketcard.db";
var connectionString = $"Data Source={databasePath}";
builder.Services.AddScoped<IDbConnection>(options => new SqliteConnection(connectionString));
SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        // This ensures extensions are preserved during the WriteAsync process
        context.ProblemDetails.Extensions["traceId"] = context.HttpContext.TraceIdentifier;
    };
});
builder.Services.AddExceptionHandler<GlobalValidationExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalDbUpdateExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICardReadRepository, CardReadRepository>();
builder.Services.AddScoped<IUserReadRepository, UserReadRepository>();
builder.Services.AddScoped<IAccountReadRepository, AccountReadRepository>();
builder.Services.UseSqlite(connectionString);
builder.Services.AddScoped<IPasswordHasher<string>, PasswordHasher<string>>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
jwtOptions => jwtOptions.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = builder.Configuration["Jwt:Issuer"],
    ValidAudience = builder.Configuration["Jwt:Issuer"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? string.Empty))
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{

    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "\"Bearer {token}\"",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddSingleton<ITokenProvider, TokenProvider>();
builder.Services.AddAuthorization();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());//scan entire workspace not just /Api(typeof(Program).Assembly - .Api );
    config.AddOpenBehavior(typeof(VaidationBehaviour<,>));
});
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseExceptionHandler();
app.UseSwagger();
app.UseSwaggerUI();

app.UseUserEndpoints();
app.UseAccountEndpoints();
app.UseCardEndpoints();

using (var scope = app.Services.CreateAsyncScope())
{
    PocketcardDbContext dbcontext = scope.ServiceProvider.GetRequiredService<PocketcardDbContext>();
    await dbcontext.Database.EnsureCreatedAsync();

    var users = DataSeeder.GenerateUsers();
    var accounts = DataSeeder.GenerateAccounts();
    var cards = DataSeeder.GenerateCards();

    if (!dbcontext.Users.Any())
    {
    await dbcontext.Users.AddRangeAsync(users);
    await dbcontext.Accounts.AddRangeAsync(accounts);
    await dbcontext.Cards.AddRangeAsync(cards);
    await dbcontext.SaveChangesAsync();
    }
}

app.MapGet("/health", () => Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow }));

app.Run();
