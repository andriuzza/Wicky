using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SoftwareHouse.Contract.DataContracts;

namespace SoftwareHouse.Contract.Repositories
{
    public interface IQualificationsRepository
    {
        Task<IEnumerable<QualificationDto>> GetAll();
        Task<QualificationDto> GetById(int id);
        Task<QualificationDto> Add(QualificationDto qualificationDto);
        Task<QualificationDto> Update(QualificationDto qualificationDto);
        Task Delete(int id);
    }
}
