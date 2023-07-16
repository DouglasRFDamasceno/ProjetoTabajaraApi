using System.ComponentModel.DataAnnotations;

namespace ProjetoTabajaraApi.Data.Dtos.Address;

public class UpdateAddressDto
{
<<<<<<< HEAD
=======
    [Key] public int Id { get; set; }

>>>>>>> develop
    [Required(ErrorMessage = "O cep � obrigat�rio")]
    [MaxLength(255)]
    public string ZipCode { get; set; }

    [Required(ErrorMessage = "A rua � obrigat�ria")]
    [MaxLength(255)]
    public string Street { get; set; }

    [Required(ErrorMessage = "O numero � obrigat�rio")]
    [MaxLength(255)]
    public string Number { get; set; }

    [Required(ErrorMessage = "O bairro � obrigat�rio")]
    [MaxLength(255)]
    public string Neighborhood { get; set; }

    [Required(ErrorMessage = "O cidade � obrigat�rio")]
    [MaxLength(255)]
    public string City { get; set; }

    [Required(ErrorMessage = "O estado � obrigat�rio")]
    [MaxLength(255)]
    public string State { get; set; }

    [Required(ErrorMessage = "O pa�s � obrigat�rio")]
    [MaxLength(255)]
    public string Country { get; set; }
}