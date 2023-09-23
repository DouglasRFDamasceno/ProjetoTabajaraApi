using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoTabajaraApi.Data.Dtos.Attendance
{
    public class CreateAttendanceDto
    {
        [Required(ErrorMessage = "O data é obrigatória")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "O presença é obrigatória.")]
        public bool StudentPresent { get; set; }

        [Required(ErrorMessage = "O local do treino é obrigatório")]
        [MaxLength(255)]
        public string Location { get; set; }

        [Required(ErrorMessage = "O Id do aluno é obrigatório")]
        public int StudentId { get; set; }
    }
}
