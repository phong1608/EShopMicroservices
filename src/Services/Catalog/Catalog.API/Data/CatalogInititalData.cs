using Catalog.API.Models;
using Marten;
using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInititalData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();
            if(await session.Query<Product>().AnyAsync(cancellation))
            {
                return;
            }
            session.Store<Product>(GetPreconfiguredProduct());
            await session.SaveChangesAsync(cancellation);
        }
        private static IEnumerable<Product> GetPreconfiguredProduct()=> new List<Product>()
        {

            new Product()
            {
                Id = new Guid("81f4d58b-1a2c-4b68-88b6-66efa3c135a8"),
                Name = "IPhone X", Description = "The iPhone is a smartphone made by Apple that combines a computer, iPod, digital camera and cellular phone into one device with a touchscreen interface",
                Price = 950,
                ImageFile="IphoneX.png",
                Stock=100,
                Category = new List<string> { "Smart Phone", "New Arrival" }
            },
            new Product()
            { 
                Id = new Guid("bad5acb2-eea0-4b33-aca8-269e10f70d00"),
                Name = "Samsung Galaxy S21",
                Description = "The Samsung Galaxy S21 is the latest flagship smartphone from Samsung, featuring a powerful camera system and fast performance.",
                Price = 1099,
                ImageFile="Samsung Galaxy S21.png",
                Stock=100,
                Category = new List<string> { "Smart Phone", "Flagship" }
            },
            new Product()
            {
                Id = new Guid("95165c4e-4757-48f0-9e59-d372ffbd5b9b"),
                Name = "MacBook Air",
                Description = "The MacBook Air is a thin, lightweight laptop computer designed and manufactured by Apple Inc.",
                Price = 999,
                ImageFile="MacBook Air.png",
                Stock=100,
                Category = new List<string> { "Laptop", "Ultrabook" }
            },
            new Product() { 
                Id = new Guid("246d7900-a91f-44ba-9a1a-66776725ccab"),
                Name = "Sony PlayStation 5",
                Description = "The PlayStation 5 is a home video game console developed by Sony Interactive Entertainment.",
                Price = 500,
                ImageFile="Sony PlayStation 5.png",
                Stock=100,
                Category = new List<string> { "Gaming Console", "Next Gen" }
            },
            new Product()
            {
                Id = new Guid("71cd4958-b93e-46b0-aa0f-b096cc8ca5d6"),
                Name = "Amazon Echo Dot",
                Description = "The Amazon Echo Dot is a smart speaker with Alexa, perfect for any room.",
                Price = 50,
                ImageFile="Amazon Echo Dot.png",
                Stock=100,
                Category = new List<string> { "Smart Speaker", "Home Automation" }
            },
            new Product()
            { 
                Id = new Guid("ee689483-fac1-4c6c-86b5-8afea2d6f15f"),
                Name = "DJI Mavic Air 2",
                Description = "The DJI Mavic Air 2 is a foldable drone that offers 4K video at 60fps and 48MP photos.",
                Price = 8000,
                ImageFile="DJI Mavic Air.png",
                Stock=100,
                Category = new List<string> { "Drone", "Aerial Photography"}
            }
        };
    }
    
}
