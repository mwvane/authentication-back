using System.ComponentModel.DataAnnotations;

namespace authentication_back.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime PasswordLastUpdatedAt { get; set; }

    }
}
