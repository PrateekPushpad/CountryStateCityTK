using BAL;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UI.ViewModels;

namespace UI.Controllers
{
    public class CityController : Controller
    {
        private readonly ICityRepo cityRepo;
        private readonly IStateRepo stateRepo;
        public CityController(ICityRepo cityRepo, IStateRepo stateRepo)
        {
            this.cityRepo = cityRepo;
            this.stateRepo = stateRepo;
        }
        public async Task<IActionResult> Index()
        {
            var city = await this.cityRepo.GetAll();
            return View(city);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var state = await this.stateRepo.GetAll();
            ViewBag.Statelist = new SelectList(state, "Id", "Name");
            return View();
        }
        public async Task<IActionResult> Create(CityViewModel vm)
        {
            var city = new City
            {
                Name = vm.Name,
                StateId = vm.StateId
            };
            await this.cityRepo.Save(city);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int Id)
        {
            var city = await this.cityRepo.GetById(Id);
            var states = await this.stateRepo.GetAll();
            ViewBag.Statelist = new SelectList(states, "Id", "Name");
            var vm = new CityViewModel
            {
                Id = city.Id,
                Name = city.Name,
                StateId = city.StateId
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CityViewModel vm)
        {
            var city = new City
            {
                Id = (int)vm.Id,
                Name = vm.Name,
                StateId = vm.StateId
            };
            await this.cityRepo.Edit(city);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var city = await this.cityRepo.GetById(Id);
            await this.cityRepo.RemoveData(city);
            return RedirectToAction("Index");
        }
    }
}
