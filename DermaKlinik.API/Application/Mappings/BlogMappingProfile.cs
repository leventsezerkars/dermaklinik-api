using AutoMapper;
using DermaKlinik.API.Application.DTOs.Blog;
using DermaKlinik.API.Application.DTOs.BlogCategory;
using DermaKlinik.API.Core.Entities;

namespace DermaKlinik.API.Application.Mappings
{
    public class BlogMappingProfile : Profile
    {
        public BlogMappingProfile()
        {
            CreateMap<Blog, BlogDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Translations.FirstOrDefault().Name))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

            CreateMap<BlogTranslation, BlogTranslationDto>()
                .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Language));

            CreateMap<CreateBlogDto, Blog>()
                .ForMember(dest => dest.ViewCount, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

            CreateMap<CreateBlogTranslationDto, BlogTranslation>();

            CreateMap<UpdateBlogDto, Blog>();

            CreateMap<UpdateBlogTranslationDto, BlogTranslation>();
        }
    }
}