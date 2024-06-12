using Discount.gRPC.Data;
using Discount.gRPC.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Services
{
    public class DiscountService :DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly DiscountContext _context;
        private readonly ILogger<DiscountService> _logger;
        public DiscountService(ILogger<DiscountService> logger,DiscountContext context)
        {
            _logger = logger;
            _context = context;
        }
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _context.Coupons.FirstOrDefaultAsync(c=>c.ProductName == request.ProductName);
            if (coupon == null)
            {
                coupon = new Coupon { ProductName="No Discount",Amount=0,Description="No Discount Desc"};
            }
            _logger.LogInformation("Discount is retrieved for ProductName:{productName},Amount:{amount}",coupon.ProductName,coupon.Amount);
            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if(coupon == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));
            }
            _context.Coupons.Add(coupon);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Discount is successfully created.ProductName:{productName},Amount:{amount}", coupon.ProductName, coupon.Amount);
            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount for {request.ProductName} not found"));

            }
            _context.Coupons.Remove(coupon);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Discount is successfully removed.ProductName:{productName}", coupon.ProductName);
            return new DeleteDiscountResponse { IsSuccess=true};
        }
        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));
            }
            _context.Coupons.Update(coupon);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Discount is successfully updated .ProductName:{productName},Amount:{amount}", coupon.ProductName, coupon.Amount);
            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;

        }
    }
}
