using AutoMapper;
using DermaKlinik.API.Application.DTOs.Menu;
using DermaKlinik.API.Application.Models.FilterModels;
using DermaKlinik.API.Core.Extensions;
using DermaKlinik.API.Core.Interfaces;
using DermaKlinik.API.Core.Models;
using DermaKlinik.API.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DermaKlinik.API.Application.Services.Menu
{
    public class MenuService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IMenuRepository menuRepository,
        IMenuTranslationRepository menuTranslationRepository) : IMenuService
    {
        public async Task<List<MenuDto>> GetAllAsync(PagingRequestModel request, MenuFilter filters)
        {
            var menus = menuRepository.Query()
                .Include(m => m.Children)
                .Include(m => m.Translations).ThenInclude(t => t.Language)
                .Where(s =>
                    s.Translations.Any(t => t.Language.Code == request.LanguageCode)
                    && s.IsActive == true
                    && s.IsDeleted == false
                    && (string.IsNullOrEmpty(filters.Slug) || s.Translations.Any(t => t.Slug.Contains(filters.Slug)))
                    && (string.IsNullOrEmpty(filters.Name) || s.Translations.Any(t => t.Title.Contains(filters.Name)))
                );

            menus = menus.OrderByDynamic(request.OrderBy, request.Direction);
            menus = menus.Paging(request.Page, request.Take);
            var data = await menus.ToListAsync();
            return mapper.Map<List<MenuDto>>(data);
        }

        public async Task<MenuDto> GetByIdAsync(Guid id)
        {
            var menu = await menuRepository.GetByIdAsync(id);
            return mapper.Map<MenuDto>(menu);
        }

        public async Task<MenuDto> GetBySlugAsync(string slug)
        {
            var menu = menuRepository.Query().FirstOrDefault(m => m.Slug == slug);

            if (menu == null)
            {
                throw new KeyNotFoundException($"Menu with slug {slug} not found.");
            }

            return mapper.Map<MenuDto>(menu);
        }

        public async Task<List<MenuDto>> GetByParentIdAsync(Guid? parentId)
        {
            var menus = await menuRepository.Query()
                .Where(m => m.ParentId == parentId)
                .ToListAsync();

            return mapper.Map<List<MenuDto>>(menus);
        }

        public async Task<MenuDto> CreateAsync(CreateMenuDto createMenuDto)
        {
            var menu = mapper.Map<Core.Entities.Menu>(createMenuDto);
            await menuRepository.AddAsync(menu);
            await unitOfWork.CompleteAsync();
            return mapper.Map<MenuDto>(menu);
        }

        public async Task<MenuTranslationDto> CreateTranslationAsync(CreateMenuTranslationDto createMenuDto)
        {
            var menu = mapper.Map<Core.Entities.MenuTranslation>(createMenuDto);
            await menuTranslationRepository.AddAsync(menu);
            await unitOfWork.CompleteAsync();

            return mapper.Map<MenuTranslationDto>(menu);
        }

        public async Task<MenuDto> UpdateAsync(Guid id, UpdateMenuDto updateMenuDto)
        {
            var menu = await menuRepository.GetByIdAsync(id);
            if (menu == null)
            {
                return null;
            }

            mapper.Map(updateMenuDto, menu);
            menuRepository.Update(menu);
            await unitOfWork.CompleteAsync();
            return mapper.Map<MenuDto>(menu);
        }

        public async Task<MenuTranslationDto> UpdateTranslationAsync(Guid id, UpdateMenuTranslationDto updateMenuDto)
        {
            var menuTranslation = await menuTranslationRepository.GetByIdAsync(id);
            if (menuTranslation == null)
            {
                return null;
            }

            mapper.Map(updateMenuDto, menuTranslation);
            menuTranslationRepository.Update(menuTranslation);
            await unitOfWork.CompleteAsync();
            return mapper.Map<MenuTranslationDto>(menuTranslation);
        }

        public async Task DeleteAsync(Guid id)
        {
            var menu = await menuRepository.GetByIdAsync(id);
            if (menu != null)
            {
                menuRepository.SoftDelete(menu);
                await unitOfWork.CompleteAsync();
            }
        }

        public async Task HardDeleteAsync(Guid id)
        {
            var menu = await menuRepository.GetByIdAsync(id);
            if (menu != null)
            {
                menuRepository.HardDelete(menu);
                await unitOfWork.CompleteAsync();
            }
        }
    }
}