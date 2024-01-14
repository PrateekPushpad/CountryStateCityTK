using BAL;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TechnologyKeeda.UI.ViewModels.StudentViewModels;

namespace UI.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepo _studentRepo;
        private readonly ISkillRepo _skillRepo;

        public StudentController(IStudentRepo studentRepo, ISkillRepo skillRepo)
        {
            _studentRepo = studentRepo;
            _skillRepo = skillRepo;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _studentRepo.GetAll();
            List<StudentViewModel> studentList = new List<StudentViewModel>();
            foreach (var student in students)
            {
                studentList.Add(new StudentViewModel { Id = student.Id, Name = student.Name });
            }
            return View(studentList);
        }
        [HttpGet]

        public async Task<IActionResult> Create()
        {
            CreateStudentViewModel vm = new CreateStudentViewModel();
            var skills = await _skillRepo.GetAll();
            foreach (var skill in skills)
            {
                vm.SkillList.Add(new CheckBoxTable { SkillId = skill.Id, SkillName = skill.Title, IsChecked = false });

            }
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var student = new Student
                {
                    Name = vm.StudentName
                };
                var selectedSkillIds = vm.SkillList.Where(x => x.IsChecked == true)
                    .Select(y => y.SkillId).ToList();  //1,2,3

                foreach (var skillId in selectedSkillIds)
                {
                    student.StudentSkills.Add(new StudentSkill
                    {
                        SkillId = skillId
                    });

                }

                await _studentRepo.Save(student);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            EditStudentViewModel vm = new EditStudentViewModel();
            var student = await _studentRepo.GetById(id);
            vm.StudentName = student.Name;
            var existingSkillIds = student.StudentSkills.Select(x => x.SkillId).ToList();

            var skills = await _skillRepo.GetAll();
            foreach (var skill in skills)
            {
                vm.SkillList.Add(new CheckBoxTable
                {
                    SkillId = skill.Id,
                    SkillName = skill.Title,

                    IsChecked = existingSkillIds.Contains(skill.Id)
                });

            }
            return View(vm);



        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditStudentViewModel vm)
        {
            var student = await _studentRepo.GetById(vm.Id);
            student.Name = vm.StudentName;
            var existingSkillIds = student.StudentSkills
                .Select(x => x.SkillId).ToList();

            var selectedSkillIds = vm.SkillList.Where(x => x.IsChecked)
                .Select(x => x.SkillId);

            var toRemove = existingSkillIds.Except(selectedSkillIds);
            var toAdd = selectedSkillIds.Except(existingSkillIds);

            foreach(var skill in toRemove)
            {
                var studentskill = student.StudentSkills.FirstOrDefault(wh => wh.SkillId == skill);
                student.StudentSkills.Remove(studentskill);
            }
            foreach (var skill in toAdd)
            {
                var studentskill = student.StudentSkills.FirstOrDefault(wh => wh.SkillId == skill);
                student.StudentSkills.Add(new StudentSkill
                {
                    SkillId = skill
                }); 
            }
            await _studentRepo.Edit(student);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var student = await _studentRepo.GetById(Id);
            await _studentRepo.RemoveData(student);
            return RedirectToAction("Index");
        }



    }
}
