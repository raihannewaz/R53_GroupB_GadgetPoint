using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using R53_GroupB_GadgetPoint.DAL.Interface;
using R53_GroupB_GadgetPoint.DAL.SpecificQuery;
using R53_GroupB_GadgetPoint.DTOs;
using R53_GroupB_GadgetPoint.HelperAutoMapper;
using R53_GroupB_GadgetPoint.Models;

namespace R53_GroupB_GadgetPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository rpProduct;
        private readonly IMapper mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            rpProduct = productRepository;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDTO>>> GetAll([FromQuery] ProductSpecParams productParams)
        {
            var spec = new SpecificProduct(productParams);

            var countSpec = new SpecificProduct(productParams);

            var totalItems = await rpProduct.CountAsync(countSpec);

            var entities = await rpProduct.ListAsync(spec);

            var data = mapper.Map<IReadOnlyList<ProductDTO>>(entities).ToList();
            return Ok(new Pagination<ProductDTO>(productParams.PageIndex,
                productParams.PageSize, totalItems, data));

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetById(int id)
        {
            var spec = new SpecificProduct(id);

            var p = await rpProduct.GetEntityWithSpec(spec);
            if (p == null)
            {
                return NotFound();
            }
            return new ProductDTO
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Description = p.Description,
                Price = p.Price,
                ProductImage = p.ProductImage,
                Category = p.Category?.CategoryName,
                SubCategory = p.SubCategory?.SubCategoryName,
                Brand = p.Brand?.BrandName,
                IsActive = (bool)p.IsActive
            };
        }



        [HttpPost]
        public async Task<ActionResult> Create([FromForm] Product entity)
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
