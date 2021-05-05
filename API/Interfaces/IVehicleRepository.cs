using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;
using API.ViewModels;

namespace API.Interfaces
{
  public interface IVehicleRepository
  {
    void Add(Vehicle vehicle);
    Task<IEnumerable<Vehicle>> GetVehiclesAsync();
    Task<Vehicle> GetVehicleByRegNoAsync(string regNo);
    Task<Vehicle> GetVehicleById(int id);
    Task<bool> SaveAllAsync();
    void Delete(Vehicle vehicle);
    void Update(Vehicle vehicle);
  }
}