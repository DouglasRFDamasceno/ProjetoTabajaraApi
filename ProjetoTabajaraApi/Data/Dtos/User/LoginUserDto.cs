using System.ComponentModel.DataAnnotations;

namespace ProjetoTabajaraApi.Data.Dtos.User;

public class LoginUserDto
{
    [Required(ErrorMessage = "O usuário é obrigatório")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória")]
    public string Password { get; set; }
}
