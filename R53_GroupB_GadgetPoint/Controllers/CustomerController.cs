using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Entity.Context;
using Project_Entity.Models;
using R53_GroupB_GadgetPoint.DAL.Interface;

namespace R53_GroubB_GadgetPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private IGenericRepository<Customer> rpCustomer;

        public CustomerController(IGenericRepository<Customer> ripository)
        {
            rpCustomer = ripository;

        }


        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var entities = await rpCustomer.ListAllAsync();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var entity = await rpCustomer.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> Create(Customer entity)
        {
            if (ModelState.IsValid)
            {
                var createdEntity = await rpCustomer.AddAsync(entity);
                return Ok(createdEntity);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> Update(int id, Customer entity)
        {

            var updatedEntity = await rpCustomer.UpdateAsync(id, entity);
            return Ok(updatedEntity);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> Delete(int id)
        {
            var entity = await rpCustomer.GetByIdAsync(id);
            if (entity != null)
            {
                await rpCustomer.DeleteAsync(entity);
                return Ok();
            }

            return NotFound();
        }
    }
}
