using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Entity.Context;
using Project_Entity.Models;
using R53_GroupB_GadgetPoint.DAL.Interface;
using System.Linq;

namespace R53_GroubB_GadgetPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private IGenericRepository<Supplier> rpSupplier;

        public SupplierController(IGenericRepository<Supplier> ripository)
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
        public async Task<ActionResult<Supplier>> Create(Supplier entity)
        {
            if (ModelState.IsValid)
            {
                var createdEntity = await rpSupplier.AddAsync(entity);
                return Ok(createdEntity);
            }
                return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Supplier>> Update(int id, Supplier entity)
        {
          
            var updatedEntity = await rpSupplier.UpdateAsync(id, entity);
            return Ok(updatedEntity);
            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Supplier>> Delete(int id)
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
