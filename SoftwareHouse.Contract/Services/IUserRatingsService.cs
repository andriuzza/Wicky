using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SoftwareHouse.Contract.Common;
using SoftwareHouse.Contract.DataContracts;

namespace SoftwareHouse.Contract.Services
{
    public interface IUserRatingsService
    {
        Task<IEnumerable<UserRatingDto>> GetAll();
        Task<CommonResult> Add(UserRatingDto ratingDto);
        Task<CommonResult> Update(UserRatingDto ratingDto);
        Task Delete(int id);
        Task<CommonResult<UserRatingDto>> GetById(int id);
    }
}
