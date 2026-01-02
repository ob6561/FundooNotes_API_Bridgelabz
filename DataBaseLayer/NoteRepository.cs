using FundooNotes.API.Data;
using FundooNotes.API.ModelLayer.Entities;

namespace FundooNotes.API.DataBaseLayer
{
    public class NoteRepository
    {
        private readonly FundooContext _context;

        public NoteRepository(FundooContext context)
        {
            _context = context;
        }

        public void CreateNote(Note note)
        {
            _context.Notes.Add(note);
            _context.SaveChanges();
        }

        public List<Note> GetNotesByUserId(int userId)
        {
            return _context.Notes
                .Where(n => n.UserId == userId && !n.IsDeleted)
                .ToList();
        }

        public Note GetNoteById(int noteId, int userId)
        {
            return _context.Notes
                .FirstOrDefault(n => n.NoteId == noteId && n.UserId == userId && !n.IsDeleted);
        }

        public void UpdateNote(Note note)
        {
            _context.Notes.Update(note);
            _context.SaveChanges();
        }
        public bool SoftDeleteNote(int noteId, int userId)
        {
            var note = _context.Notes
                .FirstOrDefault(n => n.NoteId == noteId && n.UserId == userId && !n.IsDeleted);

            if (note == null)
                return false;

            note.IsDeleted = true;
            note.UpdatedAt = DateTime.Now;

            _context.SaveChanges();
            return true;
        }
        public bool ToggleArchive(int noteId, int userId)
        {
            var note = _context.Notes
                .FirstOrDefault(n => n.NoteId == noteId && n.UserId == userId && !n.IsDeleted);

            if (note == null)
                return false;

            note.IsArchived = !note.IsArchived;
            note.UpdatedAt = DateTime.Now;

            _context.SaveChanges();
            return true;
        }

        public bool TogglePin(int noteId, int userId)
        {
            var note = _context.Notes
                .FirstOrDefault(n => n.NoteId == noteId && n.UserId == userId && !n.IsDeleted);

            if (note == null)
                return false;

            note.IsPinned = !note.IsPinned;
            note.UpdatedAt = DateTime.Now;

            _context.SaveChanges();
            return true;
        }

    }
}
