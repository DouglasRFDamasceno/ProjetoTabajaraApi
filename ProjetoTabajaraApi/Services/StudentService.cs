using AutoMapper;
using ProjetoTabajaraApi.Data;
using ProjetoTabajaraApi.Data.Dtos.Student;
using ProjetoTabajaraApi.Models;

namespace ProjetoTabajaraApi.Services;

public class StudentService
{
    private readonly IMapper _mapper;
    private readonly appDbContext _context;

    public StudentService(IMapper mapper, appDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public Student CreateStudent(CreateStudentDto studentDto)
    {
        Student student = _mapper!.Map<Student>(studentDto);

        _context.Students.Add(student);
        _context.SaveChanges();

        if (student == null)
        {
            throw new ApplicationException("Falha ao cadastrar o estudante");
        }

        return student;
    }

    public ReadStudentDto? GetStudent(int id)
    {
        Student? student = _context!.Students?.FirstOrDefault(student => student.Id == id);

        if (student == null)
        {
            return null;
        }

        ReadStudentDto studentDto = _mapper.Map<ReadStudentDto>(student);
        return studentDto;
    }

    public UpdateStudentDto? UpdateStudent(int id, UpdateStudentDto studentDto)
    {
        var student = _context!.Students?.FirstOrDefault(student => student.Id == id);

        if (student == null) return null;

        _mapper.Map(studentDto, student);
        _context.SaveChanges();

        return studentDto;
    }

    public bool DeleteStudent(int id)
    {
        var student = _context.Students.FirstOrDefault(student => student.Id == id);

        if (student == null) return false;

        _context.Remove(student);
        _context.SaveChanges();
        return true;
    }

    public List<Student>? GetStudents(int skip, int take)
    {
        List<Student>? students = _context!.Students?
            .OrderByDescending(student => student.Name)
            .Skip(skip)
            .Take(take)
            .ToList();

        return students;
    }
}
