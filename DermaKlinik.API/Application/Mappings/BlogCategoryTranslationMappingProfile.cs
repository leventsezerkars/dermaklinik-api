using AutoMapper;
using DermaKlinik.API.Application.DTOs.BlogCategory;
using DermaKlinik.API.Core.Entities;

namespace DermaKlinik.API.Application.Mappings
{
    public class BlogCategoryTranslationMappingProfile : Profile
    {
        public BlogCategoryTranslationMappingProfile()
        {
            CreateMap<BlogCategory, BlogCategoryDto>()
                .ForMember(dest => dest.Translations, opt => opt.MapFrom(src => src.Translations));

            CreateMap<BlogCategoryTranslation, BlogCategoryTranslationDto>();

            CreateMap<CreateBlogCategoryDto, BlogCategory>();

            CreateMap<CreateBlogCategoryTranslationDto, BlogCategoryTranslation>();

            CreateMap<UpdateBlogCategoryDto, BlogCategory>();

            CreateMap<UpdateBlogCategoryTranslationDto, BlogCategoryTranslation>();
        }
    }
}