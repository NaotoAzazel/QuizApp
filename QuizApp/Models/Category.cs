using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        public List<Question> Questions { get; set; } = new();
    }
}
