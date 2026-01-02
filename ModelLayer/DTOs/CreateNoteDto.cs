using System.ComponentModel.DataAnnotations;

namespace FundooNotes.API.ModelLayer.DTOs
{
    public class CreateNoteDto
    {
        [Required]
        public string Title { get; set; }

        public string Content { get; set; }
        public string Color { get; set; }
    }
}
