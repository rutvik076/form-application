using Newtonsoft.Json;

namespace FormApplication.Models
{
    public class Question
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        public string programId { get; set; }
        public string QuestionText { get; set; }
        public string Type { get; set; }
        public virtual List<string> Options { get; set; }
    }

    public class ParagraphQuestion : Question
    {
        public ParagraphQuestion()
        {
            Type = "Paragraph";
        }
    }

    public class YesNoQuestion : Question
    {
        public YesNoQuestion()
        {
            Type = "YesNo";
        }
    }

    public class DropdownQuestion : Question
    {
        public DropdownQuestion()
        {
            Type = "Dropdown";
        }

        public override List<string> Options { get; set; }
    }

    public class MultipleChoiceQuestion : Question
    {
        public MultipleChoiceQuestion()
        {
            Type = "MultipleChoice";
        }

        public override List<string> Options { get; set; }
    }


    public class DateQuestion : Question
    {
        public DateQuestion()
        {
            Type = "Date";
        }
    }

    public class NumberQuestion : Question
    {
        public NumberQuestion()
        {
            Type = "Number";
        }
    }
}
