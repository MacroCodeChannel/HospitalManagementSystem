using HospitalManagementSystem.Data;
using HospitalManagementSystem.Interfaces;
using HospitalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Services
{
    public class SystemCodeDetailsRepository : ISystemCodeDetailsRepository
    {
        private readonly ApplicationDbContext _context;
        public SystemCodeDetailsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<SystemCodeDetail> AddSystemCodeDetailsAsync(SystemCodeDetail systemCode)
        {
            //add system code details to the database
            systemCode.CreatedOn = DateTime.UtcNow;
            await _context.SystemCodeDetails.AddAsync(systemCode);
            await _context.SaveChangesAsync();
            return systemCode;
        }

        public async Task<SystemCodeDetail> DeleteSystemCodeDetailsAsync(int id)
        {
            // Find the system code detail by id
            var systemCodeDetail = await _context.SystemCodeDetails.FindAsync(id);
            if (systemCodeDetail == null)
            {
                return null; // or throw an exception
            }
            // Remove the system code detail from the database
            _context.SystemCodeDetails.Remove(systemCodeDetail);
            await _context.SaveChangesAsync();
            return systemCodeDetail;
        }

        public async Task<SystemCodeDetail> GetSystemCodeDetailByIdAsync(int id)
        {
            // Find the system code detail by id
            var systemCodeDetail =  await _context.SystemCodeDetails
                .Include(sc => sc.SystemCode)
                .Include(sc => sc.CreatedBy)
                .Include(sc => sc.ModifiedBy)
                .FirstOrDefaultAsync(sc => sc.Id == id);
            return systemCodeDetail;
        }

        public async Task<List<SystemCodeDetail>> GetSystemCodeDetailsAsync()
        {
            // Get all system code details from the database
            var systemCodeDetails = await _context.SystemCodeDetails
                .Include(sc => sc.SystemCode)
                .Include(sc => sc.CreatedBy)
                .Include(sc => sc.ModifiedBy)
                .ToListAsync();
            return systemCodeDetails;
        }

        public async Task<SystemCodeDetail> UpdateSystemCodeDetailAsync(SystemCodeDetail systemCode)
        {
            // Find the existing system code detail by id
            var existingSystemCodeDetail = await _context.SystemCodeDetails.FindAsync(systemCode.Id);
            if (existingSystemCodeDetail == null)
            {
                return null; // or throw an exception
            }
            // Update the properties of the existing system code detail
            existingSystemCodeDetail.SystemCodeId = systemCode.SystemCodeId;
            existingSystemCodeDetail.Code = systemCode.Code;
            existingSystemCodeDetail.Description = systemCode.Description;
            existingSystemCodeDetail.OrderNo = systemCode.OrderNo;
            existingSystemCodeDetail.ModifiedById = systemCode.ModifiedById;
            existingSystemCodeDetail.ModifiedOn = DateTime.UtcNow;
            // Update the system code detail in the database
            _context.SystemCodeDetails.Update(existingSystemCodeDetail);
            await _context.SaveChangesAsync();
            return existingSystemCodeDetail;

        }
    }
}
