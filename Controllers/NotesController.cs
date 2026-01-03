using BusinessLayer;
using ModelLayer.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FundooNotes.API.Controllers
{
    [ApiController]
    [Route("api/notes")]
    [Authorize] 
    public class NotesController : ControllerBase
    {
        private readonly NoteService _service;

        public NotesController(NoteService service)
        {
            _service = service;
        }

        
        [HttpGet("debug-auth")]
        public IActionResult DebugAuth()
        {
            return Ok(User.Claims.Select(c => new { c.Type, c.Value }));
        }

        [HttpPost]
        public IActionResult CreateNote(CreateNoteDto dto)
        {
            int userId = int.Parse(User.FindFirst("UserId").Value);
            _service.CreateNote(dto, userId);
            return Ok("Note created successfully");
        }

        [HttpGet]
        public IActionResult GetNotes()
        {
            int userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(_service.GetNotes(userId));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateNote(int id, UpdateNoteDto dto)
        {
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            var result = _service.UpdateNote(id, userId, dto);
            if (!result)
                return NotFound("Note not found");

            return Ok("Note updated successfully");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteNote(int id)
        {
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            var result = _service.DeleteNote(id, userId);
            if (!result)
                return NotFound("Note not found");

            return Ok("Note moved to trash");
        }

        [HttpPatch("{id}/archive")]
        public IActionResult ToggleArchive(int id)
        {
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            var result = _service.ToggleArchive(id, userId);
            if (!result)
                return NotFound("Note not found");

            return Ok("Archive status toggled");
        }

        [HttpPatch("{id}/pin")]
        public IActionResult TogglePin(int id)
        {
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            var result = _service.TogglePin(id, userId);
            if (!result)
                return NotFound("Note not found");

            return Ok("Pin status toggled");
        }

    }
}
