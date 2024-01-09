using BAL;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class CountryRepo : ICountryRepo
    {
        private readonly ApplicationDbContext _context;
        public CountryRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Edit(Country country)
        {
            _context.Country.Update(country);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Country>> GetAll()
        {
            return await _context.Country.ToListAsync();
        }

        public async Task<Country> GetById(int id)
        {
            var country = await _context.Country.FindAsync(id);
            return country; 
        }

        public async Task RemoveData(Country country)
        {
            _context.Country.Remove(country);
            await _context.SaveChangesAsync();
        }

        public async Task Save(Country country)
        {
            await _context.Country.AddAsync(country);
            await _context.SaveChangesAsync();
        }
    }
}
