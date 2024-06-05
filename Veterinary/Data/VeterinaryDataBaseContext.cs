using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Veterinary.Models;

namespace Veterinary.Data;
public class VeterinaryDataBaseContext : DbContext
{
    public VeterinaryDataBaseContext(DbContextOptions<VeterinaryDataBaseContext> Options) : base(Options)
    {

    }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<Vet> Vets { get; set; }
}