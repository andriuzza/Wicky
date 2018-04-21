using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SoftwareHouse.Contract.Common;
using SoftwareHouse.Contract.DataContracts;

namespace SoftwareHouse.Contract.Services
{
    public interface IQualificationsService
    {
        Task<IEnumerable<QualificationDto>> GetAll();
        Task<CommonResult> Add(QualificationDto qualificationDto);
        Task Delete(int id);
        Task<CommonResult<QualificationDto>> GetUser(int id);
        Task<CommonResult> Update(QualificationDto qualificationDto);
    }
}
