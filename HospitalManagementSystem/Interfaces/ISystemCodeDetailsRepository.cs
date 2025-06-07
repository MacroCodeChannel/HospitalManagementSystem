using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Interfaces
{
    public interface ISystemCodeDetailsRepository
    {
        Task<List<SystemCodeDetail>> GetSystemCodeDetailsAsync();
        Task<SystemCodeDetail> GetSystemCodeDetailByIdAsync(int id);
        Task<SystemCodeDetail> AddSystemCodeDetailsAsync(SystemCodeDetail systemCode);
        Task<SystemCodeDetail> UpdateSystemCodeDetailAsync(SystemCodeDetail systemCode);
        Task<SystemCodeDetail> DeleteSystemCodeDetailsAsync(int id);
    }
}
