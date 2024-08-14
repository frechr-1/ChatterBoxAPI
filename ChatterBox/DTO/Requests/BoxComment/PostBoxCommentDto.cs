namespace ChatterBox.DTO.Requests.BoxComment
{
    public class PostBoxCommentDto
    {
        public string Name { get; set; } 
        public string? ImageUrl { get; set; }
        public string Content { get; set; }
        public int ChatBoxId { get; set; }
    }
}
