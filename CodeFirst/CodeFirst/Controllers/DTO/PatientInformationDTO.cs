namespace CodeFirst.Controllers.DTO;

public class PatientInformationDTO
{
    public PatientDTO Patient { get; set; }
    public List<PrescriptionDTO> Prescriptions { get; set; }
}