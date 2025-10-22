// ChatGPT (GPT-5 Thinking)
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class TheModel
    {
        [Required(ErrorMessage = "Phrase is required.")]
        [StringLength(25, MinimumLength = 5, ErrorMessage = "Phrase must be between 5 and 25 characters.")]
        public string? Phrase { get; set; }

        // Resultados para la vista
        public List<(char ch, int count)> Counts { get; set; } = new();
        public string LowerNoSpaces { get; set; } = string.Empty;
        public string UpperNoSpaces { get; set; } = string.Empty;
        public bool Submitted { get; set; }
    }
}
