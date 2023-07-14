using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public IActionResult CreateStudent(CreateStudentDto studentDto)
    {
        var result = _studentService.CreateStudent(studentDto);

        return Ok(result);
    }

    [HttpPut]
    [Authorize]
    public IActionResult UpdateStudent(int id, [FromBody] UpdateStudentDto studentDto)
    {
        var student = _studentService.UpdateStudent(id, studentDto);

        if (student == null) return NotFound();

        return Ok($"Usuário {student!.Name} criado com sucesso.");
    }

    [HttpGet("{id}")]
    [Authorize]
    public IActionResult GetStudent(int id)
    {
        var studentDto = _studentService.GetStudent(id);

        return Ok(studentDto);
    }
}
