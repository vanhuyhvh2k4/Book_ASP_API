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

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<Bill, BillDto>();
            CreateMap<BillDto, Bill>();

            CreateMap<BillDetail, BillDetailDto>();
            CreateMap<BillDetailDto, BillDetail>();
        }
    }
}
