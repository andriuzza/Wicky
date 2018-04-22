using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftwareHouse.Contract.Common;
using SoftwareHouse.Contract.DataContracts;
using SoftwareHouse.Contract.Repositories;
using SoftwareHouse.Contract.Services;

namespace SoftwareHouse.Services.Services.UsersInformation
{
    public class QualificationsService : IQualificationsService
    {
        private readonly IQualificationsRepository _qualificationsRepository;

        public QualificationsService(IQualificationsRepository qualificationsRepository)
        {
            _qualificationsRepository = qualificationsRepository;
        }

        public async Task<IEnumerable<QualificationDto>> GetAll()
        {
            var result = await _qualificationsRepository.GetAll();

            if (!result.Any())
            {
                return null;
            }

            return result;
        }


        public async Task<CommonResult> Add(QualificationDto qualificationDto)
        {
            if (string.IsNullOrEmpty(qualificationDto.QualificationField))
            {
                return CommonResult.Failure("Cannot create user without field provided.");
            }

            if (string.IsNullOrEmpty(qualificationDto.ApplicationUserId))
            {
                return CommonResult.Failure("Cannot create user without user Id provided.");
            }

            await _qualificationsRepository.Add(qualificationDto);

            return CommonResult.Success();
        }

        public async Task Delete(int id)
        {
            await _qualificationsRepository.Delete(id);

        }

        public async Task<CommonResult<QualificationDto>> GetUser(int id)
        {
            var person = await _qualificationsRepository.GetById(id);

            if (person == null)
            {
                return CommonResult<QualificationDto>.Failure<QualificationDto>("Problem occured during fetching qualification with given id.");
            }
            return CommonResult<QualificationDto>.Success(person);
        }

        public async Task<CommonResult> Update(QualificationDto qualificationDto)
        {
            var updateQualificationDto = await _qualificationsRepository.Update(qualificationDto);

            if (updateQualificationDto == null)
            {
                return CommonResult<QualificationDto>.Failure<QualificationDto>("Problem occured updating entity.");
            }

            return CommonResult<QualificationDto>.Success(qualificationDto);
        }
    }
}
