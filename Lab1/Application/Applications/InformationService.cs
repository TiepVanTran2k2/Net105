using Application.Contracts.Dtos.Information;
using Application.Contracts.Services;
using Application.ProfileMapper;
using AutoMapper;
using Domain.Entities.Information;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Applications
{
    public class InformationService : IInformationService
    {
        private readonly IInformationRepository _informationRepository;
        private readonly IMapper _autoMapper;
        private readonly IHelperService _helperService;
        public InformationService(IInformationRepository informationRepository,
                                  IMapper profileMapper,
                                  IHelperService helperService)
        {

            _informationRepository = informationRepository;
            _autoMapper = profileMapper;
            _helperService = helperService;
        }

        public async Task<InformationDto> GetAsync(Guid id)
        {
            try
            {
                var information = await _informationRepository.GetByIdAsync(id);
                if (information == null)
                {
                    throw new Exception("Information not exist");
                }
                return _autoMapper.Map<InformationDto>(information);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<InformationDto>> GetAllAsync()
        {
            try
            {
                var listInformation = await _informationRepository.GetAllAsync();
                return _autoMapper.Map<List<InformationDto>>(listInformation);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<InformationInsertDto> CreateAsync(InformationInsertDto dto)
        {
            try
            {
                var information = _autoMapper.Map<Information>(dto);
                await _informationRepository.CreateAsync(information);
                return dto;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<InformationDto> DeleteAsync(Guid id)
        {
            try
            {
                var result = await _informationRepository.DeleteAsync(id);
                return _autoMapper.Map<InformationDto>(result);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<InformationDto> EditAsync(InformationDto informationDto)
        {
            try
            {
                var information = _autoMapper.Map<Information>(informationDto);
                var result = await _informationRepository.UpdateAsync(information);
                return informationDto;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
