namespace Notification.API.Model
{
    public class Notifications
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid RecipientId { get; set; }
        public bool IsSeen { get; set; } = false;
        public Notifications(string Title,string Content,Guid RecipientId)
        {
            this.Title = Title;
            this.Content = Content;
            this.RecipientId = RecipientId;
        }
    }
}
