using System.Collections.Generic;

namespace API.Entities
{
  public class Make
  {
    public int Id { get; set; }
    public string Name { get; set; }

    //Navigation Properties
    public virtual ICollection<Vehicle> Vehicles { get; set; }
  }
}