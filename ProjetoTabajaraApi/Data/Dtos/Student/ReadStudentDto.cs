using ProjetoTabajaraApi.Data.Dtos.Address;
using System.ComponentModel.DataAnnotations;

namespace ProjetoTabajaraApi.Data.Dtos.Student;

public class ReadStudentDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string FatherName { get; set; }
    public string MotherName { get; set; }
    public int AddressId { get; set; }
    public ReadAddressDto Address { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Observation { get; set; }
    public string RG { get; set; }
    public string CPF { get; set; }
    public string CellPhone { get; set; }
    public string SpecialNeeds { get; set; }
    public string Email { get; set; }
    public string Gender { get; set; }
    public string EducationDegree { get; set; }
    public string School { get; set; }
    public string SchoolShift { get; set; }
    public string BloodType { get; set; }
    public string Height { get; set; }
    public string Weight { get; set; }
    public DateTime StartDate { get; set; }
}