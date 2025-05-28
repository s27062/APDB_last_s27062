namespace APDB_last_s27062.DTOs;

public class PrescriptionGetForPatientDTO
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public ICollection<PrescriptionMedicamentGetForPatientDTO> Medicaments { get; set; }
    public DoctorGetDTO Doctor { get; set; }
}