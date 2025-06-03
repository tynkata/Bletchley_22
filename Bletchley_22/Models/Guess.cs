using System;
using System.ComponentModel.DataAnnotations;

namespace Bletchley_22.Models
{
    public class Guess
    {
        public int Id { get; set; }

        [Required]
        public string Code { get; set; } = "";

        public int Correct { get; set; }

        public int Misplaced { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
