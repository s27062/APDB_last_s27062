namespace APDB_last_s27062.DTOs;

public class PrescriptionMedicamentGetDTO
{
    public int IdMedicament { get; set; }
    public MedicamentGetDTO? Medicament { get; set; }
    public int? Dose { get; set; }
    public string Details { get; set; }
}