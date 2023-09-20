using System.ComponentModel.DataAnnotations;

namespace ProjetoTabajaraApi.Data.Dtos.Attendance
{
    public class UpdateAttendanceDto
    {
        [Required(ErrorMessage = "O data é obrigatória")]
        public DateTime date { get; set; }

        [Required(ErrorMessage = "O Id do aluno é obrigatório")]
        public int studentId { get; set; }
    }
}
