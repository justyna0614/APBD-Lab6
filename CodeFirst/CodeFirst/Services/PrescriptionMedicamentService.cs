using CodeFirst.Context;
using CodeFirst.Controllers.DTO;
using CodeFirst.Models;
using CodeFirst.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Services;

public class PrescriptionMedicamentService
{
    
    private readonly AppDbContext _context;
    
    public PrescriptionMedicamentService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Boolean> AddPrescriptionMedicamentAsync(int id, CreateNewPrescriptionDTO request)
    {
        var patientId = request.Patient.IdPatient;
        if(!_context.Patient.Where(p => p.IdPatient == patientId).Any())
        {
            Patient patient = await CreatePatient(request.Patient);
            patientId = patient.IdPatient;
        }
    
        HashSet<int> medicamentIds = new HashSet<int>();
        foreach (var med in request.Medicaments)
        {
            medicamentIds.Add(med.IdMedicament);
        }

        if (medicamentIds.Count > 10)
        {
            throw new TooManyMedicamentException();

        }
    
        var medsCount = await _context.Medicament.Where(m => medicamentIds.Contains(m.IdMedicament)).CountAsync();
    
        if (medsCount != medicamentIds.Count)
        {
            throw new MedicamentNotFoundException();
        }

        if (request.DueDate < request.Date)
        {
            throw new WrongDueDateException();
        }
        
        var prescription = await CreatePrescription(request, id, patientId);


        foreach (var med in request.Medicaments)
        {
            await CreatePrescriptionMedicament(med, prescription.IdPrescription);
        }

        return await _context.SaveChangesAsync() == request.Medicaments.Count();
    }
    
    
    
    
    private async Task<Patient> CreatePatient(PatientDTO patientDTO)
    {
        var patient = new Patient();
        patient.FirstName = patientDTO.FirstName;
        patient.LastName = patientDTO.LastName;
        patient.Birthdate = patientDTO.Birthdate;
        await _context.Patient.AddAsync(patient);
        await _context.SaveChangesAsync();
        return patient;
    }
    
    private async Task<Prescription> CreatePrescription(CreateNewPrescriptionDTO request, int doctorId, int patientId)
    {
        var prescription = new Prescription();
        prescription.Date = request.Date;
        prescription.DueDate = request.DueDate;
        prescription.IdDoctor = doctorId;
        prescription.IdPatient = patientId;
        await _context.Prescription.AddAsync(prescription);
        await _context.SaveChangesAsync();
        return prescription;
    }
    
    private async Task<PrescriptionMedicament> CreatePrescriptionMedicament(MedicamentDTO medicament, int prescriptionId)
    {
        var prescriptionMedicament = new PrescriptionMedicament();
        prescriptionMedicament.IdMedicament = medicament.IdMedicament;
        prescriptionMedicament.IdPrescription = prescriptionId;
        prescriptionMedicament.Dose = medicament.Dose;
        prescriptionMedicament.Details = medicament.Details;
        await _context.PrescriptionMedicament.AddAsync(prescriptionMedicament);
        return prescriptionMedicament;
    }
}