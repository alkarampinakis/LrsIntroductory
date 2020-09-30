using AutoMapper;
using LrsIntroducotryApi.Models;
using LrsIntroducotryApi.Models.Entities.Custom;
using LrsIntroducotryApi.Transfer.DTOs;

namespace LrsIntroducotryApi.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            _ = CreateMap<UserType, UserTypeDTO>()
                    .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                    .ForMember(x => x.Description, y => y.MapFrom(z => z.Description))
                    .ForMember(x => x.TypeCode, y => y.MapFrom(z => z.Code));

            _ = CreateMap<UserTitle, UserTitleDTO>()
                    .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                    .ForMember(x => x.Description, y => y.MapFrom(z => z.Description));

            _ = CreateMap<UserWithTypeTitle, UserWithTypeTitleDTO>();
        }
    }
}
