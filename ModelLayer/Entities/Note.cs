using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FundooNotes.API.ModelLayer.Entities
{
    public class Note
    {
        [Key]
        public int NoteId {  get; set; }
        [Required]
        public string Title {  get; set; }
        public string Content {  get; set; }
        public string Color {  get; set; }
        public bool IsPinned { get; set; } = false;
        public bool IsArchived { get; set; } = false;
        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        
        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
