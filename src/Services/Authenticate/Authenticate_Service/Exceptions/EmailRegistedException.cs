namespace Authenticate.API.Exceptions
{
    public class EmailRegistedException: Exception
    {
        public EmailRegistedException() : base("Email is already registed") { }
    }
}
