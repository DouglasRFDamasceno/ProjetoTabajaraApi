using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProjetoTabajaraApi.Data.Dtos.Student;
using ProjetoTabajaraApi.Data.Dtos.User;
using ProjetoTabajaraApi.Services;

namespace ProjetoTabajaraApi.Controllers;

[ApiController]
[Route("api/[Controller]")]
[EnableCors("MyPolicy")]
public class StudentController : ControllerBase
{
    public StudentService _studentService;

    public StudentController(StudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpPost("create")]
    [Authorize]
    public CreatedAtActionResult CreateStudent(CreateStudentDto studentDto)
    {
        var student = _studentService.CreateStudent(studentDto);
        return CreatedAtAction(nameof(GetStudent), new { id = student?.Id }, student);
    }

    [HttpPut("{id}")]
    [Authorize]
    public IActionResult UpdateStudent(int id, [FromBody] UpdateStudentDto studentDto)
    {
        var student = _studentService.UpdateStudent(id, studentDto);

        if (student == null) return NotFound();

        return NoContent();
    }

    [HttpGet("{id}")]
    [Authorize]
    public IActionResult GetStudent(int id)
    {
        var studentDto = _studentService.GetStudent(id);

        return Ok(studentDto);
    }

    [HttpGet]
    [Authorize]
    public IActionResult GetStudents(int skip = 0, int take = 50)
    {
        var studentsDto = _studentService.GetStudents(skip, take);

        return Ok(studentsDto);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public IActionResult DeleteStudent(int id)
    {
        var deletedStudent = _studentService.DeleteStudent(id);

        if (!deletedStudent) return NotFound();

        return NoContent();
    }

    [HttpPatch("{id}")]
    [Authorize]
    public IActionResult PatchStudent(string id, [FromBody] JsonPatchDocument<UpdateStudentDto> patch)
    {
        return _studentService.PatchStudent(id, patch);
    }
}
