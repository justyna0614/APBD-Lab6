using CodeFirst.Context;
using CodeFirst.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace CodeFirst.Services;

public class PatientInformationService
{
    private readonly AppDbContext _context;

    public PatientInformationService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<Patient> GetPatientInformations()
    {
        return await _context.Patient
            .Include(p => p.Prescriptions).ThenInclude(pr => pr.PrescriptionMedicaments)
            .ThenInclude(prm => prm.IdMedicamentNavigation)
            .Include(p => p.Prescriptions).ThenInclude(pr => pr.IdDoctorNavigation)
            .FirstAsync();
    }
}