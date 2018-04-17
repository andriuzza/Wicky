using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SoftwareHouse.Contract.DataContracts;

namespace SoftwareHouse.Contract.Repositories
{
    public interface IPhotosOfWorkRepository
    {
        Task<IEnumerable<UserWorkPhotoDto>> GetAll();
        Task<UserWorkPhotoDto> GetById(int id);
        Task<UserWorkPhotoDto> Add(UserWorkPhotoDto photoDto);
        Task<UserWorkPhotoDto> Update(UserWorkPhotoDto photo);
        Task Delete(int id);
    }
}
