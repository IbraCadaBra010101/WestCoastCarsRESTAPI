using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
  public class Vehicle
  {
    public int Id { get; set; }
    public string Color { get; set; }
    public string FuelType { get; set; }
    public string GearType { get; set; }
    public int? MakeId { get; set; }
    public int Mileage { get; set; }
    public int? ModelId { get; set; }
    public int ModelYear { get; set; }
    public DateTime? RegistrationDate { get; set; }
    public string RegistrationNumber { get; set; }
    public string VehicleName { get; set; }

    //Navigation Properties
    [ForeignKey("MakeId")]
    public virtual Make Make { get; set; }

    [ForeignKey("ModelId")]
    public virtual VehicleModel Model { get; set; }
  }
}