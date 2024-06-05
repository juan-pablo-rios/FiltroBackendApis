using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Veterinary.Data;
using Veterinary.Models;

namespace Veterinary.Repository.Appointments
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly VeterinaryDataBaseContext _context;
        public AppointmentRepository(VeterinaryDataBaseContext context)
        {
            _context = context;
        }
        // ----------------------- BUSCAR OWNER BY ID:
        public async Task<Owner> GetOwnerByIdAsync(int id)
        {
            var pet = await _context.Pets.FirstOrDefaultAsync(p => p.Id == id);
            return await _context.Owners.FirstOrDefaultAsync(o => o.Id == pet.OwnerId);
        }
        // ----------------------- CONFIRMAR CITA (CREAR):
        public async Task<Appointment> ExistAppointmentAlreadyCreateAsync(int vetId, int petId, DateTime appointmentDate)
        {
            // Se confirma que no se interponga con otra cita:
            var existAppointment = await _context.Appointments.FirstOrDefaultAsync(a => (a.VetId == vetId && a.PetId == petId && a.Date == appointmentDate) || (a.VetId == vetId && a.Date == appointmentDate) || (a.PetId == petId && a.Date == appointmentDate));
            return existAppointment;
        }
        // ----------------------- CREAR CITA:
        public async Task<Appointment> CreateAppointmentAsync(Appointment appointment)
        {
            _context.Add(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }
        // ----------------------- CONFIRMAR CITA (EDITAR):
        public async Task<Appointment> ExistAppointmentAlreadyUpdateAsync(int vetId, int petId, DateTime appointmentDate, int appointmentId)
        {
            // Se confirma que no se interponga con otra cita:
            var existAppointment = await _context.Appointments.FirstOrDefaultAsync(a => (a.VetId == vetId && a.PetId == petId && a.Date == appointmentDate && a.Id != appointmentId) || (a.VetId == vetId && a.Date == appointmentDate && a.Id != appointmentId) || (a.PetId == petId && a.Date == appointmentDate && a.Id != appointmentId));
            return existAppointment;
        }
        // ----------------------- EDITAR CITA:
        public async Task<Appointment> UpdateAppointmentAsync(Appointment appointment)
        {
            _context.Entry(appointment).State = EntityState.Modified;
            // _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }
        // ----------------------- LISTAR CITAS:
        public async Task<IEnumerable<Appointment>> ListAppointmentsAsync()
        {
            return await _context.Appointments.Include(a => a.Pet).Include(a => a.Vet).ToListAsync();
        }
        // ----------------------- BUSCAR CITA POR ID:
        public async Task<Appointment> ListAppointmentByIdAsync(int appointmentId)
        {
            return await _context.Appointments.Include(a => a.Pet).Include(a => a.Vet).FirstOrDefaultAsync(a => a.Id == appointmentId);
        }
        // ----------------------- LISTAR CITAS POR FECHA:
        public async Task<IEnumerable<Appointment>> AppointmentsByDate(DateTime date)
        {
            return await _context.Appointments.Where(a => a.Date.Date == date.Date).Include(a => a.Vet).Include(a => a.Pet).ToListAsync();
        }
        // ----------------------- LISTAR MASCOTAS DE UN DUEÑO:
        public async Task<IEnumerable<Pet>> ListOwnerPets(int ownerId)
        {
            return await _context.Pets.Where(p => p.OwnerId == ownerId).Include(p => p.Owner).ToListAsync();
        }
        // ----------------------- LISTAR CITAS VETERINARIO:
        public async Task<IEnumerable<Appointment>> ListAppointmentsVet(int id)
        {
            return await _context.Appointments.Where(a => a.VetId == id).Include(a => a.Vet).ToListAsync();
        }
    }
}