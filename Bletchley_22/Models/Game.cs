namespace Bletchley_22.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Code { get; set; } 
        public bool AllowRepeats { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Guess> Guesses { get; set; }
    }
}
