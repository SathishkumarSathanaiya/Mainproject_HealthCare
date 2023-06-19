using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mainproject_HealthCare.Models
{
    public class Prescription
    {

            [Key]    
            public int PrescriptionId { get; set; }

            public int PatientId { get; set; }

            [ForeignKey(nameof(PatientId))] 
            public virtual Patient? Patient { get; set; } 

            public string? PatientName => Patient?.Patient_Name;

             public int DoctorId { get; set; }
 
            [ForeignKey(nameof(DoctorId))]

            public virtual Doctor? Doctor { get; set; }

            public string? DoctorName => Doctor?.Doctor_Name;


            public string? Problem => Doctor?.Specialty;
            public string? Medicine1Name { get; set; }
            public string? Medicine1Dose { get; set; }
            public string? Medicine2Name { get; set; }
            public string? Medicine2Dose { get; set; }
            public string? Medicine3Name { get; set; }
            public string? Medicine3Dose { get; set; }

            public string? Injection { get; set; }

        // Add more properties for additional medicines as needed
            [DataType(DataType.Date)]
            public DateTime Date { get; set; }
    

    }
}
