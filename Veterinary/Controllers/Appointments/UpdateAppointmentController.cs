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
public class UpdateAppointmentController : ControllerBase
{
    private readonly IAppointmentServices _appointmentServices;
    public UpdateAppointmentController(IAppointmentServices appointmentServices)
    {
        _appointmentServices = appointmentServices;
    }
    // ----------------------- EDITAR CITA:
    [HttpPut]
    public async Task<ActionResult<ResponseUtils<Appointment>>> UpdateAppointmentAsync([FromBody] Appointment appointment)
    {
        var response = await _appointmentServices.CreateAppointmentAsync(appointment);
        // Condicional que determina el código HTTP:
        if(response.Code == 200)
        {
            return StatusCode(200, response);
        }
        else if(response.Code == 404)
        {
            return StatusCode(404, response);
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
