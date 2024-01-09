using BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IStudentRepo
    {
        Task<IEnumerable<Student>> GetAll();
        Task<Student> GetById(int id);
        Task Save(Student Student);
        Task Edit(Student Student);
        Task RemoveData(Student Student);
    }
}
