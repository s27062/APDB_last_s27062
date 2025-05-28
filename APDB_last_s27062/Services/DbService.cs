using APDB_last_s27062.Data;
using APDB_last_s27062.DTOs;
using APDB_last_s27062.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace APDB_last_s27062.Services;

public interface IDbService
{
    public Task<ICollection<PrescriptionGetDTO>> GetPrescriptionsAsync();
    public Task<PrescriptionGetDTO> PrescriptionAddAsync(PrescriptionAddDTO prescription);
    public Task<ICollection<PatientGetDTO>> GetPatientsAsync();
}

public class DbService(AppDbContext data) : IDbService
{
    public async Task<ICollection<PrescriptionGetDTO>> GetPrescriptionsAsync()
    {
        return await data.Prescriptions.Select(pre => new PrescriptionGetDTO
        {
            IdPrescription = pre.IdPrescription,
            Date = pre.Date,
            DueDate = pre.DueDate,
            Doctor = new DoctorGetDTO()
            {
                IdDoctor = pre.Doctor.IdDoctor,
                FirstName = pre.Doctor.FirstName,
                LastName = pre.Doctor.LastName,
                Email = pre.Doctor.Email,
            },
            Patient = new PatientGetDTO()
            {
                IdPatient = pre.Patient.IdPatient,
                FirstName = pre.Patient.FirstName,
                LastName = pre.Patient.LastName,
                BirthDate = pre.Patient.BirthDate,
            },
            PrescriptionsMedicaments = pre.PrescriptionsMedicaments.Select(premed => new PrescriptionMedicamentGetDTO
            {
                Medicament = new MedicamentGetDTO()
                {
                    IdMedicament = premed.Medicaments.IdMedicament,
                    Name = premed.Medicaments.Name,
                    Description = premed.Medicaments.Description,
                    Type = premed.Medicaments.Type
                },
                Dose = premed.Dose,
                Details = premed.Details,
            }).ToList()
        }).ToListAsync();
    }

    public async Task<PrescriptionGetDTO> PrescriptionAddAsync(PrescriptionAddDTO prescriptionData)
    {
        if (prescriptionData.DueDate < prescriptionData.Date)
        {
            throw new BadHttpRequestException("Due date cannot be earlier than Date");
        }

        if (prescriptionData.Medicaments.Count > 10)
        {
            throw new BadHttpRequestException("Medicaments cannot be more than 10");
        }

        foreach (var medicamentDto in prescriptionData.Medicaments)
        {
            var medicament = await data.Medicaments.FirstOrDefaultAsync(e => e.IdMedicament == medicamentDto.IdMedicament);

            if (medicament == null)
            {
                throw new BadHttpRequestException("Medicament " + medicamentDto.IdMedicament + " does not exist");
            }
        }
        
        var patientDto = prescriptionData.Patient;
        var patient = await data.Patients.FirstOrDefaultAsync(e => e.IdPatient == patientDto.IdPatient);
        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = prescriptionData.Patient.FirstName,
                LastName = prescriptionData.Patient.LastName,
                BirthDate = prescriptionData.Patient.BirthDate,
            };
            await data.Patients.AddAsync(patient);
            await data.SaveChangesAsync();
        }

        var prescription = new Prescription
        {
            Date = prescriptionData.Date,
            DueDate = prescriptionData.DueDate,
            IdDoctor = 1,
            IdPatient = patient.IdPatient,
            PrescriptionsMedicaments = prescriptionData.Medicaments.Select(med => new PrescriptionMedicament
            {
                IdPrescription = 2,
                IdMedicament = med.IdMedicament,
                Dose = med.Dose,
                Details = med.Details,
            }).ToList()
        };
        
        await data.Prescriptions.AddAsync(prescription);
        await data.SaveChangesAsync();

        return GetPrescriptionsAsync().Result.Last();
    }

    public async Task<ICollection<PatientGetDTO>> GetPatientsAsync()
    {
        return await data.Patients.Select(p => new PatientGetDTO
        {
            IdPatient = p.IdPatient,
            FirstName = p.FirstName,
            LastName = p.LastName,
            BirthDate = p.BirthDate,
            Prescriptions = p.Prescriptions.Select(pre => new PrescriptionGetForPatientDTO()
            {
                IdPrescription = pre.IdPrescription,
                Date = pre.Date,
                DueDate = pre.DueDate,
                Medicaments = pre.PrescriptionsMedicaments.Select(med => new PrescriptionMedicamentGetForPatientDTO()
                {
                    IdMedicament = med.IdMedicament,
                    Name = med.Medicaments.Name,
                    Dose = med.Dose,
                    Details = med.Details,
                }).ToList(),
                Doctor = new DoctorGetDTO()
                {
                    IdDoctor = pre.Doctor.IdDoctor,
                    FirstName = pre.Doctor.FirstName,
                    LastName = pre.Doctor.LastName,
                    Email = pre.Doctor.Email,
                }
            }).ToList()
        }).ToListAsync();
    }
    
}