using System.ComponentModel.DataAnnotations;

namespace RedcorpCenter.API.Request;

public class TeamRequest
{
    [Microsoft.Build.Framework.Required]
    [MinLength(3)]
    public string Name { get; set; }
    [MaxLength(70)]
    public string Description { get; set; }
    public int Id_Employee { get; set; }
    public int Id_Project { get; set; }
    public int Id_Task { get; set; }
}