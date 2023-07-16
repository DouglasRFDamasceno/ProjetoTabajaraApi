using System.ComponentModel.DataAnnotations;

namespace ProjetoTabajaraApi.Models;

public class Student
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [MaxLength(255)]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "O nome do pai é obrigatório")]
    [MaxLength(255)]
    public string FatherName{ get; set; }

    [Required(ErrorMessage = "O nome da mãe é obrigatório")]
    [MaxLength(255)]
    public string MotherName { get; set; }

    [Required(ErrorMessage = "A data de nascimento é obrigatória")]
<<<<<<< HEAD
    [MaxLength(255)]
=======
>>>>>>> develop
    public DateTime DateOfBirth { get; set; }
    
    [MaxLength(255)]
    public string Observation { get; set; }

    [Required(ErrorMessage = "O id do endereço é obrigatório")]
    public int AddressId { get; set; }

    public virtual Address Address { get; set; }
}
