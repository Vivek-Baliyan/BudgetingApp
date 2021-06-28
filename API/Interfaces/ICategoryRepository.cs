using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface ICategoryRepository
    {
        void UpdateMaster(MasterCategory masterCategory);
        void UpdateSub(SubCategory subCategory);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<MasterCategoryDto>> GetCategories(int AppUserId);
    }
}