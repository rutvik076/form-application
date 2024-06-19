using System.ComponentModel.DataAnnotations;

namespace FormApplication.DTOs
{
    public class CreateQuestionDto : IValidatableObject
    {
        [Required]
        public string Type { get; set; }

        [Required]
        public string QuestionText { get; set; }

        public List<string>? Options { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Type == "Dropdown" || Type == "MultipleChoice")
            {
                if (Options == null || Options.Count == 0)
                {
                    yield return new ValidationResult("The Options field is required for Dropdown and MultipleChoice question types.", new[] { nameof(Options) });
                }
            }
        }
    }
}
