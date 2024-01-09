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
    public class CityRepo : ICityRepo
    {
        private readonly ApplicationDbContext _context;
        public CityRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Edit(City city)
        {
            _context.City.Update(city);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<City>> GetAll()
        {
            return await _context.City.Include(x=>x.State).ToListAsync();
        }

        public async Task<City> GetById(int id)
        {
            var city = await _context.City.FindAsync(id);
            return city; 
        }

        public async Task RemoveData(City city)
        {
            _context.City.Remove(city);
            await _context.SaveChangesAsync();
        }

        public async Task Save(City city)
        {
            await _context.City.AddAsync(city);
            await _context.SaveChangesAsync();
        }
    }
}
