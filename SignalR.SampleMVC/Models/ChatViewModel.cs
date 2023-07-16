namespace SignalR.SampleMVC.Models
{
    public class ChatViewModel
    {
        public int MaxRoomAllowed { get; set; }
        public List<ChatRoom> Rooms { get; set; } = new();
        public string? UserId { get; set; }
        public bool AllowAddRoom => Rooms == null || Rooms.Count < MaxRoomAllowed;
    }
}
