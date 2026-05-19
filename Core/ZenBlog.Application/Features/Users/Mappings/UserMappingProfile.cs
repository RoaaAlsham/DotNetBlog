

using AutoMapper;
using ZenBlog.Application.Features.Users.Commands;
using ZenBlog.Domain.Entities;
namespace ZenBlog.Application.Features.Users.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<CreateUserCommand, AppUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Id, opt => opt.Ignore())       //Let Identity generate it
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore()); // Not in the command
        }
    }
}
