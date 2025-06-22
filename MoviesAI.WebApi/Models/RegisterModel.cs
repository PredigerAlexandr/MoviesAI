using System.ComponentModel.DataAnnotations;

namespace MoviesAI.WebApi.Models;

public class RegisterModel
{
    [Required(ErrorMessage ="Не указан Email")]
    public string Email { get; set; }
         
    [Required(ErrorMessage = "Не указан пароль")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
         
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Пароль введен неверно")]
    public string ConfirmPassword { get; set; }
    
    [Required(ErrorMessage = "Имя пользователя обязательно.")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Фамилия пользователя обязательно.")]
    public string Surname { get; set; }
}