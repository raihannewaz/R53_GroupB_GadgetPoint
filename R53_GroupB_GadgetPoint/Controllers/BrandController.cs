using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using R53_GroupB_GadgetPoint.DAL.Interface;
using R53_GroupB_GadgetPoint.Models;

namespace R53_GroubB_GadgetPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private IBrandRepository rpBrand;

        public BrandController(IBrandRepository ripository)
        {
            rpBrand = ripository;
        }


        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var entities = await rpBrand.ListAllAsync();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var entity = await rpBrand.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Brand entity)
        {
            if (ModelState.IsValid)
            {
                var createdEntity = await rpBrand.CreateAsync(entity);
                return Ok(createdEntity);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Brand entity)
        {

            var updatedEntity = await rpBrand.UpdateAsync(id, entity);
            return Ok(updatedEntity);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await rpBrand.GetByIdAsync(id);
            if (entity != null)
            {
                await rpBrand.DeleteAsync(entity);
                return Ok();
            }

            return NotFound();
        }
    }
}
