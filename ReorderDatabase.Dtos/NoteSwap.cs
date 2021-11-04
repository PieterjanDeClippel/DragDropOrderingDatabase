namespace ReorderDatabase.Dtos
{
    public class NoteSwap
    {
        public Note NoteBefore { get; set; }
        public Note Note { get; set; }
        public Note NoteAfter { get; set; }
    }
}
