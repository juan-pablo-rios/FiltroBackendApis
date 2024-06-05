using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Veterinary.Data;
using Veterinary.Models;
using System.Collections.Generic;
using Veterinary.Services.Appointments;
using Veterinary.Utils;

namespace Veterinary.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentServices _appointmentServices;
    public AppointmentsController(IAppointmentServices appointmentServices)
    {
        _appointmentServices = appointmentServices;
    }
    // ----------------------- LISTAR CITAS:
    [HttpGet]
    [Route("ListAppointments")]
    public async Task<ActionResult<ResponseUtils<Appointment>>> ListAppointments()
    {
        var result = await _appointmentServices.ListAppointmentsAsync();
        // Condicional que determina el código HTTP:
        if(result.Code == 200)
        {
            return Ok(result);
        }
        else if(result.Code == 404)
        {
            return StatusCode(404, result);
        }
        else
        {
            return StatusCode(422, result);
        }
    }
    // ----------------------- LISTAR CITA POR ID:
    [HttpGet]
    [Route("ListAppointmentById/{appointmentId}")]
    public async Task<ActionResult<ResponseUtils<Appointment>>> ListAppointmentById(int appointmentId)
    {
        var result = await _appointmentServices.ListAppointmentByIdAsync(appointmentId);
        // Condicional que determina el código HTTP:
        if(result.Code == 200)
        {
            return Ok(result);
        }
        else if(result.Code == 404)
        {
            return StatusCode(404, result);
        }
        else
        {
            return StatusCode(422, result);
        }
    }
    // ----------------------- LISTAR CITAS POR FECHA:
    [HttpGet]
    [Route("AppointmentsByDate/{date}")]
    public async Task<ActionResult<ResponseUtils<Appointment>>> AppointmentsByDate(DateTime date)
    {
        var result = await _appointmentServices.AppointmentsByDate(date);
        // Condicional que determina el código HTTP:
        if(result.Code == 200)
        {
            return Ok(result);
        }
        else if(result.Code == 404)
        {
            return StatusCode(404, result);
        }
        else
        {
            return StatusCode(422, result);
        }
    }
    // ----------------------- LISTAR MASCOTAS DE UN DUEÑO:
    [HttpGet]
    [Route("ListOwnerPets/{ownerId}")]
    public async Task<ActionResult<ResponseUtils<Appointment>>> ListOwnerPets(int ownerId)
    {
        var result = await _appointmentServices.ListOwnerPets(ownerId);
        // Condicional que determina el código HTTP:
        if(result.Code == 200)
        {
            return Ok(result);
        }
        else if(result.Code == 404)
        {
            return StatusCode(404, result);
        }
        else
        {
            return StatusCode(422, result);
        }
    }
    // ----------------------- LISTAR CITAS VETERINARIO:
    [HttpGet]
    [Route("ListAppointmentsVet/{veId}")]
    public async Task<ActionResult<ResponseUtils<Appointment>>> ListAppointmentsVet(int veId)
    {
        var result = await _appointmentServices.ListAppointmentsVet(veId);
        // Condicional que determina el código HTTP:
        if(result.Code == 200)
        {
            return Ok(result);
        }
        else if(result.Code == 404)
        {
            return StatusCode(404, result);
        }
        else
        {
            return StatusCode(422, result);
        }
    }
}