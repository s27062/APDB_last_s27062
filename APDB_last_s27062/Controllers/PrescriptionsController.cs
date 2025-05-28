using APDB_last_s27062.DTOs;
using APDB_last_s27062.Models;
using APDB_last_s27062.Services;
using Microsoft.AspNetCore.Mvc;

namespace APDB_last_s27062.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionsController(IDbService dbService) : ControllerBase
{



    [HttpGet]
    public async Task<IActionResult> GetPrescriptionsAsync()
    {
        return Ok(await dbService.GetPrescriptionsAsync());
    }

    [HttpPost]
    public async Task<IActionResult> AddPrescriptionAsync([FromBody] PrescriptionAddDTO prescription)
    {
        return Ok(await dbService.PrescriptionAddAsync(prescription));
    }
}