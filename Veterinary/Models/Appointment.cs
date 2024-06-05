using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Veterinary.Models;

public class Appointment
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string? Description { get; set; }
    public int PetId { get; set; }
    public int VetId { get; set; }
    public Pet? Pet { get; set; }
    public Vet? Vet { get; set; }
}