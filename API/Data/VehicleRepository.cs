using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
  public class VehicleRepository : IVehicleRepository
  {
    private readonly DataContext _context;
    public VehicleRepository(DataContext context)
    {
      _context = context;
    }

    public void Add(Vehicle vehicle)
    {
      _context.Entry(vehicle).State = EntityState.Added;
    }

    public void Delete(Vehicle vehicle)
    {
      _context.Entry(vehicle).State = EntityState.Deleted;
    }

    public async Task<Vehicle> GetVehicleById(int id)
    {
      return await _context.Vehicles
        .Include(c => c.Make)
        .Include(c => c.Model)
        .SingleOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Vehicle> GetVehicleByRegNoAsync(string regNo)
    {
      return await _context.Vehicles
      .Include(c => c.Make)
      .Include(c => c.Model)
      //   .Select(vehicle => new VehicleViewModel
      //   {
      //     Id = vehicle.Id,
      //     Color = vehicle.Color,
      //     FuelType = vehicle.FuelType,
      //     GearType = vehicle.GearType,
      //     Mileage = vehicle.Mileage,
      //     ModelYear = vehicle.ModelYear,
      //     RegistrationDate = vehicle.RegistrationDate,
      //     RegistrationNumber = vehicle.RegistrationNumber,
      //     Make = vehicle.Make.Name,
      //     Model = vehicle.Model.Description,
      //     VehicleName = vehicle.VehicleName
      //   })
      .SingleOrDefaultAsync(c => c.RegistrationNumber.ToLower() == regNo.ToLower());
    }

    public async Task<IEnumerable<Vehicle>> GetVehiclesAsync()
    {
      return await _context.Vehicles
        .Include(c => c.Make)
        .Include(c => c.Model)
        .ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }

    public void Update(Vehicle vehicle)
    {
      _context.Entry(vehicle).State = EntityState.Modified;
    }
  }
}