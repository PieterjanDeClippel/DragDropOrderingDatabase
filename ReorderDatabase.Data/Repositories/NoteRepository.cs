namespace ReorderDatabase.Data.Repositories
{
    internal interface INoteRepository
    {
        Task<IEnumerable<Dtos.Note>> GetNotes();
        Task<Dtos.Note> GetNote(int id);
        Task<Dtos.Note> InsertNote(Dtos.Note note);
        Task<Dtos.Note> UpdateNote(Dtos.Note note);
        Task<(Dtos.Note, bool)> SwapNote(Dtos.NoteSwap noteSwap);
        Task DeleteNote(int id);
    }

    internal class NoteRepository : INoteRepository
    {
        private readonly NoteContext noteContext;
        public NoteRepository(NoteContext noteContext)
        {
            this.noteContext = noteContext;
        }

        public Task<IEnumerable<Dtos.Note>> GetNotes()
        {
            var notes = noteContext.Notes
                .OrderBy(n => n.Order)
                .Select(n => new Dtos.Note
                {
                    Id = n.Id,
                    Text = n.Text,
                    Numerator = n.Numerator,
                    Denominator = n.Denominator,
                    Order = n.Order,
                });
            return Task.FromResult<IEnumerable<Dtos.Note>>(notes);
        }

        public async Task<Dtos.Note> GetNote(int id)
        {
            var note = await noteContext.Notes.FindAsync(id);
            return new Dtos.Note
            {
                Id = note.Id,
                Text = note.Text,
                Numerator = note.Numerator,
                Denominator = note.Denominator,
                Order = note.Order,
            };
        }

        public async Task<Dtos.Note> InsertNote(Dtos.Note note)
        {
            ulong last = 0;
            if (noteContext.Notes.Any())
            {
                last = noteContext.Notes.Max(n => n.Numerator);
            }

            var entityNote = new Entities.Note
            {
                Id = 0,
                Text = note.Text,
                Numerator = last + 1,
                Denominator = 1,
            };
            await noteContext.Notes.AddAsync(entityNote);
            await noteContext.SaveChangesAsync();

            return new Dtos.Note
            {
                Id = entityNote.Id,
                Text = entityNote.Text,
                Numerator = entityNote.Numerator,
                Denominator = entityNote.Denominator,
                Order = entityNote.Order,
            };
        }

        public async Task<Dtos.Note> UpdateNote(Dtos.Note note)
        {
            var entityNote = noteContext.Notes.Find(note.Id);

            entityNote.Text = note.Text;
            entityNote.Numerator = note.Numerator;
            entityNote.Denominator = note.Denominator;

            noteContext.Update(entityNote);
            await noteContext.SaveChangesAsync();

            return new Dtos.Note
            {
                Id = note.Id,
                Text = entityNote.Text,
                Numerator = note.Numerator,
                Denominator = note.Denominator,
                Order = note.Order,
            };
        }

        public async Task<(Dtos.Note, bool)> SwapNote(Dtos.NoteSwap noteSwap)
        {
            var entityNote = await noteContext.Notes.FindAsync(noteSwap.Note.Id);
            entityNote.Numerator =
                (noteSwap.NoteBefore == null ? 0 : noteSwap.NoteBefore.Numerator) +
                (noteSwap.NoteAfter == null ? 1 : noteSwap.NoteAfter.Numerator);
            entityNote.Denominator =
                (noteSwap.NoteBefore == null ? 1 : noteSwap.NoteBefore.Denominator) + 
                (noteSwap.NoteAfter == null ? 0 : noteSwap.NoteAfter.Denominator);

            noteContext.Update(entityNote);
            await noteContext.SaveChangesAsync();

            var needsReindexing = noteContext.Notes.Select(n => n.Order).Distinct().Count() != noteContext.Notes.Count();

            return (new Dtos.Note
            {
                Id = noteSwap.Note.Id,
                Text = entityNote.Text,
                Numerator = entityNote.Numerator,
                Denominator = entityNote.Denominator,
                Order = entityNote.Order,
            }, needsReindexing);
        }

        public async Task DeleteNote(int id)
        {
            var entityNote = await noteContext.Notes.FindAsync(id);
            noteContext.Notes.Remove(entityNote);
            await noteContext.SaveChangesAsync();
        }

    }
}
