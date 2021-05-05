using API.Entities;
using API.ViewModels;
using AutoMapper;

namespace API.Helpers
{
  public class AutoMapperProfile : Profile
  {
    public AutoMapperProfile()
    {
      CreateMap<Vehicle, VehicleViewModel>()
        .ForMember(dest => dest.RegNumber, opt => opt.MapFrom(src => src.RegistrationNumber))
        .ForMember(dest => dest.Make, opt => opt.MapFrom(src => src.Make.Name))
        .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model.Description));

      CreateMap<AddVehicleViewModel, Vehicle>();

      //CreateMap<Make, ManufacturerViewModel>();
    }
  }
}