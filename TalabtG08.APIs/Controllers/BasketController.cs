using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalabatG08.Core.Entites;
using TalabatG08.Core.Repositories;
using TalabtG08.APIs.Dtos;
using TalabtG08.APIs.Errors;

namespace TalabtG08.APIs.Controllers
{
   
    public class BasketController : ApiBaseController
    {
        private readonly IBasketRepository basketRepository;
        private readonly IMapper mapper;

        public BasketController(IBasketRepository basketRepository,IMapper mapper)
        {
            this.basketRepository = basketRepository;
            this.mapper = mapper;
        }

        [HttpGet] //api/buskets ? id as query string
        public async Task<ActionResult<CustomerBasket>> GetCustomerBusket (string id)
        {
            var busket =await basketRepository.GetBasketAsync (id);

            return busket is null ? new CustomerBasket(id) : busket;
        }

        [HttpPost] //post /api/baskets
        public async Task<ActionResult<CustomerBasket>> UpdateBusket (CustomerBusketDto basket)
        {
            var mappedCustomerBusket = mapper.Map<CustomerBusketDto,CustomerBasket>(basket);

            var CreatedOrUpdatedBusket =await basketRepository.UpadteBasketAsync(mappedCustomerBusket);

            if (CreatedOrUpdatedBusket is null) return BadRequest(new ApiErrorResponse(400));

            return Ok(CreatedOrUpdatedBusket);   
        }

        [HttpDelete]  //api/buskets ? id as query string
        public async Task<ActionResult<bool>> DeleteBusket(string id)
        {
           return  await basketRepository.DeleteBasketAsync(id);

           
        }



    }
}
