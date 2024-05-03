using System.ComponentModel.DataAnnotations;

namespace RedcorpCenter.API.Request
{
    public class EmployeeRequestPut
    {

        public string? Name { get; set; }

        public string? last_name { get; set; }


        public string? dni { get; set; }

        public string? email { get; set; }

        public string? area { get; set; }
        public string? cargo { get; set; }

        public string? photo { get; set; }

    }
}
