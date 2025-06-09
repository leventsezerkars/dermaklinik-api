using AutoMapper;
using DermaKlinik.API.Application.DTOs.CompanyInfo;
using DermaKlinik.API.Core.Interfaces;
using DermaKlinik.API.Infrastructure.Repositories;

namespace DermaKlinik.API.Application.Services
{
    public class CompanyInfoService(IUnitOfWork unitOfWork, IMapper mapper, ICompanyInfoRepository companyInfoRepository) : ICompanyInfoService
    {
        public async Task<CompanyInfoDto> GetByIdAsync(Guid id)
        {
            var companyInfo = await companyInfoRepository.GetByIdAsync(id);
            return mapper.Map<CompanyInfoDto>(companyInfo);
        }

        public async Task<IEnumerable<CompanyInfoDto>> GetAllAsync()
        {
            var companyInfos = companyInfoRepository.GetAll();
            return mapper.Map<IEnumerable<CompanyInfoDto>>(companyInfos);
        }

        public async Task<CompanyInfoDto> CreateAsync(CreateCompanyInfoDto createCompanyInfoDto)
        {
            var companyInfo = mapper.Map<Core.Entities.CompanyInfo>(createCompanyInfoDto);
            await companyInfoRepository.AddAsync(companyInfo);
            await unitOfWork.CompleteAsync();
            return mapper.Map<CompanyInfoDto>(companyInfo);
        }

        public async Task<CompanyInfoDto> UpdateAsync(Guid id, UpdateCompanyInfoDto updateCompanyInfoDto)
        {
            var companyInfo = await companyInfoRepository.GetByIdAsync(id);
            if (companyInfo == null)
            {
                return null;
            }

            mapper.Map(updateCompanyInfoDto, companyInfo);
            companyInfoRepository.Update(companyInfo);
            await unitOfWork.CompleteAsync();
            return mapper.Map<CompanyInfoDto>(companyInfo);
        }

        public async Task DeleteAsync(Guid id)
        {
            var companyInfo = await companyInfoRepository.GetByIdAsync(id);
            if (companyInfo != null)
            {
                companyInfoRepository.SoftDelete(companyInfo);
                await unitOfWork.CompleteAsync();
            }
        }
    }
}