using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api")]
    public class VehicleMakeController : ControllerBase
    {
        private readonly IManufacturerRepository _makeRepo;
        public VehicleMakeController(IManufacturerRepository makeRepo)
        {
            _makeRepo = makeRepo;
        }

        // GET

        [HttpGet("manufacturers")]
        public async Task<ActionResult<IList<ManufacturerViewModel>>> GetManufacturers()
        {
            return Ok(await _makeRepo.GetManufacturers());
        }



        //ADD
        [HttpPost("manufacturer")]
        public async Task<ActionResult> Add(ManufacturerViewModel model)
        {
            try
            {
                var manufacturer = await _makeRepo.GetManufacturerByName(model.Name);

                if (manufacturer != null) { return BadRequest("Tillverkaren finns redan i systemet!"); }

                var make = new Make
                {
                    Name = model.Name
                };

                _makeRepo.Add(make);

                if (await _makeRepo.SaveAllAsync()) return StatusCode(201, make);

                return StatusCode(500, "Det gick inte att spara ner tillverkaren");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        //GET BY ID

        [HttpPut("manufacturer/{id}")]
        public async Task<ActionResult> Update(int id, ManufacturerViewModel model)
        {
            try
            {
                var manufacturer = await _makeRepo.GetManufacturerById(id);
                if (manufacturer == null) return NotFound($"Kunde inte hitta någon tillverkare med id: {id}");

                manufacturer.Name = model.Name;

                _makeRepo.Update(manufacturer);

                if (await _makeRepo.SaveAllAsync()) return NoContent();

                return StatusCode(500, "Det gick inte att uppdatera tillverkaren");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}