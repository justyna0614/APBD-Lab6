namespace CodeFirst.Models;

public class Medicament
{
    public int IdMedicament { get; set; }
    public String Name { get; set; }
    public String Description { get; set; }
    public String Type { get; set; }
    
    public virtual ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
}