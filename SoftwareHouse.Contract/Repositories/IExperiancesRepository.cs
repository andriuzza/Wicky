using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SoftwareHouse.Contract.DataContracts;

namespace SoftwareHouse.Contract.Repositories
{
    public interface IExperiancesRepository
    {
        Task<IEnumerable<ExperianceDto>> GetAll();
        Task<ExperianceDto> GetById(int id);
        Task<ExperianceDto> Add(ExperianceDto experianceDto);
        Task<ExperianceDto> Update(ExperianceDto user);
        Task Delete(int id);

        Task<bool> CheckIfExperianceAlreadyExist(string userId, ExperianceType type);
    }
}
