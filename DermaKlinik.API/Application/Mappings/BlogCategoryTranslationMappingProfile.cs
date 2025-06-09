using AutoMapper;
using DermaKlinik.API.Application.DTOs.BlogCategory;
using DermaKlinik.API.Core.Entities;

namespace DermaKlinik.API.Application.Mappings
{
    public class BlogCategoryTranslationMappingProfile : Profile
    {
        public BlogCategoryTranslationMappingProfile()
        {
            CreateMap<BlogCategoryTranslation, BlogCategoryTranslationDto>();
            CreateMap<CreateBlogCategoryTranslationDto, BlogCategoryTranslation>();
            CreateMap<UpdateBlogCategoryTranslationDto, BlogCategoryTranslation>();
        }
    }
}