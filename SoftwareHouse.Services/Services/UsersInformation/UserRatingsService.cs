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
    public class UserRatingsService : IUserRatingsService
    {
        private readonly IUserRatingsRepository _ratingsRepository;

        public UserRatingsService(IUserRatingsRepository ratingsRepository)
        {
            _ratingsRepository = ratingsRepository;
        }

        public async Task<IEnumerable<UserRatingDto>> GetAll()
        {
            var result = await _ratingsRepository.GetAll();

            if (!result.Any())
            {
                return null;
            }

            return result;
        }

        public async Task<CommonResult> Add(UserRatingDto ratingDto)
        {
            if (string.IsNullOrEmpty(ratingDto.Feedback))
            {
                return CommonResult.Failure("Cannot create user without field provided.");
            }

            if (string.IsNullOrEmpty(ratingDto.UserAssessorId))
            {
                return CommonResult.Failure("Cannot create user without userAssessorId provided.");
            }

            if (string.IsNullOrEmpty(ratingDto.UserEvaluatedId))
            {
                return CommonResult.Failure("Cannot create user without userEvaluatedId provided.");
            }

            if (Enum.IsDefined(typeof(StarNumberType), ratingDto.StarType))
            {
                return CommonResult.Failure("Wrong enum value.");
            }
              

            await _ratingsRepository.Add(ratingDto);

            return CommonResult.Success();
        }

        public async Task Delete(int id)
        {
            await _ratingsRepository.Delete(id);
        }

        public async Task<CommonResult<UserRatingDto>> GetById(int id)
        {
            var person = await _ratingsRepository.GetById(id);

            if (person == null)
            {
                return CommonResult<UserRatingDto>.Failure<UserRatingDto>("Problem occured during fetching rating with given id.");
            }
            return CommonResult<QualificationDto>.Success(person);
        }

        public async Task<CommonResult> Update(UserRatingDto ratingDto)
        {
            var updateQualificationDto = await _ratingsRepository.Update(ratingDto);

            if (updateQualificationDto == null)
            {
                return CommonResult<UserRatingDto>.Failure<UserRatingDto>("Problem occured updating entity.");
            }

            return CommonResult<QualificationDto>.Success(ratingDto);
        }
    }
}

