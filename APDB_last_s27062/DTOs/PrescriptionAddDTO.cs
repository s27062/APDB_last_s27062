using System.ComponentModel.DataAnnotations;

namespace APDB_last_s27062.DTOs;

public class PrescriptionAddDTO
{
    public int? IdPrescription { get; set; }
    [Required]
    public PatientGetDTO Patient { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public DateTime DueDate { get; set; }
    public DoctorGetDTO? Doctor { get; set; }
    [Required]
    public ICollection<PrescriptionMedicamentGetDTO> Medicaments { get; set; }
}