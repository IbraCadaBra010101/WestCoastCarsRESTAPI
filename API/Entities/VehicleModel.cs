using System.Collections.Generic;

namespace API.Entities
{
  public class VehicleModel
  {
    public int Id { get; set; }
    public string Description { get; set; }

    //Navigerings egenskap
    public ICollection<Vehicle> Vehicles { get; set; }
  }
}