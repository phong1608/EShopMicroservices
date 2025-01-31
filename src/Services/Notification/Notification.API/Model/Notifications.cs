﻿namespace Notification.API.Model
{
    public class Notifications
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Type { get; set; }
        public string Content { get; set; }
        public Guid RecipientId { get; set; }
        public bool IsSeen { get; set; } = false;
        public Notifications(string Type, string Content,Guid RecipientId)
        {
            this.Type = Type;
            this.Content = Content;
            this.RecipientId = RecipientId;
            
        }
    }
}
