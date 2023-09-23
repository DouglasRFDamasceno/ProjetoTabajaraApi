using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoTabajaraApi.Models
{
    public class Attendance
    {
        [Key]
        [Required]
        public long Id { get; set; }

        [Required(ErrorMessage = "O data é obrigatória")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "O presença é obrigatória.")]
        public bool StudentPresent { get; set; }

        [Required(ErrorMessage = "O local do treino é obrigatório")]
        [MaxLength(255)]
        public string Location { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Create_time { get; set; }

        public DateTime Update_time { get; set; }

        [Required(ErrorMessage = "O Id do aluno é obrigatório")]
        public int StudentId { get; set; }

        public virtual Student Student { get; set; }
    }
}
