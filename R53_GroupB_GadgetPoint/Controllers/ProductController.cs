using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Entity.Models;
using R53_GroupB_GadgetPoint.DAL.Interface;
using R53_GroupB_GadgetPoint.DTOs;

namespace R53_GroupB_GadgetPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository rpProduct;

        public ProductController(IProductRepository productRepository)
        {
            rpProduct = productRepository;
        }


        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> Get()
        {
            var entities = await rpProduct.ListAllAsync();
            return entities.Select(p=>new ProductDTO
            {
                ProductId = p.ProductId,
                ProdcutName = p.ProdcutName,
                Description = p.Description,
                Price = p.Price,
                ProductImage = p.ProductImage,
                Category=p.Category?.CategoryName,
                SubCategory = p.SubCategory?.SubCategoryName,
                Brand=p.Brand?.BrandName }).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var entity = await rpProduct.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromForm]Product entity)
        {
            if (ModelState.IsValid)
            {
                var createdEntity = await rpProduct.CreateAsync(entity);
                return Ok(createdEntity);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Product entity)
        {

            var updatedEntity = await rpProduct.UpdateAsync(id, entity);
            return Ok(updatedEntity);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await rpProduct.GetByIdAsync(id);
            if (entity != null)
            {
                await rpProduct.DeleteAsync(entity);
                return Ok();
            }

            return NotFound();
        }
    }
}
