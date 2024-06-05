using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Veterinary.Utils;
using Veterinary.Repository.Appointments;
using Veterinary.Models;
using SimulacroDos.Utils;
using System.Security.Cryptography.X509Certificates;

namespace Veterinary.Services.Appointments
{
    public class AppointmentServices : IAppointmentServices
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public AppointmentServices(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        // ----------------------- CREAR CITA:
        public async Task<ResponseUtils<Appointment>> CreateAppointmentAsync(Appointment appointment)
        {
            try
            {
                // Se confirma que no se interponga con otra cita existente:
                var existAppointment = await _appointmentRepository.ExistAppointmentAlreadyCreateAsync(appointment.VetId, appointment.PetId, appointment.Date);
                // Condicional que determina si es posible crear la nueva cita:
                if(existAppointment == null)
                {
                    // Se agrega la nueva cita a la entidad 'Appointments':
                    var newAppointment = await _appointmentRepository.CreateAppointmentAsync(appointment);
                    // Se busca al dueño de la mascota asociado con la cita:
                    var owner = await _appointmentRepository.GetOwnerByIdAsync(appointment.PetId);
                    // Se instancia un objeto de la clase 'Mailersend':
                    var sendEmail = new MailersendUtils();
                    // Se utiliza el método .EnviarCorreo(), se envía como parámetro el email del dueño y la fecha de la cita:
                    sendEmail.EnviarCorreo(owner.Email, appointment.Date);
                    // Retorno de la respuesta éxitosa con la estructura de la clase 'ResponseUtils':
                    return new ResponseUtils<Appointment>(true, new List<Appointment>{newAppointment}, null, 201, message: "¡La cita ha sido registrada!");
                }
                else
                {
                    return new ResponseUtils<Appointment>(false, null, null, 406, message: "¡La cita se interpone con otra!");
                }
            }
            catch (Exception ex)
            {
                return new ResponseUtils<Appointment>(false, null, null, 422, message: ex.Message);
            }
        }
        // ----------------------- EDITAR CITA:
        public async Task<ResponseUtils<Appointment>> UpdateAppointmentAsync(Appointment appointment)
        {
            try
            {
                // Se confirma que la cita exista:
                var existAppointment = await _appointmentRepository.ListAppointmentByIdAsync(appointment.Id);
                // Condicional que determina si la cita existe:
                if(existAppointment != null)
                {
                    // Se confirma que no se interponga con otra cita existente:
                    var scheduledAppointment = await _appointmentRepository.ExistAppointmentAlreadyUpdateAsync(appointment.VetId, appointment.PetId, appointment.Date, appointment.Id);
                    // Condicional que determina si es posible editar la cita:
                    if(scheduledAppointment == null)
                    {
                        // Se actualiza la cita:
                        await _appointmentRepository.UpdateAppointmentAsync(appointment);
                        // Retorno de la respuesta éxitosa con la estructura de la clase 'ResponseUtils':
                        return new ResponseUtils<Appointment>(true, new List<Appointment>{appointment}, null, 200, message: "¡La cita ha sido actualizada!");
                    }
                    else
                    {
                        return new ResponseUtils<Appointment>(false, null, null, 406, message: "¡La cita se interpone con otra!");
                    }
                }
                else
                {
                    return new ResponseUtils<Appointment>(false, null, null, 404, message: "¡No se ha encontrado la cita!");
                }
                
            }
            catch (Exception ex)
            {
                return new ResponseUtils<Appointment>(false, null, null, 422, message: ex.Message);
            }
        }
        // ----------------------- LISTAR CITAS:
        public async Task<ResponseUtils<Appointment>> ListAppointmentsAsync()
        {
            try
            {
                var appointments = await _appointmentRepository.ListAppointmentsAsync();
                if(appointments != null)
                {
                    // Retorno de la respuesta éxitosa con la estructura de la clase 'ResponseUtils':
                    return new ResponseUtils<Appointment>(true, new List<Appointment>(appointments), null, 200, message: "¡Listado de citas!");
                }
                else
                {
                    return new ResponseUtils<Appointment>(false, null, null, 404, message: "¡No hay citas registradas!");
                }
            }
            catch (Exception ex)
            {
                return new ResponseUtils<Appointment>(false, null, null, 422, message: ex.Message);
            }
        }
        // ----------------------- BUSCAR CITA POR ID:
        public async Task<ResponseUtils<Appointment>> ListAppointmentByIdAsync(int appointmentId)
        {
            try
            {
                var appointment = await _appointmentRepository.ListAppointmentByIdAsync(appointmentId);
                if(appointment != null)
                {
                    // Retorno de la respuesta éxitosa con la estructura de la clase 'ResponseUtils':
                    return new ResponseUtils<Appointment>(true, null, appointment, 200, message: "¡Cita encontrada!");
                }
                else
                {
                    return new ResponseUtils<Appointment>(false, null, null, 404, message: "¡No hay cita registrada con el parámetro enviado!");
                }
            }
            catch (Exception ex)
            {
                return new ResponseUtils<Appointment>(false, null, null, 422, message: ex.Message);
            }
        }
        // ----------------------- LISTAR CITAS POR FECHA:
        public async Task<ResponseUtils<Appointment>> AppointmentsByDate(DateTime date)
        {
            try
            {
                var Appointments = await _appointmentRepository.AppointmentsByDate(date);
                if(Appointments.Any())
                {
                    // Retorno de la respuesta éxitosa con la estructura de la clase 'ResponseUtils':
                    return new ResponseUtils<Appointment>(true, new List<Appointment>(Appointments), null, 200, message: "¡Listado de citas!");
                }
                else
                {
                    return new ResponseUtils<Appointment>(false, null, null, 404, message: "¡No hay citas agendadas en la fecha indicada!");
                }
            }
            catch (Exception ex)
            {
                return new ResponseUtils<Appointment>(false, null, null, 422, message: ex.Message);
            }
        }
        // ----------------------- LISTAR MASCOTAS DE UN DUEÑO:
        public async Task<ResponseUtils<Pet>> ListOwnerPets(int ownerId)
        {
            try
            {
                var ownerPets = await _appointmentRepository.ListOwnerPets(ownerId);
                if(ownerPets.Any())
                {
                    // Retorno de la respuesta éxitosa con la estructura de la clase 'ResponseUtils':
                    return new ResponseUtils<Pet>(true, new List<Pet>(ownerPets), null, 200, message: "¡Mascotas!");
                }
                else
                {
                    return new ResponseUtils<Pet>(false, null, null, 404, message: "El dueño ingresado no tiene mascotas asociadas!");
                }
            }
            catch (Exception ex)
            {
                return new ResponseUtils<Pet>(false, null, null, 422, message: ex.Message);
            }
        }
        // ----------------------- LISTAR CITAS VETERINARIO:
        public async Task<ResponseUtils<Appointment>> ListAppointmentsVet(int id)
        {
            try
            {
                var Appointments = await _appointmentRepository.ListAppointmentsVet(id);
                if(Appointments.Any())
                {
                    // Retorno de la respuesta éxitosa con la estructura de la clase 'ResponseUtils':
                    return new ResponseUtils<Appointment>(true, new List<Appointment>(Appointments), null, 200, message: "¡Listado de citas!");
                }
                else
                {
                    return new ResponseUtils<Appointment>(false, null, null, 404, message: "¡No hay citas asociadas con el veterinario!");
                }
            }
            catch (Exception ex)
            {
                return new ResponseUtils<Appointment>(false, null, null, 422, message: ex.Message);
            }
        }
    }
}