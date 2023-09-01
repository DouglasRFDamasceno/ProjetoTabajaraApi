using System.ComponentModel.DataAnnotations;
using System.Reflection;

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

    [Required(ErrorMessage = "A data de início é obrigatória")]
    public DateTime StartDate { get; set; }

    [MaxLength(20)]
    public string RG { get; set; }
    
    [MaxLength(20)]
    public string CPF { get; set; }

    [Required(ErrorMessage = "O telefone para contato é obrigatório")]
    [MaxLength(20)]
    public string CellPhone { get; set; }
    
    [MaxLength(255)]
    public string SpecialNeeds { get; set; }
    
    [MaxLength(30)]
    public string Email { get; set; }
    
    [MaxLength(30)]
    public string Gender { get; set; }
    
    [Required(ErrorMessage = "A escolaridade é obrigatória")]
    [MaxLength(50)]
    public string EducationDegree { get; set; }
    
    [MaxLength(255)]
    public string School { get; set; }
    
    [MaxLength(30)]
    public string SchoolShift { get; set; }

    [MaxLength(15)]
    public string BloodType { get; set; }

    [MaxLength(15)]
    public string Height { get; set; }

    [MaxLength(15)]
    public string Weight { get; set; }
}
