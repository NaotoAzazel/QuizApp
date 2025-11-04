using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Models
{
    public class UserQuizResult
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User? User { get; set; }

        [ForeignKey(nameof(Category))]
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public int CorrectAnswers { get; set; }

        public DateTime CompletedAt { get; set; } = DateTime.Now.ToUniversalTime();
    }
}
