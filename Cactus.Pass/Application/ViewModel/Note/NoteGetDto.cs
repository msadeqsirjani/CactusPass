using Application.ViewModel.Common;

namespace Application.ViewModel.Note
{
    public class NoteGetDto : EntityGetDto
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string UserId { get; set; }
    }
}
