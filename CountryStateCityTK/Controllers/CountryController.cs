using BAL;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using UI.ViewModels;

namespace UI.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryRepo countryRepo;

        public CountryController(ICountryRepo countryRepo)
        {
            this.countryRepo = countryRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(HttpContext.Session.GetInt32("User") != null)
            {
                List<CountryViewModel> vm = new List<CountryViewModel>();
                var countries = await this.countryRepo.GetAll();
                foreach (var country in countries)
                {
                    vm.Add(new CountryViewModel { Id = country.Id, Name = country.Name });
                }
                return View(vm);
            }

            return RedirectToAction("Login", "Auth");

        }

        [HttpGet]
        public async Task<IActionResult> GetById(int Id)
        {
            CountryViewModel vm = new CountryViewModel();
            var country = await this.countryRepo.GetById(Id);
            vm.Id = country.Id;
            vm.Name = country.Name;
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CountryViewModel vm = new CountryViewModel();
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CountryViewModel vm)
        {
            var country = new Country
            {
                Name = vm.Name
            };
            await this.countryRepo.Save(country);
            return RedirectToAction("GetAll");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            CountryViewModel vm = new CountryViewModel();
            var country = await this.countryRepo.GetById(Id);
            vm.Id = country.Id;
            vm.Name = country.Name;
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CountryViewModel vm)
        {
            var country = new Country
            {
                Id = (int)vm.Id,
                Name = vm.Name
            };
            await this.countryRepo.Edit(country);
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var city = await this.countryRepo.GetById(Id);
            await this.countryRepo.RemoveData(city);
            return RedirectToAction("GetAll");
        }
    }
}
