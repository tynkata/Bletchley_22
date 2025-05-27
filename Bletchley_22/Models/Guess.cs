namespace Bletchley_22.Models
{
    public class Guess
    {
        public int Id { get; set; }
        public string CodeBreakerGuess { get; set; } 
        public int CorrectNumbers { get; set; }
        public int CorrectPositions { get; set; }
        public DateTime Timestamp { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}
