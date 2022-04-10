using AutoMapper;
using DataAccessLayer.Models;

namespace ELibrary.Models.MapProfiles
{
    public class BookMapProfile : Profile
    {
        public BookMapProfile()
        {
            CreateMap<BookDto, Book>();
            CreateMap<Book, BookDto>();
            
            CreateMap<BookDetailsDto, Book>();
            CreateMap<Book, BookDetailsDto>();
            
            CreateMap<BookDtoCreate, Book>();
            CreateMap<Book, BookDtoCreate>();
            
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}