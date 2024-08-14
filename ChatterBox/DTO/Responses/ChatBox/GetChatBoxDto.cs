namespace ChatterBox.DTO.Responses.ChatBox
{
    public class GetChatBoxDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string ImageUrl { get; set; }
        public string Content { get; set; }
    }
}
