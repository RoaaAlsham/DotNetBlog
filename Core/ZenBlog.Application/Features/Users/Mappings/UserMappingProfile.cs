

using AutoMapper;
using ZenBlog.Domain.Entities
namespace ZenBlog.Application.Features.Users.Mappings
{
    public class UserMappingProfile: Profile
    {
        public UserMappingProfile() { 
            CreateMap<AppUser, Commands.CreateUserCommand>().ReverseMap();
            //CreateMap<AppUser, Commands.UpdateUserCommand>().ReverseMap();
            //CreateMap<AppUser, Results.GetUserByIdQueryResult>().ReverseMap();
            //CreateMap<AppUser, Results.GetUserQueryResult>().ReverseMap();
        }
    }
}
