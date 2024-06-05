using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Veterinary.Models;

public class Owner
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    [JsonIgnore]
    public List<Pet>? Pets { get; set; }
}