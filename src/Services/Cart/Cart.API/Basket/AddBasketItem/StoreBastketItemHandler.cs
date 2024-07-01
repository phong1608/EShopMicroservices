using Cart.API.Data;
using Cart.API.DTOs;
using Catalog.gRPC.Protos;
using Discount.gRPC;
using FluentValidation;

public record StoreBasketCommand(CartItemsDTO Item,string UserId) : ICommand<StoreBasketResult>;
public record StoreBasketResult(bool IsSuccess);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Item).NotNull().WithMessage("Cart can not be null");
    }
}

public class StoreBasketCommandHandler : ICommandHandler<StoreBasketCommand,StoreBasketResult>
{
    private readonly IBasketRepository _repository;
    private readonly DiscountProtoService.DiscountProtoServiceClient _discountService;
    private readonly GetProductService.GetProductServiceClient _productServiceClient;
    public StoreBasketCommandHandler(IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountService, GetProductService.GetProductServiceClient productServiceClient)
    {
        _repository = repository;
        _discountService = discountService;
        _productServiceClient = productServiceClient;
    }
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        
        var cartItem = command.Item;
        
        var product = await _productServiceClient.GetProuductInfoAsync(new GetProductRequest { Id=cartItem.ProductId.ToString() });
        cartItem.Price = (decimal)product.Price;
        var coupon = await _discountService.GetDiscountAsync(new GetDiscountRequest { ProductName = product.ProductName });
        cartItem.Price -= coupon.Amount;
       
        await _repository.AddItems(cartItem,command.UserId); 


        return new StoreBasketResult(true);
    }

    
}