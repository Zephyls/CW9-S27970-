namespace Hospital.Services;

using AutoMapper;
using Hospital.Models;
using Hospital.DTOs;

public class MappingProfile : Profile {
    public MappingProfile() {
        CreateMap<NewPrescriptionDto, Prescription>()
            .ForMember(dest => dest.PrescriptionMedicaments,
                opt => opt.MapFrom(src =>
                    src.Medicaments.Select(m => new PrescriptionMedicament {
                        MedicamentId  = m.IdMedicament,
                        Dose         = m.Dose,
                        Details      = m.Details
                    })));

        CreateMap<PatientDto, Patient>();
        
        CreateMap<Prescription, PrescriptionDto>()
            .ForMember(d => d.Doctor, o => o.MapFrom(s => s.Doctor))
            .ForMember(d => d.Medicaments,
                o => o.MapFrom(s => s.PrescriptionMedicaments));

        CreateMap<PrescriptionMedicament, MedicamentDetailDto>()
            .ForMember(d => d.IdMedicament, o => o.MapFrom(s => s.MedicamentId ))
            .ForMember(d => d.Name,        o => o.MapFrom(s => s.Medicament.Name))
            .ForMember(d => d.Details,     o => o.MapFrom(s => s.Details));

        CreateMap<Doctor, DoctorDto>();
        
        CreateMap<Patient, PatientDetailsDto>();
    }
}