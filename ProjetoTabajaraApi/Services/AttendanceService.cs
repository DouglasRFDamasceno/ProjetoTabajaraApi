using AutoMapper;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.AspNetCore.Mvc;
using ProjetoTabajaraApi.Data;
using ProjetoTabajaraApi.Data.Dtos.Attendance;
using ProjetoTabajaraApi.Data.Dtos.Student;
using ProjetoTabajaraApi.Models;

namespace ProjetoTabajaraApi.Services
{
    public class AttendanceService
    {
        private readonly IMapper _mapper;
        private readonly appDbContext _context;

        public AttendanceService(IMapper mapper, appDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public bool CreateOrUpdateAttendance(CreateAttendanceDto attendanceDto)
        {
            //List<Attendance> attendances = MapList(attendancesDto);
            Attendance newAttendance = _mapper.Map<Attendance>(attendanceDto);

            try
            {
                if (newAttendance == null)
                {
                    throw new ApplicationException("Falha ao cadastrar a presença");
                }

                //foreach (var attendance in attendances)
                //{
                    var existingAttendance = _context.Attendances
                        .FirstOrDefault(attendance => attendance.Date.Date == newAttendance.Date.Date && attendance.StudentId == newAttendance.StudentId);

                    if (existingAttendance != null)
                    {
                        // Atualize as propriedades do registro existente com os valores do novo objeto "attendance"
                        existingAttendance.Date = newAttendance.Date;
                        existingAttendance.Location = newAttendance.Location;
                        existingAttendance.StudentId = newAttendance.StudentId;
                        existingAttendance.StudentPresent = newAttendance.StudentPresent;
                        existingAttendance.Update_time = DateTime.Now;
                }
                    else
                    {
                    // Se o registro não existir, adicione-o
                    _context.Attendances.Add(newAttendance);
                    }
                //}

                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro cadastrar a presença. Erro: {ex}");
                return false;
            }
        }

        internal bool DeleteAttendance(int id)
        {
            var attendance = _context.Attendances.FirstOrDefault(attendance => attendance.Id == id);

            if (attendance == null) return false;

            try
            {
                _context.Remove(attendance);
                _context.SaveChanges();
                return true;
            } catch (Exception ex)
            {
                Console.WriteLine($"Erro ao excluir a presença. Erro: {ex}");
                return false;
            }
        }

        internal ReadAttendanceDto? GetAttendance(int id)
        {
            try
            {
                Attendance? attendance = _context!.Attendances?.FirstOrDefault(attendance => attendance.Id == id);

                if (attendance == null)
                {
                    return null;
                }

                ReadAttendanceDto attendanceDto = _mapper.Map<ReadAttendanceDto>(attendance);
                return attendanceDto;
            } catch (Exception ex)
            {
                Console.WriteLine($"Erro ao tentar obter a presença. Erro: {ex}");
                return null;
            }
        }

        internal List<ReadAttendanceDto> GetAttendances(DateTime startDate, DateTime finalDate)
        {
            Console.WriteLine(startDate);
            Console.WriteLine(finalDate);

            try
            {
                List<Attendance>? attendances = _context!.Attendances?
                    .OrderByDescending(attendance => attendance.Date)
                    .Where(attendance => attendance.Date.Date >= startDate.Date && attendance.Date.Date <= finalDate.Date)
                    .ToList();

                if (attendances == null) return new List<ReadAttendanceDto>();

                var attendanceDto = _mapper.Map<List<ReadAttendanceDto>>(attendances);

                return attendanceDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao tentar obter as presenças. Erro: {ex}");
                return new List<ReadAttendanceDto>();
            }
        }

        internal UpdateAttendanceDto UpdateAttendance(int id, UpdateAttendanceDto attendanceDto)
        {
            var attendance = _context!.Attendances?.FirstOrDefault(attendance => attendance.Id == id);

            if (attendance == null) return null;

            _mapper.Map(attendanceDto, attendance);
            _context.SaveChanges();

            return attendanceDto;
        }
        // Função para mapear uma lista de CreateAttendanceDto para uma lista de Attendance
        internal List<Attendance> MapList(List<CreateAttendanceDto> attendancesDto)
        {
            return attendancesDto.Select(attendanceDto => _mapper.Map<Attendance>(attendanceDto)).ToList();
        }

    }
}