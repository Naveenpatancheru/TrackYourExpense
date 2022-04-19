using AutoMapper;
using TrackYourExpenseApi.Entities;
using TrackYourExpenseApi.Models;
namespace TrackYourExpenseApi.Profiles
{
    public class AuthenticateUserProfile : Profile
    {
        public AuthenticateUserProfile()
        {
            CreateMap<User, AuthenticateRequestDto>();
            CreateMap<User, AuthenticateResponseDto>();
            CreateMap<RegisterUserDto, User>();
            CreateMap<UpdateUserDto, User>();
        }

    }
}
