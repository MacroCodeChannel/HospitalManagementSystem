using HospitalManagementSystem.Data;
using HospitalManagementSystem.Interfaces;
using HospitalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Services
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ApplicationDbContext _context;
        public CountryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Country> AddCountryAsync(Country country)
        {
            //add country to the database
            country.CreatedOn = DateTime.UtcNow;
            await _context.Countries.AddAsync(country);
            await _context.SaveChangesAsync();
            return country;
        }

        public async Task<Country> DeleteCountryAsync(int id)
        {
            // Find the country by id
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return null; // or throw an exception
            }
            // Remove the country from the database
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
            return country;
        }

        public async Task<List<Country>> GetCountriesAsync()
        {
            // Get all countries from the database
            var countries = await _context.Countries
                .Include(c => c.CreatedBy)
                .Include(c => c.ModifiedBy)
                .ToListAsync();
            return countries;

        }

        public async Task<Country> GetCountryByIdAsync(int id)
        {
            // Find the country by id
            var country = await _context.Countries
                .Include(c => c.CreatedBy)
                .Include(c => c.ModifiedBy)
                .FirstOrDefaultAsync(c => c.Id == id);
            return country;
        }

        public async Task<Country> UpdateCountryAsync(Country country)
        {
            // Find the existing country in the database
            var existingCountry = await _context.Countries.FindAsync(country.Id);
            if (existingCountry == null)
            {
                return null; // or throw an exception
            }
            // Update the properties of the existing country
            existingCountry.Name = country.Name;
            existingCountry.Code = country.Code;
            existingCountry.ModifiedById = country.ModifiedById;
            existingCountry.ModifiedOn = DateTime.UtcNow;
            // Save changes to the database
            await _context.SaveChangesAsync();
            return existingCountry;
        }
    }
}
