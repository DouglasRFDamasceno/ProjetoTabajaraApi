using ProjetoTabajaraApi.Data.Dtos.Student;
using System.ComponentModel.DataAnnotations;

namespace ProjetoTabajaraApi.Data.Dtos.Attendance
{
    public class ReadAttendanceDto
    {
        public DateTime Date { get; set; }

        public bool StudentPresent { get; set; }

        [MaxLength(255)]
        public string Location { get; set; }

        public DateTime Create_time { get; set; }

        public DateTime Update_time { get; set; }

        public int StudentId { get; set; }
    }
}
