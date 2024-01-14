using UI.Validations;

namespace UI.ViewModels.SkillViewModels
{
    public class CreateSkillViewModel
    {
        [Uppercase]
        public string Title { get; set; }
    }
}
