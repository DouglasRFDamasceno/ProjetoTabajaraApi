using System.ComponentModel.DataAnnotations;

namespace ProjetoTabajaraApi.Data.Dtos.User
{
    public class UpdateUserDto
    {
        [Required(ErrorMessage = "O nome do usuário é obrigatório.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        public DateTime DateOfBirth { get; set; }
    }
}
