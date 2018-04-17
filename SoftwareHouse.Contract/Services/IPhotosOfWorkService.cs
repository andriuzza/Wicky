using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SoftwareHouse.Contract.Common;
using SoftwareHouse.Contract.DataContracts;

namespace SoftwareHouse.Contract.Services
{
    public interface IPhotosOfWorkService
    {
        Task<IEnumerable<UserWorkPhotoDto>> GetAll();
        Task<CommonResult> Add(UserWorkPhotoDto experianceDto);
        Task Delete(int id);
        Task<CommonResult<UserWorkPhotoDto>> GetById(int id);
        Task<CommonResult> Update(UserWorkPhotoDto photoDto);
    }
}
