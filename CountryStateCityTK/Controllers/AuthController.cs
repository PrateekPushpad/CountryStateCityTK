using BAL;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using UI.ViewModels.UserInfoViewModels;

namespace UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserInfoRepo _userInfoRepo;
        public AuthController(IUserInfoRepo userInfoRepo)
        {
            _userInfoRepo = userInfoRepo;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserInfoViewModel vm)
        {
            var user = await _userInfoRepo.GetUserInfo(vm.UserName, vm.Password);
            HttpContext.Session.SetInt32("User", user.UserId);
            HttpContext.Session.SetString("UserName", user.UserName);
            return RedirectToAction("GetAll", "Country");
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserInfoViewModel vm)
        {
            if (vm == null)
            {
                return NotFound();
            }
            else
            {
                var userInfo = new UserInfo
                {
                    UserName = vm.UserName,
                    Password = vm.Password
                };
                await _userInfoRepo.RegisterUser(userInfo);
                return RedirectToAction("Login");

            }
        }
    }
}
