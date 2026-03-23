
using System.ComponentModel.DataAnnotations;

public record CreateUserRequestDTO
{
    [Required(ErrorMessage = "Name is required")]
    public string name { get; set; } = null!;
    [Required(ErrorMessage = "Password is required")]
    public string password { get; set; } = null!;
    [Required(ErrorMessage = "Email is required")]
    public string email { get; set; } = null!;
}

public record LoginUserRequestDTO
{
    [Required(ErrorMessage = "Name is required")]
    public string name { get; set; } = null!;
    [Required(ErrorMessage = "Password is required")]
    public string password { get; set; } = null!;
}