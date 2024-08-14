namespace ChatterBox.DTO.Requests.BoxComment
{
    public class PutBoxCommentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string? ImageUrl { get; set; }
        public string Content { get; set; } 
    }
}
