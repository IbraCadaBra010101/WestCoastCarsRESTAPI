using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interfaces;
using API.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  [ApiController]
  [Route("api/vehicles")]
  public class VehiclesController : ControllerBase
  {
    private readonly IVehicleRepository _vehicleRepo;
    private readonly IMapper _mapper;
    private readonly IManufacturerRepository _makeRepo;
    private readonly IVehicleModelRepository _modelRepo;

    public VehiclesController(IVehicleRepository vehicleRepo,
      IManufacturerRepository makeRepo, IVehicleModelRepository modelRepo, IMapper mapper)
    {
      _modelRepo = modelRepo;
      _makeRepo = makeRepo;
      _mapper = mapper;
      _vehicleRepo = vehicleRepo;
    }

    [HttpGet()]
    public async Task<ActionResult<IEnumerable<VehicleViewModel>>> GetVehicles()
    {
      var result = await _vehicleRepo.GetVehiclesAsync();
      var vehicles = _mapper.Map<IEnumerable<VehicleViewModel>>(result);
      return Ok(vehicles);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VehicleViewModel>> GetVehicle(int id)
    {
      var result = await _vehicleRepo.GetVehicleById(id);
      var vehicle = _mapper.Map<VehicleViewModel>(result);
      return Ok(vehicle);
    }

    [HttpGet("find/{regNo}")]
    public async Task<ActionResult<VehicleViewModel>> FindVehicle(string regNo)
    {
      var result = await _vehicleRepo.GetVehicleByRegNoAsync(regNo);
      var vehicle = _mapper.Map<VehicleViewModel>(result);
      return Ok(vehicle);
    }

    [HttpPost()]
    public async Task<ActionResult> AddVehicle(AddVehicleViewModel model)
    {

      var manufacturer = await _makeRepo.GetManufacturerByName(model.Make);

      if (manufacturer == null) return BadRequest($"Tillverkaren {model.Make} finns ej i systemet");

      var vehicleModel = await _modelRepo.GetModelByName(model.Model);

      if (vehicleModel == null) return BadRequest($"Model beteckning {model.Model} finns ej i systemet");

      var vehicle = new Vehicle
      {
        VehicleName = model.Name,
        RegistrationNumber = model.RegNumber,
        FuelType = model.FuelType,
        GearType = model.GearType,
        Mileage = model.Mileage,
        ModelYear = model.ModelYear,
        MakeId = manufacturer.Id,
        ModelId = vehicleModel.Id
      };

      _vehicleRepo.Add(vehicle);
      if (await _vehicleRepo.SaveAllAsync())
      {
        var newVehicle = _mapper.Map<VehicleViewModel>(vehicle);
        return StatusCode(201, newVehicle);
      }

      return StatusCode(500, "Gick inte att spara fordonet");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteVehicle(int id)
    {
      var vehicle = await _vehicleRepo.GetVehicleById(id);

      if (vehicle == null) return NotFound($"Tyvärr hittade ingen bil med id {id}");

      _vehicleRepo.Delete(vehicle);
      if (await _vehicleRepo.SaveAllAsync()) return NoContent();

      return StatusCode(500, "Gick inte att ta bort fordonet");

    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> UpdateVehicle(int id, UpdateVehicleViewModel model)
    {
      var vehicle = await _vehicleRepo.GetVehicleById(id);

      vehicle.FuelType = model.FuelType;
      vehicle.GearType = model.GearType;
      vehicle.Mileage = model.Mileage;
      vehicle.RegistrationDate = model.RegistrationDate;

      _vehicleRepo.Update(vehicle);
      if (await _vehicleRepo.SaveAllAsync()) return NoContent();

      return StatusCode(500, "Det gick inte att uppdatera fordonet");
    }
  }

}