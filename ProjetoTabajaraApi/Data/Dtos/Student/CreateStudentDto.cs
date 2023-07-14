using System.ComponentModel.DataAnnotations;

namespace ProjetoTabajaraApi.Data.Dtos.Student;

public class CreateStudentDto
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    [MaxLength(255)]
    public string Name { get; set; }

    [Required(ErrorMessage = "O nome do pai é obrigatório")]
    [MaxLength(255)]
    public string FatherName { get; set; }

    [Required(ErrorMessage = "O nome da mãe é obrigatório")]
    [MaxLength(255)]
    public string MotherName { get; set; }

    [Required(ErrorMessage = "O id do endereço é obrigatório")]
    public int AddressId { get; set; }

    [Required(ErrorMessage = "A data de nascimento é obrigatória")]
    public DateTime DateOfBirth { get; set; }

    [MaxLength(255)]
    public string Observation { get; set; }
}
