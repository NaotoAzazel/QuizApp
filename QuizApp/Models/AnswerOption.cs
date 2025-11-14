using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Models
{
    public class AnswerOption
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; } = "";

        public bool IsCorrect { get; set; }

        [ForeignKey(nameof(Question))]
        public int QuestionId { get; set; }

        public Question? Question { get; set; }
    }
}
