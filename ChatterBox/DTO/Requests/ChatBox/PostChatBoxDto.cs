namespace ChatterBox.DTO.Requests.ChatBox
{
    public class PostChatBoxDto
    {
        public string Name { get; set; } 
        public string? ImageUrl { get; set; }
        public string Content { get; set; }
        public int BoxId { get; set; }
    }
}
