using System.ComponentModel.DataAnnotations;
namespace RedcorpCenter.API.Request
{
    public class EmployeeRequest
    {
        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        public string Name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
