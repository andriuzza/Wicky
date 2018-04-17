using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SoftwareHouse.Contract.Common;
using SoftwareHouse.Contract.DataContracts;
using SoftwareHouse.Contract.Repositories;
using SoftwareHouse.Contract.Services;
using System.Linq;

namespace SoftwareHouse.Services.Services.UsersInformation
{
    public class ExperiancesService : IExperiancesService
    {
        private readonly IExperiancesRepository _experiancesRepository;

        public ExperiancesService(IExperiancesRepository experiancesRepository)
        {
            _experiancesRepository = experiancesRepository;
        }

        public async Task<IEnumerable<ExperianceDto>> GetAll()
        {
            var result = await _experiancesRepository.GetAll();

            return !result.Any() ? null : result;
        }

        public async Task<CommonResult> Add(ExperianceDto experianceDto)
        {
            if (await CheckIfExperianceAlreadyExist(experianceDto.UserId, experianceDto.ExperianceType))
            {
                return CommonResult.Failure("This experiance User already has");
            }

            await _experiancesRepository.Add(experianceDto);

            return CommonResult.Success();
        }

        public async Task<bool> CheckIfExperianceAlreadyExist(string userId, ExperianceType type)
        {
            return await _experiancesRepository.CheckIfExperianceAlreadyExist(userId, type);
        }

        public async Task Delete(int id)
        {
            await _experiancesRepository.Delete(id);

        }

        public async Task<CommonResult<ExperianceDto>> GetById(int id)
        {
            var person = await _experiancesRepository.GetById(id);

            if (person == null)
            {
                return CommonResult<ExperianceDto>.Failure<ExperianceDto>("Problem occured during fetching project with given id.");
            }
            return CommonResult<ExperianceDto>.Success(person);
        }

        public async Task<CommonResult> Update(ExperianceDto experianceDto)
        {
            var updateExperianceDto = await _experiancesRepository.Update(experianceDto);

            if (updateExperianceDto == null)
            {
                return CommonResult<ExperianceDto>.Failure<ExperianceDto>("Problem occured updating entity.");
            }

            return CommonResult<ExperianceDto>.Success(experianceDto);
        }
    }
}
