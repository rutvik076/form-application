using System.ComponentModel.DataAnnotations;

namespace FormApplication.DTOs
{
    public class UpdateQuestionDto : IValidatableObject
    {
        [Required]
        public string Type { get; set; }

        [Required]
        public string QuestionText { get; set; }

        public List<string>? Options { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            if (Options != null && (Options.Count == 0 || Options.TrueForAll(opt => string.IsNullOrWhiteSpace(opt))))
            {
                validationResults.Add(new ValidationResult("Options must contain at least one non-empty string if provided."));
            }

            return validationResults;
        }
    }
}
