using AutoMapper;
using DermaKlinik.API.Application.DTOs.Blog;
using DermaKlinik.API.Core.Entities;

namespace DermaKlinik.API.Application.Mappings
{
    public class BlogMappingProfile : Profile
    {
        public BlogMappingProfile()
        {
            CreateMap<Blog, BlogDto>();

            CreateMap<BlogTranslation, BlogTranslationDto>();

            CreateMap<CreateBlogDto, Blog>()
                .ForMember(dest => dest.ViewCount, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

            CreateMap<CreateBlogTranslationDto, BlogTranslation>();

            CreateMap<UpdateBlogDto, Blog>();

            CreateMap<UpdateBlogTranslationDto, BlogTranslation>();
        }
    }
}