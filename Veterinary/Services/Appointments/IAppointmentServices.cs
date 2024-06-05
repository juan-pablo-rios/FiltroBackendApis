using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Veterinary.Models;
using Veterinary.Utils;

namespace Veterinary.Services.Appointments
{
    public interface IAppointmentServices
    {
        // ----------------------- CREAR CITA:
        Task<ResponseUtils<Appointment>> CreateAppointmentAsync(Appointment appointment);
        // ----------------------- LISTAR CITAS:
        Task<ResponseUtils<Appointment>> ListAppointmentsAsync();
        // ----------------------- BUSCAR CITA POR ID:
        Task<ResponseUtils<Appointment>> ListAppointmentByIdAsync(int appointmentId);
        // ----------------------- LISTAR CITAS POR FECHA:
        Task<ResponseUtils<Appointment>> AppointmentsByDate(DateTime date);
        // ----------------------- LISTAR MASCOTAS DE UN DUEÑO:
        Task<ResponseUtils<Pet>> ListOwnerPets(int ownerId);
        // ----------------------- LISTAR CITAS VETERINARIO:
        Task<ResponseUtils<Appointment>> ListAppointmentsVet(int id);
    }
}