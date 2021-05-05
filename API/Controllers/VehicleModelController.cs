using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interfaces;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  [ApiController]
  [Route("api/vehiclemodel")]
  public class VehicleModelController : ControllerBase
  {
    private readonly IVehicleModelRepository _modelRepo;
    public VehicleModelController(IVehicleModelRepository modelRepo)
    {
      _modelRepo = modelRepo;
    }

    [HttpGet()]
    public async Task<ActionResult<IList<VehicleModel>>> GetVehicleModels()
    {
      return Ok(await _modelRepo.GetModels());
    }

    [HttpPost()]
    public async Task<ActionResult> Add(VehicleModelViewModel model)
    {
      try
      {
        var result = await _modelRepo.GetModelByName(model.Description);

        if (result != null) return BadRequest("Bilmodellen existerar redan i systemet!");

        var newModel = new VehicleModel
        {
          Description = model.Description
        };

        _modelRepo.Add(newModel);
        if (await _modelRepo.SaveAllAsync()) return StatusCode(201, newModel);
        return StatusCode(500, "Det gick inte att spara ner ny bilmodell");

      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
  }
}