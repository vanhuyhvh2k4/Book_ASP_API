using AutoMapper;
using BookManagement.App.Dto;
using BookManagement.App.Models;

namespace BookManagement.App.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Reader, ReaderDto>();
            CreateMap<ReaderDto, Reader>();

            CreateMap<Book, BookDto>();
            CreateMap<BookDto, Book>();
        }
    }
}
