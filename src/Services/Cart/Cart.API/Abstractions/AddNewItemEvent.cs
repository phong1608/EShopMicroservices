namespace Cart.API.Abstractions
{
    public class AddNewItemEvent :INotification
    {
        public AddNewItemEvent(string UserId)
        {
            UserId = userId;

        }
        public string userId { get; set; } = null!;

    }
}
