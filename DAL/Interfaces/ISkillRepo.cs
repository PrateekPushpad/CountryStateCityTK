using BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ISkillRepo
    {
        Task<IEnumerable<Skill>> GetAll();
        Task<Skill> GetById(int id);
        Task Save(Skill Skill);
        Task Edit(Skill Skill);
        Task RemoveData(Skill Skill);
    }
}
