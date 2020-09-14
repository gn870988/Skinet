using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection @this)
        {
            @this.AddScoped<ITokenService, TokenService>();
            @this.AddScoped<IOrderService, OrderService>();
            @this.AddScoped<IPaymentService, PaymentService>();
            @this.AddScoped<IUnitOfWork, UnitOfWork>();
            @this.AddScoped<IProductRepository, ProductRepository>();
            @this.AddScoped<IBasketRepository, BasketRepository>();
            @this.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            @this.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage);

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            return @this;
        }
    }
}