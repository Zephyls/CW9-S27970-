namespace Hospital.DTOs;
public class PatientDetailsDto {
    public int IdPatient { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName  { get; set; } = null!;
    public DateTime Birthdate { get; set; }

    public List<PrescriptionDto> Prescriptions { get; set; }
        = new();
}

public class PrescriptionDto {
    public int IdPrescription { get; set; }
    public DateTime Date    { get; set; }
    public DateTime DueDate { get; set; }

    public DoctorDto Doctor { get; set; } = null!;
    public List<MedicamentDetailDto> Medicaments { get; set; }
        = new();
}

public class MedicamentDetailDto {
    public int IdMedicament { get; set; }
    public string Name        { get; set; } = null!;
    public int Dose           { get; set; }
    public string Details     { get; set; } = null!;
}

public class DoctorDto {
    public int IdDoctor { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName  { get; set; } = null!;
}