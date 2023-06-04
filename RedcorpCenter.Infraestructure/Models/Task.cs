namespace RedcorpCenter.Infraestructure.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
        public bool IsCompleted { get; set; }
        
        public bool IsActive { get; set; }
    }
}

