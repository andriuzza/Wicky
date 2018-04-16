using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SoftwareHouse.Contract.Common;
using SoftwareHouse.Contract.DataContracts;

namespace SoftwareHouse.Contract.Services
{
    public interface IPersonManagementService
    {
        Task<IEnumerable<ApplicationUserDto>> GetAllUsers();
        Task<CommonResult> Add(ApplicationUserDto user);
        Task Delete(string id);
        Task<CommonResult<ApplicationUserDto>> GetUser(string id);
        Task<CommonResult> Update(ApplicationUserDto user);
    }
}
