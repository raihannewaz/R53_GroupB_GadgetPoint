using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using R53_GroupB_GadgetPoint.DAL.Interface;
using R53_GroupB_GadgetPoint.Models;

namespace R53_GroubB_GadgetPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private ISubCategoryRepository rpSubCategory;

        public SubCategoryController(ISubCategoryRepository ripository)
        {
            rpSubCategory = ripository;

        }


        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var entities = await rpSubCategory.ListAllAsync();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var entity = await rpSubCategory.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult> Create(SubCategory entity)
        {
            if (ModelState.IsValid)
            {
                var createdEntity = await rpSubCategory.CreateAsync(entity);
                return Ok(createdEntity);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, SubCategory entity)
        {

            var updatedEntity = await rpSubCategory.UpdateAsync(id, entity);
            return Ok(updatedEntity);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await rpSubCategory.GetByIdAsync(id);
            if (entity != null)
            {
                await rpSubCategory.DeleteAsync(entity);
                return Ok();
            }

            return NotFound();
        }
    }
}
