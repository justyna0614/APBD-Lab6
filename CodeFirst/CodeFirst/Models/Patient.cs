namespace CodeFirst.Models;

public class Patient
{
    public int IdPatient { get; set;}

    public String FirstName { get; set; }
    
    public String LastName { get; set; }
    
    public DateTime Birthdate { get; set; }
    
    public virtual ICollection<Prescription> Prescriptions { get; set; }
}