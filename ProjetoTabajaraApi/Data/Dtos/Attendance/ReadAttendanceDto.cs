using ProjetoTabajaraApi.Data.Dtos.Student;
using System.ComponentModel.DataAnnotations;

namespace ProjetoTabajaraApi.Data.Dtos.Attendance
{
    public class ReadAttendanceDto
    {
        public DateTime date { get; set; }

        public int studentId { get; set; }
        public ReadStudentDto Student { get; set; }
    }
}
