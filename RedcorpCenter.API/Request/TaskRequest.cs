using System.ComponentModel.DataAnnotations;

namespace RedcorpCenter.API.Request
{
    public class TaskRequest
    {
        [Microsoft.Build.Framework.Required]
        [MinLength(3)]
        public string Name { get; set; }
        [MaxLength(70)]
        public string Description { get; set; }

        public string StartDate { get; set; }
        public string FinalDate { get; set; }
        [Required]
        public bool IsCompleted { get; set; }
    }
}

