using Application.ViewModel.Common;

namespace Application.ViewModel.Note
{
    public class NoteAddDto : EntityAddDto
    {
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
