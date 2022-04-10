using AutoMapper;
using DataAccessLayer.Models;

namespace ELibrary.Models.MapProfiles
{
    public class UserMapProfile : Profile
    {
        public UserMapProfile() 
        {
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();

            CreateMap<UserDetailsDto, User>();
            CreateMap<User, UserDetailsDto>();
            
            CreateMap<UserDtoCreate, User>();
            CreateMap<User, UserDtoCreate>();
            
            CreateMap<UserDtoUpdate, User>();
            CreateMap<User, UserDtoUpdate>();

            CreateMap<BookDto, Book>();
            CreateMap<Book, BookDto>();
        }
    }
}