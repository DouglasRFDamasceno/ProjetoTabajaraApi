using Microsoft.AspNetCore.Mvc;
using ProjetoTabajaraApi.Data.Dtos.Student;
using ProjetoTabajaraApi.Services;

namespace ProjetoTabajaraApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class StudentController : ControllerBase
{
    public StudentService _studentService;

    public StudentController(StudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpPost]
    public IActionResult CreateStudent(CreateStudentDto studentDto)
    {
        var result = _studentService.CreateStudent(studentDto);

        return Ok(result);
    }

    [HttpPut]
    public IActionResult UpdateStudentPatch(int id, [FromBody] UpdateStudentDto studentDto)
    {
        var student = _studentService.UpdateStudentPatch(id, studentDto);

        if (student == null) return NotFound();

        return Ok($"Usuário {student!.Name} criado com sucesso.");
    }
}
