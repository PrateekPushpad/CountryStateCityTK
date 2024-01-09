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
    public class StateRepo : IStateRepo
    {
        private readonly ApplicationDbContext _context;
        public StateRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Edit(State state)
        {
            _context.State.Update(state);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<State>> GetAll()
        {
            return await _context.State.Include(x=>x.Country).ToListAsync();
        }

        public async Task<State> GetById(int id)
        {
            var state = await _context.State.FindAsync(id);
            return state; 
        }

        public async Task RemoveData(State state)
        {
            _context.State.Remove(state);
            await _context.SaveChangesAsync();
        }

        public async Task Save(State state)
        {
            await _context.State.AddAsync(state);
            await _context.SaveChangesAsync();
        }
    }
}
