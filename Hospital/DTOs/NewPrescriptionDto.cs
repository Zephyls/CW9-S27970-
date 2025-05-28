namespace Hospital.DTOs;
public class NewPrescriptionDto {
    public int? IdPatient { get; set; }
    public PatientDto Patient { get; set; }

    public int IdDoctor { get; set; }

    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }

    public List<MedicamentInputDto> Medicaments { get; set; }
        = new();
}

public class PatientDto {
    public string FirstName { get; set; } = null!;
    public string LastName  { get; set; } = null!;
    public DateTime Birthdate { get; set; }
}

public class MedicamentInputDto {
    public int IdMedicament { get; set; }
    public int Dose         { get; set; }
    public string Details  { get; set; } = null!;
}