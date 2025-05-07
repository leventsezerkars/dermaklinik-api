using DermaKlinik.API.Core.Models;
using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DermaKlinik.API.Application.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MenuService(IMenuRepository menuRepository, IUnitOfWork unitOfWork)
        {
            _menuRepository = menuRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedList<Menu>> GetAllMenusAsync(int pageNumber, int pageSize)
        {
            var query = _menuRepository.GetAll();
            return await PaginatedList<Menu>.CreateAsync(query, pageNumber, pageSize);
        }

        public async Task<IEnumerable<Menu>> GetRootMenusAsync()
        {
            return await _menuRepository.GetRootMenusAsync();
        }

        public async Task<IEnumerable<Menu>> GetActiveMenusAsync()
        {
            return await _menuRepository.GetActiveMenusAsync();
        }

        public async Task<Menu?> GetMenuByIdAsync(Guid id)
        {
            return await _menuRepository.GetByIdAsync(id);
        }

        public async Task<Menu?> GetMenuWithChildrenAsync(Guid id)
        {
            return await _menuRepository.GetMenuWithChildrenAsync(id);
        }

        public async Task<IEnumerable<Menu>> GetMenusByPermissionAsync(string permission)
        {
            return await _menuRepository.GetMenusByPermissionAsync(permission);
        }

        public async Task<Menu> CreateMenuAsync(Menu menu)
        {
            await _menuRepository.AddAsync(menu);
            await _unitOfWork.CompleteAsync();
            return menu;
        }

        public async Task UpdateMenuAsync(Menu menu)
        {
            var existingMenu = await _menuRepository.GetByIdAsync(menu.Id);
            if (existingMenu == null)
                throw new KeyNotFoundException($"Menu with ID {menu.Id} not found.");

            _menuRepository.Update(menu);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteMenuAsync(Guid id)
        {
            var menu = await _menuRepository.GetByIdAsync(id);
            if (menu == null)
                throw new KeyNotFoundException($"Menu with ID {id} not found.");

            menu.IsDeleted = true;
            _menuRepository.Update(menu);
            await _unitOfWork.CompleteAsync();
        }

        public async Task HardDeleteMenuAsync(Guid id)
        {
            var menu = await _menuRepository.GetByIdAsync(id);
            if (menu == null)
                throw new KeyNotFoundException($"Menu with ID {id} not found.");

            _menuRepository.HardDelete(menu);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> HasChildMenusAsync(Guid parentId)
        {
            return await _menuRepository.GetAll().AnyAsync(m => m.ParentId == parentId && !m.IsDeleted);
        }
    }
} 