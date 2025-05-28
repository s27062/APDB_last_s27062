namespace APDB_last_s27062.DTOs;

public class PrescriptionGetDTO
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public PatientGetDTO Patient { get; set; }
    public DoctorGetDTO Doctor { get; set; }
    public ICollection<PrescriptionMedicamentGetDTO> PrescriptionsMedicaments { get; set; }
}