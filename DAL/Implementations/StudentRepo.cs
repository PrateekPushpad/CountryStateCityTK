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
    public class StudentRepo : IStudentRepo
    {
        private readonly ApplicationDbContext _context;
        public StudentRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Edit(Student Student)
        {
            _context.Student.Update(Student);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            return await _context.Student.ToListAsync();
        }

        public async Task<Student> GetById(int id)
        {
            var student = await _context.Student.Include(x=>x.StudentSkills).FirstOrDefaultAsync(y=>y.Id == id);
            return student; 
        }

        public async Task RemoveData(Student Student)
        {
            _context.Student.Remove(Student);
            await _context.SaveChangesAsync();
        }

        public async Task Save(Student Student)
        {
            await _context.Student.AddAsync(Student);
            await _context.SaveChangesAsync();
        }
    }
}
