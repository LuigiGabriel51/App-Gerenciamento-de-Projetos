namespace ServiceHub.Model
{
    public class MessageModel
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Message { get; set; }
        public byte[] Image { get; set; }
    }
}
