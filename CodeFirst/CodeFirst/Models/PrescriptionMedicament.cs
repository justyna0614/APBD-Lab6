namespace CodeFirst.Models;

public class PrescriptionMedicament
{
    public int IdMedicament { get; set; }
    public int IdPrescription { get; set; }
    public int? Dose { get; set; }
    public String Details { get; set; }
    
    public virtual Prescription IdPrescriptionNavigation { get; set; }
    public virtual Medicament IdMedicamentNavigation { get; set; }
}