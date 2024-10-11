using BuildingBlocks.Exceptions;

namespace Cart.API.Exception
{
    public class BasketItemNotFoundException :NotFoundException
    {
        public BasketItemNotFoundException(string userName) : base("ProductId", userName) { }
    }
}
