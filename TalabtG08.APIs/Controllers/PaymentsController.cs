using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using TalabatG08.Core.Entites;
using TalabatG08.Core.Services;
using TalabtG08.APIs.Dtos;
using TalabtG08.APIs.Errors;

namespace TalabtG08.APIs.Controllers
{
   
    public class PaymentsController : ApiBaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper mapper;
        const string endpointSecret = "whsec_1cb985fdce133816864599eb48aac4158795a931924760d9bb499ac7ea11e081";


        public PaymentsController(IPaymentService paymentService,IMapper mapper)
        {
            this._paymentService = paymentService;
            this.mapper = mapper;
        }

        //Create Or Update end point

        [ProducesResponseType(typeof(CustomerBusketDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpPost("{basketId}")]
        [Authorize]
        public async Task<ActionResult<CustomerBusketDto?>> CreateOrUpdatePayment(string basketId)
        {
            var busket =await _paymentService.CreateOUpdatePaymentIntend(basketId);
            if(busket is null) { return BadRequest(new ApiErrorResponse(400, "there is a problem with busket")); }
            var mappedBusket = mapper.Map<CustomerBasket,CustomerBusketDto>(busket);
            return Ok(mappedBusket);  
        }

        [HttpPost("webhook")] // Post => baseUrl/api/Payments/webhook
        public async Task<IActionResult> StripeWebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], endpointSecret);

                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

                // Handle the event
                if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
                {
                    await _paymentService.UpdatePaymentIntendToSucceedOrFailed(paymentIntent.Id,false);
                }
                else if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    await _paymentService.UpdatePaymentIntendToSucceedOrFailed(paymentIntent.Id, true);
                }
                // ... handle other event types
                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }
    }
}
