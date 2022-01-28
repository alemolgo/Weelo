using AutoMapper;
using co_weelo_testproject_common.Dto;
using co_weelo_testproject_dal.ModelData;

namespace co_weelo_testproject_sl.Automapper
{
    public class MappingProfileModule : Profile
    {
        public MappingProfileModule()
        {
            CreateMap<Owner, OwnerDto>().ReverseMap();
            CreateMap<Property, PropertyDto>().ReverseMap();
            CreateMap<PropertyImage, PropertyImageDto>().ReverseMap();
            CreateMap<PropertyTrace, PropertyTraceDto>().ReverseMap();
        }
    }
}
