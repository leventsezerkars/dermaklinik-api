using AutoMapper;
using DermaKlinik.API.Application.DTOs.GalleryGroup;
using DermaKlinik.API.Application.DTOs.GalleryImage;
using DermaKlinik.API.Application.DTOs.GalleryImageGroupMap;
using DermaKlinik.API.Core.Entities;

namespace DermaKlinik.API.Application.Mappings
{
    public class GalleryMappingProfile : Profile
    {
        public GalleryMappingProfile()
        {
            CreateMap<GalleryImage, GalleryImageDto>();
            CreateMap<GalleryGroup, GalleryGroupDto>();
            CreateMap<GalleryImageGroupMap, GalleryImageGroupMapDto>();

            CreateMap<CreateGalleryImageDto, GalleryImage>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore()) // ImageUrl artık manuel olarak set ediliyor
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

            CreateMap<CreateGalleryGroupDto, GalleryGroup>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

            CreateMap<CreateGalleryImageGroupMapDto, GalleryImageGroupMap>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

            CreateMap<UpdateGalleryImageDto, GalleryImage>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore()); // ImageUrl artık manuel olarak set ediliyor
            CreateMap<UpdateGalleryGroupDto, GalleryGroup>();
            CreateMap<UpdateGalleryImageGroupMapDto, GalleryImageGroupMap>();
        }
    }
}