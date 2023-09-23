﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoTabajaraApi.Data;
using ProjetoTabajaraApi.Data.Dtos.Student;
using ProjetoTabajaraApi.Data.Dtos.User;
using ProjetoTabajaraApi.Models;

namespace ProjetoTabajaraApi.Services;

public class StudentService: ControllerBase
{
    private readonly IMapper _mapper;
    private readonly appDbContext _context;

    public StudentService(IMapper mapper, appDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public Student? CreateStudent(CreateStudentDto studentDto)
    {
        try
        {
            Student student = _mapper!.Map<Student>(studentDto);

            _context.Students.Add(student);
            _context.SaveChanges();

            if (student == null)
            {
                throw new ApplicationException("Falha ao cadastrar o estudante");
            }

            return student;
        } catch (Exception ex)
        {
            Console.WriteLine($"Erro ao criar o estudante. Erro {ex}");
            return null;
        }
    }

    public ReadStudentDto? GetStudent(int id)
    {
        try
        {
            Student? student = _context!.Students?.FirstOrDefault(student => student.Id == id);

            if (student == null)
            {
                return null;
            }

            ReadStudentDto studentDto = _mapper.Map<ReadStudentDto>(student);
            return studentDto;
        } catch (Exception ex)
        {
            Console.WriteLine($"Erro ao obter o estudante. Erro {ex}");
            return null;
        }
    }

    public UpdateStudentDto? UpdateStudent(int id, UpdateStudentDto studentDto)
    {
        try
        {
            var student = _context!.Students?.FirstOrDefault(student => student.Id == id);

            if (student == null) return null;

            _mapper.Map(studentDto, student);
            _context.SaveChanges();

            return studentDto;
        } catch (Exception ex)
        {
            Console.WriteLine($"Erro ao atualziar o estudante. Erro {ex}");
            return null;
        }
    }

    public IActionResult PatchStudent(string id, JsonPatchDocument<UpdateStudentDto> patch)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("O ID do estudante é inválido.");
        }

        Student? student = _context.Students.FirstOrDefault(student => student.Id == int.Parse(id));

        if (student == null)
        {
            return NotFound("Estudante não encontrado.");
        }

        UpdateStudentDto studentToUpdate = _mapper.Map<UpdateStudentDto>(student);

        patch.ApplyTo(studentToUpdate, ModelState);

        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        try
        {
            // Mapeamento reverso após a validação do modelo.
            _mapper.Map(studentToUpdate, student);
            _context.SaveChanges();
            return Ok(studentToUpdate); // Retorna um status 204 (No Content) em caso de sucesso.
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ocorreu um erro ao atualizar o usuário: {ex.Message}");
        }
    }

    public bool DeleteStudent(int id)
    {
        try
        {
            var student = _context.Students.FirstOrDefault(student => student.Id == id);

            if (student == null) return false;

            _context.Remove(student);
            _context.SaveChanges();
            return true;
        } catch (Exception ex)
        {
            Console.WriteLine($"Erro ao remover o estudante. Erro {ex}");
            return false;
        }
    }

    public object GetStudents(DateTime date, int skip, int take)
    {
        try
        {
            // Total de estudantes que existem na base de dados
            int totalStudents = _context.Students.Count();

            IQueryable<Student> query = _context.Students
                .OrderBy(student => student.Name)
                .Skip(skip)
                .Take(take);

            if (date != DateTime.MinValue)
            {
                query = query
                    .Include(student => student.Attendances)
                    .Where(student => student.Attendances.Any(attendance => attendance.Date.Date == date.Date));

                // Sobreescrita do total caso a data da presença seja informada
                totalStudents = query.Count();
            }

            List<Student> students = query.ToList();

            if (students == null)
            {
                return new { students = new List<ReadStudentDto>(), totalStudents = 0 };
            }

            var studentsDto = _mapper.Map<List<ReadStudentDto>>(students);

            return new { students = studentsDto, totalStudents };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao obter os estudantes. Erro {ex}");
            return new { students = new List<ReadStudentDto>(), totalStudents = 0 };
        }
    }
}
