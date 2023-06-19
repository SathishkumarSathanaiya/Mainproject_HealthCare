using System.ComponentModel.DataAnnotations;

namespace Mainproject_HealthCare.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; } 
        public string? Patient_Name { get; set; }

        public DateTime dateofBirth { get; set; }

        public int Age { get; set; }
        public string? Gender { get; set; }

        public string? Problem { get; set; }

        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email  is required")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        public string? Address { get; set; }

        public string? Emergency_Phone { get; set; }
    }
}
