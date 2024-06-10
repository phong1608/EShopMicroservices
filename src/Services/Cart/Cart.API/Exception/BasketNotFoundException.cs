using BuildingBlocks.Exceptions;

namespace Cart.API.Exception
{
    public class BasketNotFoundException:NotFoundException
    {
        public BasketNotFoundException(string userName) : base("Basket",userName) { }
    }
}
