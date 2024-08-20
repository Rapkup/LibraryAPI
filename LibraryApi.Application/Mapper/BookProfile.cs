using AutoMapper;
using LibraryApi.Application.Models;
using LibraryApi.Application.Models.DTO_s.Responces;
using LibraryApi.Domain.Models;

namespace LibraryApi.Application.Mapper
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDTO>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.ISBN))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.AuthorName))
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
                .ForMember(dest => dest.TakenBy, opt => opt.MapFrom(src => src.TakenBy))
                .ForMember(dest => dest.TakenAt, opt => opt.MapFrom(src => src.TakenAt))
                .ForMember(dest => dest.ShouldBeReturnedAt, opt => opt.MapFrom(src => src.ShouldBeReturnedAt))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ReverseMap();

            CreateMap<BookCreateResponse, Book>()
               .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.ISBN))
               .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
               .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
               .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.AuthorName))
               .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
               .ForMember(dest => dest.TakenBy, opt => opt.MapFrom(src => src.TakenBy))
               .ForMember(dest => dest.TakenAt, opt => opt.MapFrom(src => src.TakenAt))
               .ForMember(dest => dest.ShouldBeReturnedAt, opt => opt.MapFrom(src => src.ShouldBeReturnedAt));

              CreateMap<BookUpdateResponce, Book>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.ISBN))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.AuthorName))
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
                .ForMember(dest => dest.TakenBy, opt => opt.MapFrom(src => src.TakenBy))
                .ForMember(dest => dest.TakenAt, opt => opt.MapFrom(src => src.TakenAt))
                .ForMember(dest => dest.ShouldBeReturnedAt, opt => opt.MapFrom(src => src.ShouldBeReturnedAt));
        }
    }
}
