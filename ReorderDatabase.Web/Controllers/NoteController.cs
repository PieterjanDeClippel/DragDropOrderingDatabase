using Microsoft.AspNetCore.Mvc;
using ReorderDatabase.Data.Services;
using ReorderDatabase.Dtos;

namespace ReorderDatabase.Web.Controllers
{
    [Route("web/[controller]")]
    public class NoteController : Controller
    {
        private readonly INoteService noteService;
        public NoteController(INoteService noteService)
        {
            this.noteService = noteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> Index()
        {
            var notes = await noteService.GetNotes();
            return Ok(notes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> Get(int id)
        {
            var note = await noteService.GetNote(id);
            return Ok(note);
        }

        [HttpPost]
        public async Task<ActionResult<Note>> Post([FromBody] Note note)
        {
            var newNote = await noteService.InsertNote(note);
            return Ok(newNote);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Note>> Put([FromBody] Note note)
        {
            var updatedNote = await noteService.UpdateNote(note);
            return Ok(updatedNote);
        }
        
        [HttpPut("swap")]
        public async Task<ActionResult<Note>> Swap([FromBody] NoteSwap noteSwap)
        {
            var note = await noteService.SwapNote(noteSwap);
            return Ok(note);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await noteService.DeleteNote(id);
            return Ok();
        }
    }
}
