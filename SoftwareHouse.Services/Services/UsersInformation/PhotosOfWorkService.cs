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
    public class PhotosOfWorkService : IPhotosOfWorkService
    {
        private readonly IPhotosOfWorkRepository _photosOfWorkRepository;

        public PhotosOfWorkService(IPhotosOfWorkRepository photosOfWorkRepository)
        {
            _photosOfWorkRepository = photosOfWorkRepository;
        }

        public async Task<IEnumerable<UserWorkPhotoDto>> GetAll()
        {
            var result = await _photosOfWorkRepository.GetAll();

            if (!result.Any())
            {
                return null;
            }

            return result;
        }

        public async Task<CommonResult> Add(UserWorkPhotoDto experianceDto)
        {
            if (string.IsNullOrEmpty(experianceDto.ApplicationUserId))
            {
                return CommonResult.Failure("Empty user Id");
            }

            if (string.IsNullOrEmpty(experianceDto.PhotoUrl))
            {
                return CommonResult.Failure("Cannot create photo without its url address");
            }

            await _photosOfWorkRepository.Add(experianceDto);

            return CommonResult.Success();
        }

        public async Task Delete(int id)
        {
            await _photosOfWorkRepository.Delete(id);

        }

        public async Task<CommonResult<UserWorkPhotoDto>> GetById(int id)
        {
            var photoDto = await _photosOfWorkRepository.GetById(id);

            if (photoDto == null)
            {
                return CommonResult<UserWorkPhotoDto>.Failure<UserWorkPhotoDto>("Problem occured during fetching photo url with given id.");
            }
            return CommonResult<UserWorkPhotoDto>.Success(photoDto);
        }

        public async Task<CommonResult> Update(UserWorkPhotoDto photoDto)
        {
            var updateApplicationUserDto = await _photosOfWorkRepository.Update(photoDto);

            if (updateApplicationUserDto == null)
            {
                return CommonResult<UserWorkPhotoDto>.Failure<UserWorkPhotoDto>("Problem occured updating entity.");
            }

            return CommonResult<UserWorkPhotoDto>.Success(photoDto);
        }
    }
}
