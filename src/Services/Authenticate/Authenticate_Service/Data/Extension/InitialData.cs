using Authenticate_Service.Models;

namespace Authenticate.API.Data.Extension
{
    public class InitialData
    {
        public static IEnumerable<ApplicationRole> Roles => new List<ApplicationRole>()
        {
            new ApplicationRole(){Id=new Guid("65E791C7-8C92-4A4D-8A8D-EB2011C7C385"),Name="Customer"},
            new ApplicationRole(){Id=new Guid("37863660-53C2-4D8E-A76A-27F078D8EE22"),Name="Admin"}
        };
        
    }
}
