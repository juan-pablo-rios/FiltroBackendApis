using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Veterinary.Data;
using Veterinary.Models;

namespace Veterinary.Repository.Appointments
{
    public interface IAppointmentRepository
    {
        // ----------------------- CONFIRMAR CITA (CREAR):
        Task<Appointment> ExistAppointmentAlreadyCreateAsync(int vetId, int petId, DateTime appointmentDate);
        // ----------------------- CREAR CITA:
        Task<Appointment> CreateAppointmentAsync(Appointment appointment);
        // ----------------------- CONFIRMAR CITA (EDITAR):
        Task<Appointment> ExistAppointmentAlreadyUpdateAsync(int vetId, int petId, DateTime appointmentDate, int appointmentId);
        // ----------------------- EDITAR CITA:
        Task<Appointment> UpdateAppointmentAsync(Appointment appointment);
        // ----------------------- LISTAR CITAS:
        Task<IEnumerable<Appointment>> ListAppointmentsAsync();
        // ----------------------- BUSCAR CITA POR ID:
        Task<Appointment> ListAppointmentByIdAsync(int appointmentId);
        // ----------------------- BUSCAR OWNER BY ID:
        Task<Owner> GetOwnerByIdAsync(int id);
        // ----------------------- LISTAR CITAS POR FECHA:
        Task<IEnumerable<Appointment>> AppointmentsByDate(DateTime date);
        // ----------------------- LISTAR MASCOTAS DE UN DUEÑO:
        Task<IEnumerable<Pet>> ListOwnerPets(int ownerId);
        // ----------------------- LISTAR CITAS VETERINARIO:
        Task<IEnumerable<Appointment>> ListAppointmentsVet(int id);
    }
}