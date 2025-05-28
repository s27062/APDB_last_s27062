namespace APDB_last_s27062.DTOs;

public class PrescriptionMedicamentGetForPatientDTO
{
    public int IdMedicament { get; set; }
    public string Name { get; set; }
    public int? Dose { get; set; }
    public string Details { get; set; }
}