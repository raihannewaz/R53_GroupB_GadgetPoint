using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using R53_GroupB_GadgetPoint.DAL.Interfaces;
using R53_GroupB_GadgetPoint.Models;

namespace R53_GroupB_GadgetPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;

        public StockController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var entities = await _stockRepository.ListAllAsync();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var entity = await _stockRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Stock entity)
        {
            if (ModelState.IsValid)
            {
                var createdEntity = await _stockRepository.CreateAsync(entity);
                return Ok(createdEntity);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Stock entity)
        {

            var updatedEntity = await _stockRepository.UpdateAsync(id, entity);
            return Ok(updatedEntity);

        }

        [HttpPut("updateQuantity/{id}")]
        public async Task<ActionResult> UpdateQuantity(int id, int entity)
        {

            var updatedEntity = await _stockRepository.UpdateStockQuantityAsync(id, entity);
            return Ok(updatedEntity);

        }



        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await _stockRepository.GetByIdAsync(id);
            if (entity != null)
            {
                await _stockRepository.DeleteAsync(entity);
                return Ok();
            }

            return NotFound();
        }
    }
}
