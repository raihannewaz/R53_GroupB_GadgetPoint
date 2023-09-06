using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using R53_GroupB_GadgetPoint.DAL.Interface;
using R53_GroupB_GadgetPoint.DAL.Interfaces;
using R53_GroupB_GadgetPoint.Models;

namespace R53_GroupB_GadgetPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryMethodController : ControllerBase
    {
        private IDeliveryMethodRepository rpDeliveryMethod;

        public DeliveryMethodController(IDeliveryMethodRepository ripository)
        {
            rpDeliveryMethod = ripository;
        }


        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var entities = await rpDeliveryMethod.ListAllAsync();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var entity = await rpDeliveryMethod.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult> Create(DeliveryMethod entity)
        {
            if (ModelState.IsValid)
            {
                var createdEntity = await rpDeliveryMethod.CreateAsync(entity);
                return Ok(createdEntity);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, DeliveryMethod entity)
        {

            var updatedEntity = await rpDeliveryMethod.UpdateAsync(id, entity);
            return Ok(updatedEntity);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await rpDeliveryMethod.GetByIdAsync(id);
            if (entity != null)
            {
                await rpDeliveryMethod.DeleteAsync(entity);
                return Ok();
            }

            return NotFound();
        }
    }
}
