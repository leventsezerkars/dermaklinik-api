using AutoMapper;
using DermaKlinik.API.Application.DTOs.CompanyInfo;
using DermaKlinik.API.Application.Models.FilterModels;
using DermaKlinik.API.Core.Interfaces;
using DermaKlinik.API.Core.Models;
using DermaKlinik.API.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DermaKlinik.API.Application.Services
{
    public class CompanyInfoService(IUnitOfWork unitOfWork, IMapper mapper, ICompanyInfoRepository companyInfoRepository) : ICompanyInfoService
    {
        public async Task<CompanyInfoDto> GetByIdAsync(Guid id)
        {
            var companyInfo = await companyInfoRepository.GetByIdAsync(id);
            if (companyInfo == null)
            {
                throw new KeyNotFoundException($"CompanyInfo with ID {id} not found.");
            }
            return mapper.Map<CompanyInfoDto>(companyInfo);
        }

        public async Task<List<CompanyInfoDto>> GetAllAsync()
        {
            var companyInfos = await companyInfoRepository.GetAll()
                .Where(c => !c.IsDeleted)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
            return mapper.Map<List<CompanyInfoDto>>(companyInfos);
        }

        public async Task<List<CompanyInfoDto>> GetAllAsync(PagingRequestModel request, CompanyInfoFilter filters)
        {
            Expression<Func<Core.Entities.CompanyInfo, bool>>? filterExpression = null;

            if (filters != null)
            {
                filterExpression = c => !c.IsDeleted &&
                    (string.IsNullOrEmpty(filters.Name) || c.Name.Contains(filters.Name)) &&
                    (string.IsNullOrEmpty(filters.Email) || c.Email.Contains(filters.Email)) &&
                    (string.IsNullOrEmpty(filters.Phone) || c.Phone.Contains(filters.Phone)) &&
                    (!filters.IsActive.HasValue || c.IsActive == filters.IsActive) &&
                    (!filters.CreatedDateFrom.HasValue || c.CreatedAt >= filters.CreatedDateFrom) &&
                    (!filters.CreatedDateTo.HasValue || c.CreatedAt <= filters.CreatedDateTo);
            }

            var companyInfos = await companyInfoRepository.GetAll(filterExpression, request)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            return mapper.Map<List<CompanyInfoDto>>(companyInfos);
        }

        public async Task<List<CompanyInfoDto>> GetActiveAsync()
        {
            var companyInfos = await companyInfoRepository.GetAll()
                .Where(c => c.IsActive && !c.IsDeleted)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
            return mapper.Map<List<CompanyInfoDto>>(companyInfos);
        }

        public async Task<CompanyInfoDto?> GetActiveCompanyInfoAsync()
        {
            var companyInfo = await companyInfoRepository.GetActiveCompanyInfoAsync();
            return mapper.Map<CompanyInfoDto>(companyInfo);
        }

        public async Task<CompanyInfoDto> CreateAsync(CreateCompanyInfoDto createCompanyInfoDto)
        {
            // Validation
            if (!await companyInfoRepository.IsNameUniqueAsync(createCompanyInfoDto.Name))
            {
                throw new InvalidOperationException($"Company with name '{createCompanyInfoDto.Name}' already exists.");
            }

            if (!await companyInfoRepository.IsEmailUniqueAsync(createCompanyInfoDto.Email))
            {
                throw new InvalidOperationException($"Company with email '{createCompanyInfoDto.Email}' already exists.");
            }

            var companyInfo = mapper.Map<Core.Entities.CompanyInfo>(createCompanyInfoDto);
            companyInfo.IsActive = true;
            
            await companyInfoRepository.AddAsync(companyInfo);
            await unitOfWork.CompleteAsync();
            
            return mapper.Map<CompanyInfoDto>(companyInfo);
        }

        public async Task<CompanyInfoDto> UpdateAsync(Guid id, UpdateCompanyInfoDto updateCompanyInfoDto)
        {
            var companyInfo = await companyInfoRepository.GetByIdAsync(id);
            if (companyInfo == null)
            {
                throw new KeyNotFoundException($"CompanyInfo with ID {id} not found.");
            }

            // Validation
            if (!await companyInfoRepository.IsNameUniqueAsync(updateCompanyInfoDto.Name, id))
            {
                throw new InvalidOperationException($"Company with name '{updateCompanyInfoDto.Name}' already exists.");
            }

            if (!await companyInfoRepository.IsEmailUniqueAsync(updateCompanyInfoDto.Email, id))
            {
                throw new InvalidOperationException($"Company with email '{updateCompanyInfoDto.Email}' already exists.");
            }

            mapper.Map(updateCompanyInfoDto, companyInfo);
            companyInfoRepository.Update(companyInfo);
            await unitOfWork.CompleteAsync();
            
            return mapper.Map<CompanyInfoDto>(companyInfo);
        }

        public async Task DeleteAsync(Guid id)
        {
            var companyInfo = await companyInfoRepository.GetByIdAsync(id);
            if (companyInfo == null)
            {
                throw new KeyNotFoundException($"CompanyInfo with ID {id} not found.");
            }

            companyInfoRepository.SoftDelete(companyInfo);
            await unitOfWork.CompleteAsync();
        }

        public async Task HardDeleteAsync(Guid id)
        {
            var companyInfo = await companyInfoRepository.GetByIdAsync(id);
            if (companyInfo == null)
            {
                throw new KeyNotFoundException($"CompanyInfo with ID {id} not found.");
            }

            companyInfoRepository.HardDelete(companyInfo);
            await unitOfWork.CompleteAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await companyInfoRepository.ExistsAsync(id);
        }

        public async Task<bool> IsNameUniqueAsync(string name, Guid? excludeId = null)
        {
            return await companyInfoRepository.IsNameUniqueAsync(name, excludeId);
        }

        public async Task<bool> IsEmailUniqueAsync(string email, Guid? excludeId = null)
        {
            return await companyInfoRepository.IsEmailUniqueAsync(email, excludeId);
        }
    }
}