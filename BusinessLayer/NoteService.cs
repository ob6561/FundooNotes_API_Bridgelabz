using FundooNotes.API.DataBaseLayer;
using FundooNotes.API.ModelLayer.DTOs;
using FundooNotes.API.ModelLayer.Entities;

namespace FundooNotes.API.BusinessLayer
{
    public class NoteService
    {
        private readonly NoteRepository _repository;

        public NoteService(NoteRepository repository)
        {
            _repository = repository;
        }

        public void CreateNote(CreateNoteDto dto, int userId)
        {
            Note note = new Note
            {
                Title = dto.Title,
                Content = dto.Content,
                Color = dto.Color,
                UserId = userId
            };

            _repository.CreateNote(note);
        }

        public List<Note> GetNotes(int userId)
        {
            return _repository.GetNotesByUserId(userId);
        }
        public bool UpdateNote(int noteId, int userId, UpdateNoteDto dto)
        {
            var note = _repository.GetNoteById(noteId, userId);
            if (note == null)
                return false;

            note.Title = dto.Title ?? note.Title;
            note.Content = dto.Content ?? note.Content;
            note.Color = dto.Color ?? note.Color;
            note.UpdatedAt = DateTime.Now;

            _repository.UpdateNote(note);
            return true;
        }
        public bool DeleteNote(int noteId, int userId)
        {
            return _repository.SoftDeleteNote(noteId, userId);
        }

        public bool ToggleArchive(int noteId, int userId)
        {
            return _repository.ToggleArchive(noteId, userId);
        }

        public bool TogglePin(int noteId, int userId)
        {
            return _repository.TogglePin(noteId, userId);
        }

    }
}
