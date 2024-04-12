using AutoMapper;

namespace ITPlanet.Mapper;

public class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<ITPlanet.Data.Models.User, Models.AccountModel>()
            .ReverseMap();
    }
}