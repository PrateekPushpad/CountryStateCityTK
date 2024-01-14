using UI.Utility;

namespace UI.ViewModels.SkillViewModels
{
    public class SkillViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
    public class PagedSkillViewModel
    {
        public List<SkillViewModel> Skills { get; set; }
        public PageInfo PageInfo { get; set; }

    }
}
