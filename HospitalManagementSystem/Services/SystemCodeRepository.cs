using HospitalManagementSystem.Data;
using HospitalManagementSystem.Interfaces;
using HospitalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Services
{
    public class SystemCodeRepository : ISystemCodeRepository
    {
        private readonly ApplicationDbContext _context;
        public SystemCodeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<SystemCode>> GetSystemCodesAsync()
        {
           var systemCodes = await _context.SystemCodes
                .Include(sc => sc.CreatedBy)
                .Include(sc => sc.ModifiedBy)
                .ToListAsync();
            return systemCodes;
        }
        public async Task<SystemCode> GetSystemCodeByIdAsync(int id)
        {
            var systemCode =  await _context.SystemCodes
                .Include(sc => sc.CreatedBy)
                .Include(sc => sc.ModifiedBy)
                .FirstOrDefaultAsync(sc => sc.Id == id);
            return systemCode;

        }
        public async Task<List<SystemCodeDetail>> GetSystemCodeDetailsAsync(int systemCodeId)
        {
            var systemCodeDetails = await _context.SystemCodeDetails
                 .Include(sc => sc.SystemCode)
                 .Include(sc => sc.CreatedBy)
                 .Include(sc => sc.ModifiedBy)
                 .Where(sc => sc.SystemCodeId == systemCodeId)
                 .ToListAsync();
            return systemCodeDetails;


        }
        public async Task<SystemCode> AddSystemCodeAsync(SystemCode systemCode)
        {
            systemCode.CreatedOn = DateTime.UtcNow;
            await _context.SystemCodes.AddAsync(systemCode);
            await _context.SaveChangesAsync();
            return systemCode;
        }
        public async Task<SystemCode> UpdateSystemCodeAsync(SystemCode systemCode)
        {
            var existingSystemCode = await _context.SystemCodes.FindAsync(systemCode.Id);
            if (existingSystemCode == null)
            {
                return null;
            }
            existingSystemCode.Code = systemCode.Code;
            existingSystemCode.Description = systemCode.Description;
            existingSystemCode.OrderNo = systemCode.OrderNo;
            existingSystemCode.ModifiedById = systemCode.ModifiedById;
            existingSystemCode.ModifiedOn = DateTime.UtcNow;
            _context.SystemCodes.Update(existingSystemCode);
            await _context.SaveChangesAsync();
            return existingSystemCode;

        }
        public async Task<SystemCode> DeleteSystemCodeAsync(int id)
        {
            var systemCode = await _context.SystemCodes.FindAsync(id);
            if (systemCode == null)
            {
                return null;
            }
             _context.SystemCodes.Remove(systemCode);
            await _context.SaveChangesAsync();
            return systemCode;
        }
    }
}
