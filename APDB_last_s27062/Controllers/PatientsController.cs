using APDB_last_s27062.DTOs;
using APDB_last_s27062.Models;
using APDB_last_s27062.Services;
using Microsoft.AspNetCore.Mvc;

namespace APDB_last_s27062.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController(IDbService dbService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPatientsAsync()
    {
        return Ok(await dbService.GetPatientsAsync());
    }
}