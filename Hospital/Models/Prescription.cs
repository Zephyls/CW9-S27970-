using System.ComponentModel.DataAnnotations;
namespace Hospital.Models;
public class Prescription {
    public int PrescriptionId { get; set; }
    public DateTime Date    { get; set; }
    public DateTime DueDate { get; set; }

    public int IdPatient { get; set; }
    public Patient Patient { get; set; } = null!;

    public int IdDoctor { get; set; }
    public Doctor Doctor { get; set; } = null!;

    public ICollection<PrescriptionMedicament> PrescriptionMedicaments
    { get; set; } = new List<PrescriptionMedicament>();

    [Timestamp]
    public byte[] RowVersion { get; set; } = null!;
}