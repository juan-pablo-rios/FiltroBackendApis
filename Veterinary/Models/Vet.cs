using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Veterinary.Models;

public class Vet
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
    [JsonIgnore]
    public List<Appointment>? Appointments { get; set; }
}