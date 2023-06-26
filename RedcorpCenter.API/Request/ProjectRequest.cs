using System.ComponentModel.DataAnnotations;
namespace RedcorpCenter.API.Request
{
    public class ProjectRequest
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        [MaxLength(70)]
        public string Description { get; set; }
        public string InitialDate { get; set; }
        public string FinalDate { get; set; }
        public string State { get; set; }
    }
}

