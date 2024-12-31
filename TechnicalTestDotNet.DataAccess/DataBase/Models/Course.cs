using System.ComponentModel.DataAnnotations;

namespace TechnicalTestDotNet.DataAccess.DataBase.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string UserCreated { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }
    }
}
