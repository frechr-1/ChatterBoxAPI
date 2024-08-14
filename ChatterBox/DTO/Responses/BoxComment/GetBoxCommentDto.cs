namespace ChatterBox.DTO.Responses.BoxComment
{
    public class GetBoxCommentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string ImageUrl { get; set; }
        public string Content { get; set; }

        public int ChatBoxId { get; set; }
    }
}
