namespace ReorderDatabase.Dtos
{
    public class Note
    {
        public int Id { get; set; }
        public string? Text { get; set; }

        // Ordering
        public int Numerator { get; set; }
        public int Denominator { get; set; }
        public double Order { get; set; }
    }
}
