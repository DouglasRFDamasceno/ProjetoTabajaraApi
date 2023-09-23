using System.ComponentModel.DataAnnotations;

namespace ProjetoTabajaraApi.Data.Dtos.Attendance
{
    public class UpdateAttendanceDto
    {
        [Required(ErrorMessage = "O data é obrigatória")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "O presença é obrigatória.")]
        public bool StudentPresent { get; set; }

        [Required(ErrorMessage = "O local do treino é obrigatório")]
        [MaxLength(255)]
        public string Location { get; set; }

        public DateTime Update_time { get; set; }

        [Required(ErrorMessage = "O Id do aluno é obrigatório")]
        public int StudentId { get; set; }
    }
}
