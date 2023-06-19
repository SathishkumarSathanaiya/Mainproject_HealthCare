using System.ComponentModel.DataAnnotations;

namespace Mainproject_HealthCare.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }

        public string? Doctor_Name { get; set; }

        public string? Age { get; set; }
        public string? Gender { get; set; }

        public string? Specialty { get; set; }

        [Required(ErrorMessage = "Email  is required")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        public string? Password { get; set; }
        public string? phone_number { get; set; }

        

        public string? Address { get; set; }
    }
}
