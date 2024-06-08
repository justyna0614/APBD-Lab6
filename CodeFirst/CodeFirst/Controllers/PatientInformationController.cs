using CodeFirst.Controllers.DTO;
using CodeFirst.Models;
using CodeFirst.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirst.Controllers;

[Route("/api/patients")]
[ApiController]
public class PatientInformationController : ControllerBase
{
    private readonly PatientInformationService _service;

    public PatientInformationController(PatientInformationService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetPatientInformation()
    {
        var patient = await _service.GetPatientInformations();
        var result = toDto(patient);
        return Ok(result);
    }

    private PatientInformationDTO toDto(Patient patient)
    {
        var patientInformation = new PatientInformationDTO();

        var patientDto = new PatientDTO();
        patientDto.IdPatient = patient.IdPatient;
        patientDto.FirstName = patient.FirstName;
        patientDto.LastName = patient.LastName;
        patientDto.Birthdate = patient.Birthdate;

        patientInformation.Patient = patientDto;
        List<PrescriptionDTO> prescriptionDtos = new List<PrescriptionDTO>();
        foreach (var p in patient.Prescriptions)
        {
            var prescrpitonDTO = new PrescriptionDTO();
            prescrpitonDTO.IdPrescription = p.IdPrescription;
            prescrpitonDTO.Date = p.Date;
            prescrpitonDTO.DueDate = p.DueDate;

            var doctorDTO = new DoctorDTO();
            doctorDTO.IdDoctor = p.IdDoctorNavigation.IdDoctor;
            doctorDTO.FirstName = p.IdDoctorNavigation.FirstName;
            doctorDTO.LastName = p.IdDoctorNavigation.LastName;
            doctorDTO.Email = p.IdDoctorNavigation.Email;

            List<MedicamentDTO> medicaments = new List<MedicamentDTO>();
            foreach (var m in p.PrescriptionMedicaments)
            {
                var medicamentDTO = new MedicamentDTO();
                medicamentDTO.IdMedicament = m.IdMedicament;
                medicamentDTO.Details = m.Details;
                medicamentDTO.Dose = m.Dose;
                medicaments.Add(medicamentDTO);
            }

            prescrpitonDTO.Medicaments = medicaments;
            prescriptionDtos.Add(prescrpitonDTO);
        }

        patientInformation.Prescriptions = prescriptionDtos;
        return patientInformation;
    }
}