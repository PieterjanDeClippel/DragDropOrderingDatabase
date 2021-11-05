namespace ReorderDatabase.Dtos
{
    public class Note
    {
        public int Id { get; set; }
        public string? Text { get; set; }

        // Ordering
        public ulong Numerator { get; set; }
        public ulong Denominator { get; set; }
        public decimal Order { get; set; }
    }
}
