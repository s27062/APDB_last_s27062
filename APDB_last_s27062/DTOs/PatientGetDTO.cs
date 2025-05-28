namespace APDB_last_s27062.DTOs;

public class PatientGetDTO
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public ICollection<PrescriptionGetForPatientDTO> Prescriptions { get; set; }
}