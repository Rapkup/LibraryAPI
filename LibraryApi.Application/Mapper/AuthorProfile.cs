﻿using AutoMapper;
using LibraryApi.Application.Models;
using LibraryApi.Application.Models.DTO_s.Responces;
using LibraryApi.Domain.Models;

namespace LibraryApi.Application.Mapper
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorDTO>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.MiddleName))
               .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday))
               .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
               .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
               .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
               .ReverseMap();

            CreateMap<AuthorCreateRequest, Author>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.MiddleName))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country));

            CreateMap<AuthorUpdateResponce, Author>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
              .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.MiddleName))
              .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday))
              .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country));
        }
    }
}
