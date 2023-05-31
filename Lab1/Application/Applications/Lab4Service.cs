using Application.Contracts.Dtos.Lab4;
using Application.Contracts.Services;
using AutoMapper;
using Domain.Entities.Lab4;
using Domain.EnumStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Applications
{
    public class Lab4Service : ILab4Service
    {
        public readonly ILab4Repository _iLab4Repository;
        private readonly IMapper _iMapper;
        public Lab4Service(ILab4Repository lab4Repository,
                           IMapper mapper)
        {
            _iLab4Repository = lab4Repository;
            _iMapper = mapper;
        }

        public async Task<bool> CreateAsync(Lab4Dto input)
        {
            var dataCreate = _iMapper.Map<lab4>(input);
            await _iLab4Repository.CreateAsync(dataCreate);
            return true;
        }

        public async Task<List<Lab4Dto>> GetListAsync()
        {
            var listStudent = (await _iLab4Repository.GetAllAsync()).Where(x => x.Status == (int)StatusProductEnum.open).ToList();
            var result = _iMapper.Map<List<Lab4Dto>>(listStudent);
            return result;
        }
    }
}
