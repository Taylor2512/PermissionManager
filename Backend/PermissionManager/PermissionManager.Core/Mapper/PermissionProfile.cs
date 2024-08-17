using AutoMapper;

using PermissionManager.Core.Services.Dtos;
using PermissionManager.Core.Services.Request;

namespace PermissionManager.Core.Mapper
{
    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            CreateMap<Permission, PermissionDto>()
                .ForMember(e => e.PermissionTypeName, y => y.MapFrom(y => y.PermissionType.Name))
                .ReverseMap();
            CreateMap<Permission, PermissionRequest>().ReverseMap();
            CreateMap<PermissionRequest, PermissionDto>().ReverseMap();
            CreateMap<PermissionType, PermissionTypeRequest>().ReverseMap();
            CreateMap<PermissionType, PermissionTypeDto>().ReverseMap();
            CreateMap<PermissionTypeRequest, PermissionTypeDto>().ReverseMap();
        }
    }
}