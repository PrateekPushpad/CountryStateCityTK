using BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserInfoRepo
    {
        Task<UserInfo> GetUserInfo(string username, string password);
        Task RegisterUser(UserInfo UserInfo);
    }
}
