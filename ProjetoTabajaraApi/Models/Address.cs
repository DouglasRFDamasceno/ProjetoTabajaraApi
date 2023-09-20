using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoTabajaraApi.Models;

public class Address
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "O cep é obrigatório")]
    [MaxLength(255)]
    public string ZipCode{ get; set; }

    [Required(ErrorMessage = "A rua é obrigatória")]
    [MaxLength(255)]
    public string Street { get; set; }

    [MaxLength(255)]
    public string Complement { get; set; }

    [Required(ErrorMessage = "O numero é obrigatório")]
    [MaxLength(255)]
    public string Number { get; set; }

    [Required(ErrorMessage = "O bairro é obrigatório")]
    [MaxLength(255)]
    public string Neighborhood { get; set; }
    
    [Required(ErrorMessage = "O cidade é obrigatório")]
    [MaxLength(255)]
    public string City { get; set; }
    
    [Required(ErrorMessage = "O estado é obrigatório")]
    [MaxLength(255)]
    public string State { get; set; }
    
    [Required(ErrorMessage = "O país é obrigatório")]
    [MaxLength(255)]
    public string Country { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime Create_time { get; set; }

    public DateTime Update_time { get; set; }

    public virtual Student Student { get; set; }
}