using HospitalManagementSystem.Data;
using HospitalManagementSystem.Interfaces;
using HospitalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Services
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;
        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Department> AddDepartmentAsync(Department department)
        {
            // Add department to the database
            department.CreatedOn = DateTime.UtcNow;
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<Department> DeleteDepartmentAsync(int id)
        {
            // Find the department by id
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return null; // or throw an exception
            }
            // Remove the department from the database
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return department;

        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            // Find the department by id
            var department = await _context.Departments
                .Include(d => d.CreatedBy)
                .Include(d => d.ModifiedBy)
                .FirstOrDefaultAsync(d => d.Id == id);
            return department;
        }

        public async Task<List<Department>> GetDepartmentsAsync()
        {
            // Get all departments from the database
            var departments = await _context.Departments
                .Include(d => d.CreatedBy)
                .Include(d => d.ModifiedBy)
                .ToListAsync();
            return departments;
        }

        public async Task<Department> UpdateDepartmentAsync(Department department)
        {
            var existingDepartment = await _context.Departments.FindAsync(department.Id);
            if (existingDepartment == null)
            {
                return null;
            }
            existingDepartment.Name = department.Name;
            existingDepartment.Code = department.Code;
            existingDepartment.ModifiedById = department.ModifiedById;
            existingDepartment.ModifiedOn = DateTime.UtcNow;
            _context.Departments.Update(existingDepartment);
            await _context.SaveChangesAsync();
            return existingDepartment;
        }
    }
}
