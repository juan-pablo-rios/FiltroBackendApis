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
public class CreateAppointmentController : ControllerBase
{
    private readonly IAppointmentServices _appointmentServices;
    public CreateAppointmentController(IAppointmentServices appointmentServices)
    {
        _appointmentServices = appointmentServices;
    }
    // ----------------------- CREAR CITA:
    [HttpPost]
    public async Task<ActionResult<ResponseUtils<Appointment>>> CreateAppointmentAsync([FromBody] Appointment appointment)
    {
        var response = await _appointmentServices.CreateAppointmentAsync(appointment);
        // Condicional que determina el código HTTP:
        if(response.Code == 201)
        {
            return StatusCode(201, response);
        }
        else if(response.Code == 406)
        {
            return StatusCode(406, response);
        }
        else
        {
            return StatusCode(422, response);
        }
    }
}