namespace Authenticate.API.Data.DTOs
{
    public class UserCreatedEventDTO
    {
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
    }
}
