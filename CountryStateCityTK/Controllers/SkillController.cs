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
        
        public async Task<IActionResult> GetAll(string filterText, int pageNumber = 1, int pageSize = 3, string searchText = null)
        {
            List<SkillViewModel> vm = new List<SkillViewModel>();
            var skills = await this._skillRepo.GetAll();
            if(searchText != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchText = filterText;
            }
            ViewData["filterData"] = searchText;

            var totalCount = 0;
            if(!string.IsNullOrEmpty(searchText))
            {
                skills = skills.Where(x=>x.Title.Contains(searchText));
            }
            totalCount = skills.Count();
            skills = skills.Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
            foreach (var skill in skills)
            {
                vm.Add(new SkillViewModel { Id = skill.Id, Title = skill.Title });
            }
            var pvm = new PagedSkillViewModel
            {
                Skills = vm,
                PageInfo = new Utility.PageInfo
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalItems = totalCount
                }
            };
            return View(pvm);
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSkillViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var skill = new Skill
                {
                    Title = vm.Title
                };
                await _skillRepo.Save(skill);
                return RedirectToAction("GetAll");
            }
            return View();
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
