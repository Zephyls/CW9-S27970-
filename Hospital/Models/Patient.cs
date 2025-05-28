namespace Hospital.Models;
public class Patient {
    public int PatientId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName  { get; set; } = null!;
    public DateTime Birthdate { get; set; }

    public ICollection<Prescription> Prescriptions { get; set; }
        = new List<Prescription>();
}