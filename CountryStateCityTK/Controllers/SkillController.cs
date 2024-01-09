using BAL;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using UI.ViewModels;
using UI.ViewModels.SkillViewModels;

namespace UI.Controllers
{
    public class SkillController : Controller
    {
        private readonly ISkillRepo _skillRepo;

        public SkillController(ISkillRepo skillRepo)
        {
            _skillRepo = skillRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<SkillViewModel> vm = new List<SkillViewModel>();
            var skills = await this._skillRepo.GetAll();
            foreach (var skill in skills)
            {
                vm.Add(new SkillViewModel { Id = skill.Id, Title = skill.Title });
            }
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int Id)
        {
            SkillViewModel vm = new SkillViewModel();
            var skill = await _skillRepo.GetById(Id);
            vm.Id = skill.Id;
            vm.Title = skill.Title;
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSkillViewModel vm)
        {
            var skill = new Skill
            {
                Title = vm.Title
            };
            await _skillRepo.Save(skill);
            return RedirectToAction("GetAll");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            SkillViewModel vm = new SkillViewModel();
            var country = await _skillRepo.GetById(Id);
            vm.Id = country.Id;
            vm.Title = country.Title;
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SkillViewModel vm)
        {
            var skill = new Skill
            {
                Id = (int)vm.Id,
                Title = vm.Title
            };
            await this._skillRepo.Edit(skill);
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var city = await _skillRepo.GetById(Id);
            await _skillRepo.RemoveData(city);
            return RedirectToAction("GetAll");
        }
    }
}
