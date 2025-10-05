using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = "";

        [Required]
        public string Password { get; set; } = "";

        public DateTime Birthday { get; set; }
    }
}
