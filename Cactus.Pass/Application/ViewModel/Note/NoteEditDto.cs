using Application.ViewModel.Common;

namespace Application.ViewModel.Note
{
    public class NoteEditDto : EntityEditDto
    {
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
