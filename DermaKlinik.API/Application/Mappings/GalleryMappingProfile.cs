using AutoMapper;
using DermaKlinik.API.Application.DTOs.GalleryGroup;
using DermaKlinik.API.Application.DTOs.GalleryImage;
using DermaKlinik.API.Core.Entities;

namespace DermaKlinik.API.Application.Mappings
{
    public class GalleryMappingProfile : Profile
    {
        public GalleryMappingProfile()
        {
            CreateMap<GalleryImage, GalleryImageDto>();
            CreateMap<GalleryGroup, GalleryGroupDto>();

            CreateMap<CreateGalleryImageDto, GalleryImage>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

            CreateMap<CreateGalleryGroupDto, GalleryGroup>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

            CreateMap<UpdateGalleryImageDto, GalleryImage>();
            CreateMap<UpdateGalleryGroupDto, GalleryGroup>();
        }
    }
}