using AutoMapper;
using MediStoS.Database.Models;
using MediStoS.DataTransferObjects;
using MediStoS.Enums;

namespace MediStoS.Helpers;

public class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<BatchCreateDto, Batch>();
        CreateMap<Batch, BatchDto>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));
        CreateMap<MedicineCreateDto, Medicine>();
        CreateMap<MedicineDto, Medicine>();
        CreateMap<Medicine, MedicineDto>();
        CreateMap<WarehouseCreateDto, Warehouse>();
        CreateMap<WarehouseDto, Warehouse>();
        CreateMap<Warehouse, WarehouseDto>();
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));
        CreateMap<UserDto, User>()
            .ForMember(a => a.Role, opt => opt.MapFrom(src => Enum.Parse<Roles>(src.Role)));

        CreateMap<Sensor, SensorDto>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));

        CreateMap<SensorDto, Sensor>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<SensorType>(src.Type)));
    }
}
