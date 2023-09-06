using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using R53_GroupB_GadgetPoint.DAL.Interface;
using R53_GroupB_GadgetPoint.Models;

namespace R53_GroubB_GadgetPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryRepository rpCategory;

        public CategoryController(ICategoryRepository ripository)
        {
            rpCategory = ripository;

        }


        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var entities = await rpCategory.ListAllAsync();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var entity = await rpCategory.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Category entity)
        {
            if (ModelState.IsValid)
            {
                var createdEntity = await rpCategory.CreateAsync(entity);
                return Ok(createdEntity);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Category entity)
        {

            var updatedEntity = await rpCategory.UpdateAsync(id, entity);
            return Ok(updatedEntity);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await rpCategory.GetByIdAsync(id);
            if (entity != null)
            {
                await rpCategory.DeleteAsync(entity);
                return Ok();
            }

            return NotFound();
        }
    }
}
