using System.ComponentModel.DataAnnotations;

namespace TechnicalTestDotNet.DataAccess.DataBase.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string IdentificationNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }

        [Required]
        [StringLength(100)]
        public string UserCreated { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }
    }
}
