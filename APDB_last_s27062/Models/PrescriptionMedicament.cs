using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APDB_last_s27062.Models;

[Table("Prescription_Medicament")]
[PrimaryKey("IdMedicament", "IdPrescription")]
public class PrescriptionMedicament
{
    public int IdMedicament { get; set; }
    
    public int IdPrescription { get; set; }
    
    public int? Dose { get; set; }
    
    [MaxLength(100)]
    public string Details { get; set; } = null!;
    
    [ForeignKey("IdMedicament")]
    public virtual Medicament Medicaments { get; set; } = null!;
    
    [ForeignKey("IdPrescription")]
    public virtual Prescription Prescription { get; set; } = null!;
}