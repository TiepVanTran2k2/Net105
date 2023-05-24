using Application.Contracts.Dtos.StudentInformation;
using Application.Contracts.Services;
using AutoMapper;
using Domain.Entities.Lab3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Applications
{
    public class StudentInformationService : IStudentInformationService
    {
        private readonly IStudentInformationRepository _iStudentinformationRepository;
        private readonly IMapper _iMapper;
        public StudentInformationService(IStudentInformationRepository studentInformationRepository,
                                         IMapper mapper)
        {
            _iStudentinformationRepository = studentInformationRepository;
            _iMapper = mapper;
        }

        public async Task<bool> CreateAsync(StudentInformationDto input)
        {
            try
            {
                var inputMap = _iMapper.Map<StudentInformation>(input);
                await _iStudentinformationRepository.CreateAsync(inputMap);
                return true;
            }
            catch(Exception ex)
            {
                throw ex.GetBaseException();
            }
        }

        public async Task<List<StudentInformationResponseDto>> GetAllAsync()
        {
            try
            {
                var all = await _iStudentinformationRepository.GetAllAsync();
                var allMapper = _iMapper.Map<List<StudentInformationResponseDto>>(all);
                return allMapper;
            }
            catch(Exception ex)
            {
                throw ex.GetBaseException();
            }
        }

        public string[] GetGenderAsync()
        {
            string[] strings = new string[2];
            strings[0] = "Male";
            strings[1] = "FeMale";
            return strings;
        }
    }
}
