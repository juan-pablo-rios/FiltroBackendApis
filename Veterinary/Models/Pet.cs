using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Veterinary.Models;

public class Pet
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Specie { get; set; }
    public string? Breed { get; set; }
    public DateOnly DateBirth { get; set; }
    public string? Photo { get; set; }
    public int? OwnerId { get; set; }
    public Owner? Owner { get; set; }
    [JsonIgnore]
    public List<Appointment>? Appointments { get; set; }
}