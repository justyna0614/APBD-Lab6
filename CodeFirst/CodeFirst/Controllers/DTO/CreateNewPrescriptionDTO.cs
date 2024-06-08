using CodeFirst.Models;

namespace CodeFirst.Controllers.DTO;

public class CreateNewPrescriptionDTO
{
    public PatientDTO Patient { get; set; }
    public List<MedicamentDTO> Medicaments { get; set; }
    public  DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
}