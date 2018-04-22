using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SoftwareHouse.Contract.DataContracts;

namespace SoftwareHouse.Contract.Repositories
{
    public interface IUserRatingsRepository
    {
        Task<IEnumerable<UserRatingDto>> GetAll();
        Task<UserRatingDto> Add(UserRatingDto userRatingDto);
        Task<UserRatingDto> GetById(int id);
        Task<UserRatingDto> Update(UserRatingDto userRatingDto);
        Task Delete(int id);
    }
}
