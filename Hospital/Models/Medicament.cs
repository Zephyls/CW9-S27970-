namespace Hospital.Models;
public class Medicament {
    public int MedicamentId { get; set; }
    public string Name        { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Type        { get; set; } = null!;

    public ICollection<PrescriptionMedicament> PrescriptionMedicaments
      { get; set; } = new List<PrescriptionMedicament>();
}
