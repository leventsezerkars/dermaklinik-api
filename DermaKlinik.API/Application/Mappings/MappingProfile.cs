using AutoMapper;
using DermaKlinik.API.Application.DTOs.Blog;
using DermaKlinik.API.Application.DTOs.BlogCategory;
using DermaKlinik.API.Application.DTOs.CompanyInfo;
using DermaKlinik.API.Application.DTOs.GalleryGroup;
using DermaKlinik.API.Application.DTOs.GalleryImage;
using DermaKlinik.API.Application.DTOs.GalleryImageGroupMap;
using DermaKlinik.API.Application.DTOs.Language;
using DermaKlinik.API.Application.DTOs.Menu;
using DermaKlinik.API.Application.DTOs.User;
using DermaKlinik.API.Core.Entities;

namespace DermaKlinik.API.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Menu Mappings
            CreateMap<Menu, MenuDto>()
                .ForMember(dest => dest.Translations, opt => opt.MapFrom(src => src.Translations))
                .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.Children));

            CreateMap<CreateMenuDto, Menu>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ForMember(dest => dest.Children, opt => opt.Ignore());

            CreateMap<UpdateMenuDto, Menu>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ForMember(dest => dest.Children, opt => opt.Ignore());

            // MenuTranslation Mappings
            CreateMap<MenuTranslation, MenuTranslationDto>()
                .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Language));
                
            CreateMap<CreateMenuTranslationDto, MenuTranslation>();
            CreateMap<UpdateMenuTranslationDto, MenuTranslation>();

            // Language Mappings
            CreateMap<Language, LanguageDto>();
            CreateMap<CreateLanguageDto, Language>();
            CreateMap<UpdateLanguageDto, Language>();

            // CompanyInfo Mappings
            CreateMap<CompanyInfo, CompanyInfoDto>();
            CreateMap<CreateCompanyInfoDto, CompanyInfo>();
            CreateMap<UpdateCompanyInfoDto, CompanyInfo>();

            // BlogCategory Mappings
            CreateMap<BlogCategory, BlogCategoryDto>()
                .ForMember(dest => dest.Translations, opt => opt.MapFrom(src => src.Translations))
                .ForMember(dest => dest.Blogs, opt => opt.MapFrom(src => src.Blogs));

            CreateMap<CreateBlogCategoryDto, BlogCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ForMember(dest => dest.Blogs, opt => opt.Ignore());

            CreateMap<UpdateBlogCategoryDto, BlogCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ForMember(dest => dest.Blogs, opt => opt.Ignore());

            // BlogCategoryTranslation Mappings
            CreateMap<BlogCategoryTranslation, BlogCategoryTranslationDto>();
            CreateMap<CreateBlogCategoryTranslationDto, BlogCategoryTranslation>();
            CreateMap<UpdateBlogCategoryTranslationDto, BlogCategoryTranslation>();

            // Blog Mappings
            CreateMap<Blog, BlogDto>()
                .ForMember(dest => dest.Translations, opt => opt.MapFrom(src => src.Translations));

            CreateMap<CreateBlogDto, Blog>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore());

            CreateMap<UpdateBlogDto, Blog>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore());

            // BlogTranslation Mappings
            CreateMap<BlogTranslation, BlogTranslationDto>();
            CreateMap<CreateBlogTranslationDto, BlogTranslation>();
            CreateMap<UpdateBlogTranslationDto, BlogTranslation>();

            //// GalleryImage Mappings
            //CreateMap<GalleryImage, GalleryImageDto>()
            //    .ForMember(dest => dest.Groups, opt => opt.MapFrom(src => src.GalleryImageGroupMaps.Select(x => x.GalleryGroup)));

            //CreateMap<CreateGalleryImageDto, GalleryImage>()
            //    .ForMember(dest => dest.GalleryImageGroupMaps, opt => opt.Ignore());

            //CreateMap<UpdateGalleryImageDto, GalleryImage>()
            //    .ForMember(dest => dest.GalleryImageGroupMaps, opt => opt.Ignore());

            //// GalleryGroup Mappings
            //CreateMap<GalleryGroup, GalleryGroupDto>()
            //    .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.GalleryImageGroupMaps.Select(x => x.GalleryImage)));

            //CreateMap<CreateGalleryGroupDto, GalleryGroup>()
            //    .ForMember(dest => dest.GalleryImageGroupMaps, opt => opt.Ignore());

            //CreateMap<UpdateGalleryGroupDto, GalleryGroup>()
            //    .ForMember(dest => dest.GalleryImageGroupMaps, opt => opt.Ignore());

            // User Mappings
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt));

            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) // Password hash'i ayrıca işlenecek
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => "User")) // Varsayılan rol
                .ForMember(dest => dest.LastLoginDate, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            CreateMap<UpdateUserDto, User>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.LastLoginDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
        }
    }
}