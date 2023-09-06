using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R53_GroupB_GadgetPoint.DAL.Interface;
using R53_GroupB_GadgetPoint.Models;
using System.Linq;

namespace R53_GroubB_GadgetPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private ISupplierRepository rpSupplier;

        public SupplierController(ISupplierRepository ripository)
        {
            rpSupplier = ripository;

        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var entities = await rpSupplier.ListAllAsync();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var entity = await rpSupplier.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Supplier entity)
        {
            if (ModelState.IsValid)
            {
                var createdEntity = await rpSupplier.CreateAsync(entity);
                return Ok(createdEntity);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Supplier entity)
        {

            var updatedEntity = await rpSupplier.UpdateAsync(id, entity);
            return Ok(updatedEntity);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await rpSupplier.GetByIdAsync(id);
            if (entity != null)
            {
                await rpSupplier.DeleteAsync(entity);
                return Ok();
            }

            return NotFound();
        }

    }
}
