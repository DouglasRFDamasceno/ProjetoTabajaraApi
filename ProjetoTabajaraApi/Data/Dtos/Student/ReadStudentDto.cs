using ProjetoTabajaraApi.Models;

namespace ProjetoTabajaraApi.Data.Dtos.Student;

public class ReadStudentDto
{
    public string Name { get; set; }

    public string FatherName { get; set; }

    public string MotherName { get; set; }

    public int AddressId { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string Observation { get; set; }
}