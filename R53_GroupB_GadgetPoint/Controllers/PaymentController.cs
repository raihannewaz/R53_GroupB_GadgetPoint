using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Entity.Models;
using R53_GroupB_GadgetPoint.DAL.Interface;

namespace R53_GroubB_GadgetPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private IGenericRepository<Payment> rpPayment;

        public PaymentController(IGenericRepository<Payment> ripository)
        {
            rpPayment = ripository;

        }


        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var entities = await rpPayment.ListAllAsync();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var entity = await rpPayment.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<Payment>> Create(Payment entity)
        {
            if (ModelState.IsValid)
            {
                var createdEntity = await rpPayment.AddAsync(entity);
                return Ok(createdEntity);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Payment>> Update(int id, Payment entity)
        {

            var updatedEntity = await rpPayment.UpdateAsync(id, entity);
            return Ok(updatedEntity);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Brand>> Delete(int id)
        {
            var entity = await rpPayment.GetByIdAsync(id);
            if (entity != null)
            {
                await rpPayment.DeleteAsync(entity);
                return Ok();
            }

            return NotFound();
        }
    }
}
