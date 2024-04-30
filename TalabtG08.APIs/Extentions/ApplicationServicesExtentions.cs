using Microsoft.AspNetCore.Mvc;
using TalabatG08.Core;
using TalabatG08.Core.Repositories;
using TalabatG08.Core.Services;
using TalabatG08.Repository;
using TalabatG08.Service;
using TalabtG08.APIs.Errors;
using TalabtG08.APIs.Helpers;

namespace TalabtG08.APIs.Extentions
{
    public static class ApplicationServicesExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddScoped<IPaymentService,PaymentService>();

            Services.AddScoped<IUnitOfWork,UnitOfWork>();
            Services.AddScoped(typeof(IOrderService), typeof(OrderService));

            //Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));


            Services.AddAutoMapper(typeof(MappingProfiles));

            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actioContext) =>
                {
                    var errors = actioContext.ModelState.Where(E => E.Value.Errors.Count() > 0)
                                                       .SelectMany(E => E.Value.Errors)
                                                       .Select(E => E.ErrorMessage);

                    var ValidationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(ValidationErrorResponse);
                };
            });




            return Services;
        }

    }
}
