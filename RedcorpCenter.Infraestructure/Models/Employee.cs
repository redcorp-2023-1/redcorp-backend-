using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedcorpCenter.Infraestructure.Models
{
    public class Employee
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        public string? Name { get; set; }
        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        public string? last_name { get; set; }
        [Required]
        [MaxLength(8)]
        [MinLength(8)]
        public string? dni { get; set; }
        [Required]
        public string? email { get; set; }
        [Required]
        public string? password { get; set; }
        [Required]
        public string? area { get; set; }
        [Required]
        public string? cargo { get; set; }   

        public string? photo { get; set; }
        public bool IsActive { get; set; }

        public string? Roles { get; set; }
    }
}
