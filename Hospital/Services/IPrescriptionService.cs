namespace Hospital.Services;

using Hospital.DTOs;
public interface IPrescriptionService {
    Task<PrescriptionDto> AddAsync(NewPrescriptionDto dto);
    Task<PatientDetailsDto> GetPatientAsync(int idPatient);
}