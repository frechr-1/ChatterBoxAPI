using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ChatterBox.Entities
{
    public class ChatBox
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date {get; set;}
        public string? ImageUrl { get; set; }
        public string Content { get; set; }
        public ICollection<BoxComment> Comments { get; set; }
        public int BoxId { get; set; }
        public Box Box { get; set; }    
        
    }
}
