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
    public class SkillRepo : ISkillRepo
    {
        private readonly ApplicationDbContext _context;
        public SkillRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Edit(Skill Skill)
        {
            _context.Skill.Update(Skill);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Skill>> GetAll()
        {
            return await _context.Skill.ToListAsync();
        }

        public async Task<Skill> GetById(int id)
        {
            var skill = await _context.Skill.FindAsync(id);
            return skill; 
        }

        public async Task RemoveData(Skill Skill)
        {
            _context.Skill.Remove(Skill);
            await _context.SaveChangesAsync();
        }

        public async Task Save(Skill Skill)
        {
            await _context.Skill.AddAsync(Skill);
            await _context.SaveChangesAsync();
        }
    }
}
