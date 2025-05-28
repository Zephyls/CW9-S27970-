namespace Hospital.Services;

using AutoMapper;
using Hospital.Data;
using Hospital.DTOs;
using Hospital.Exceptions;
using Hospital.Models; 
using Microsoft.EntityFrameworkCore;

public class PrescriptionService : IPrescriptionService {
  private readonly AppDbContext _ctx;
  private readonly IMapper _mapper;

  public PrescriptionService(AppDbContext ctx, IMapper mapper) {
    _ctx    = ctx;
    _mapper = mapper;
  }

  public async Task<PrescriptionDto> AddAsync(NewPrescriptionDto dto) {
    if (dto.Medicaments.Count > 10)
      throw new BusinessException("No more than 10 medicaments are allowed.");
    if (dto.DueDate < dto.Date)
      throw new BusinessException("DueDate must be greater than or equal to Date.");
    
    var meds = await _ctx.Medicaments
      .Where(m => dto.Medicaments.Select(x => x.IdMedicament).Contains(m.MedicamentId))
      .Select(m => m.MedicamentId)
      .ToListAsync();

    if (meds.Count != dto.Medicaments.Count)
      throw new BusinessException("One or more medicaments do not exist.");
    
    Patient patient;
    if (dto.IdPatient.HasValue) {
      patient = await _ctx.Patients.FindAsync(dto.IdPatient.Value)
                ?? throw new BusinessException("Patient does not exist.");
    } else {
      patient = _mapper.Map<Patient>(dto.Patient);
      _ctx.Patients.Add(patient);
    }
    
    var pres = _mapper.Map<Prescription>(dto);
    pres.Patient = patient;
    _ctx.Prescriptions.Add(pres);

    try {
      await _ctx.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException) {
      throw new BusinessException("A concurrency conflict occurred while saving data.");
    }
    
    var loaded = await _ctx.Prescriptions
      .Include(p => p.Doctor)
      .Include(p => p.PrescriptionMedicaments)
        .ThenInclude(pm => pm.Medicament)
      .FirstAsync(p => p.PrescriptionId  == pres.PrescriptionId );

    return _mapper.Map<PrescriptionDto>(loaded);
  }

  public async Task<PatientDetailsDto> GetPatientAsync(int idPatient) {
    var patient = await _ctx.Patients
      .Include(p => p.Prescriptions.OrderBy(pr => pr.DueDate))
        .ThenInclude(pr => pr.Doctor)
      .Include(p => p.Prescriptions)
        .ThenInclude(pr => pr.PrescriptionMedicaments)
          .ThenInclude(pm => pm.Medicament)
      .FirstOrDefaultAsync(p => p.PatientId  == idPatient);

    if (patient == null)
      throw new NotFoundException("Patient not found.");

    return _mapper.Map<PatientDetailsDto>(patient);
  }
}
