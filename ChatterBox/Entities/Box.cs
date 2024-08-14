namespace ChatterBox.Entities
{
    public class Box
    {
        public int Id { get; set; }
        public string BoxName { get; set; }
        public string Agenda { get; set; }
        public ICollection<ChatBox> ChatBoxes { get; set; } 
    }
}
