using AutoMapper;
using MediStoS.Database.Models;
using MediStoS.DataTransferObjects;

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
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
    }
}
