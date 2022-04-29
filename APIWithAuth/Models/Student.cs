using System.ComponentModel.DataAnnotations;

namespace APIWithAuth.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string Gender { get; set; } = string.Empty;
    }
}
