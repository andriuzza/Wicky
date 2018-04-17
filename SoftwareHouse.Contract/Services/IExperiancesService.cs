using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SoftwareHouse.Contract.Common;
using SoftwareHouse.Contract.DataContracts;

namespace SoftwareHouse.Contract.Services
{
    public interface IExperiancesService
    {
        Task<IEnumerable<ExperianceDto>> GetAll();
        Task<CommonResult> Add(ExperianceDto experianceDto);
        Task<bool> CheckIfExperianceAlreadyExist(string userId, ExperianceType type);
        Task Delete(int id);
        Task<CommonResult<ExperianceDto>> GetById(int id);
        Task<CommonResult> Update(ExperianceDto experianceDto);
    }
}
