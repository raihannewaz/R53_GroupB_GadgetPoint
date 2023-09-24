using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R53_GroupB_GadgetPoint.Context;
using R53_GroupB_GadgetPoint.DAL.Interface;
using R53_GroupB_GadgetPoint.DAL.Repositories;
using R53_GroupB_GadgetPoint.Models;

namespace R53_GroupB_GadgetPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        public readonly IBasketRepository _rpBasket;


        public BasketController(IBasketRepository basket)
        {
            _rpBasket = basket;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        {
            var basket = await _rpBasket.GetBasketAsync(id);
            return Ok(basket?? new CustomerBasket(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
            var updateBasket = await _rpBasket.UpdateBasketAsync(basket);
            return Ok(updateBasket);
        }


        [HttpDelete]
        public async Task DeleteBasketAync(string id)
        {
            await _rpBasket.DeleteBasketAsync(id);
        }

        [HttpDelete("{id}")]
        public async Task DeleteBasketItem(int id)
        {
            await _rpBasket.DeleteBasketItem(id);          
        }

    }
}
