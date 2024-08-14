namespace ChatterBox.DTO.Requests.ChatBox
{
    public class PutChatBoxDto
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string ImageUrl { get; set; }
        public string Content { get; set; } 
    }
}
