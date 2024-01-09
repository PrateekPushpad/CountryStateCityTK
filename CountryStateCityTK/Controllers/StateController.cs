using BAL;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UI.ViewModels;

namespace UI.Controllers
{
    public class StateController : Controller
    {
        private readonly IStateRepo stateRepo;
        private readonly ICountryRepo countryRepo;
        public StateController(ICountryRepo countryRepo, IStateRepo stateRepo)
        {
            this.countryRepo = countryRepo;
            this.stateRepo = stateRepo;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var states = await this.stateRepo.GetAll();
            return View(states);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var countries = await this.countryRepo.GetAll();
            ViewBag.Countrylist = new SelectList(countries, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StateViewModel vm)
        {
            var state = new State
            {
                Name = vm.Name,
                CountryId = vm.CountryId
            };
            await this.stateRepo.Save(state);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var state = await this.stateRepo.GetById(Id);
            var vm = new StateViewModel
            {
                Id = state.Id,
                Name = state.Name,
                CountryId = state.CountryId
            };
            var countries = await this.countryRepo.GetAll();
            ViewBag.Countrylist = new SelectList(countries, "Id", "Name");
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StateViewModel vm)
        {
            var state = new State
            {   
                Id = (int)vm.Id,
                Name = vm.Name,
                CountryId = vm.CountryId
            };
            await this.stateRepo.Edit(state);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var state = await this.stateRepo.GetById(Id);
            await this.stateRepo.RemoveData(state);
            return RedirectToAction("Index");
        }
    }
}
