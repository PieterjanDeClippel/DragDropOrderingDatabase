using ReorderDatabase.Data.Repositories;

namespace ReorderDatabase.Data.Services
{
    public interface INoteService
    {
        Task<IEnumerable<Dtos.Note>> GetNotes();
        Task<Dtos.Note> GetNote(int id);
        Task<Dtos.Note> InsertNote(Dtos.Note note);
        Task<Dtos.Note> UpdateNote(Dtos.Note note);
        Task<Dtos.Note> SwapNote(Dtos.NoteSwap noteSwap);
        Task DeleteNote(int id);

    }

    internal class NoteService : INoteService
    {
        private readonly INoteRepository noteRepository;
        public NoteService(INoteRepository noteRepository)
        {
            this.noteRepository = noteRepository;
        }

        public async Task<IEnumerable<Dtos.Note>> GetNotes()
        {
            var notes = await noteRepository.GetNotes();
            return notes;
        }

        public async Task<Dtos.Note> GetNote(int id)
        {
            var note = await noteRepository.GetNote(id);
            return note;
        }

        public async Task<Dtos.Note> InsertNote(Dtos.Note note)
        {
            var newNote = await noteRepository.InsertNote(note);
            return newNote;
        }

        public async Task<Dtos.Note> UpdateNote(Dtos.Note note)
        {
            var updatedNote = await noteRepository.UpdateNote(note);
            return updatedNote;
        }

        public async Task<Dtos.Note> SwapNote(Dtos.NoteSwap noteSwap)
        {
            var swappedNote = await noteRepository.SwapNote(noteSwap);
            return swappedNote;
        }

        public async Task DeleteNote(int id)
        {
            await noteRepository.DeleteNote(id);
        }
    }
}
