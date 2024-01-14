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
    public class UserInfoRepo : IUserInfoRepo
    {
        private readonly ApplicationDbContext _context;
        public UserInfoRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserInfo> GetUserInfo(string username, string password)
        {
            return await _context.UserInfo.FirstOrDefaultAsync(x=> x.UserName.ToLower() == username.ToLower() && x.Password == password);
        }

        public async Task RegisterUser(UserInfo userInfo)
        {
            if(!Exists(userInfo.UserName))
            {
                await _context.UserInfo.AddAsync(userInfo);
                await _context.SaveChangesAsync();
            }
        }

        private bool Exists(string UserName)
        {
            return _context.UserInfo.Any(x => x.UserName.ToLower() == UserName.ToLower());
        }
    }
}
