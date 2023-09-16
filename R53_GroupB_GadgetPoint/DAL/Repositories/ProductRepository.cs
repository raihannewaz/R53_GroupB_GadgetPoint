using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using R53_GroupB_GadgetPoint.Context;
using R53_GroupB_GadgetPoint.DAL.Interface;
using R53_GroupB_GadgetPoint.DAL.SpecificQuery;
using R53_GroupB_GadgetPoint.Models;

namespace R53_GroupB_GadgetPoint.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductRepository(StoreContext store, IWebHostEnvironment hostEnvironment)
        {
            _context = store;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<Product> CreateAsync(Product entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (entity.ImageFile != null)
            {
                entity.ProductImage = await UploadImageAsync(entity.ImageFile);
            }

            await _context.Products.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Product> DeleteAsync(Product entity)
        {
            _context.Products.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.Include(c => c.Category)
                .Include(sc => sc.SubCategory)
                .Include(b => b.Brand)
                .FirstOrDefaultAsync(p => p.ProductId == id);

        }


        public async Task<IReadOnlyList<Product>> ListAllAsync()
        {
            return await _context.Products.Include(c => c.Category)
                .Include(sc => sc.SubCategory)
                .Include(b => b.Brand).ToListAsync();
        }

        public async Task<Product> UpdateAsync(int id, Product entity)
        {
            var exentity = await _context.Products.FindAsync(id);
            if (exentity != null)
            {
                if (entity.ImageFile != null)
                {
                    exentity.ProductImage = await UploadImageAsync(entity.ImageFile);
                }

                _context.Entry(exentity).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }
            return exentity;

        }
        private async Task<string> UploadImageAsync(IFormFile imageFile)
        {
            string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "ProductImage");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            var imgUrl = "http://localhost:5250/"+ "ProductImage/" + uniqueFileName;

            return imgUrl;
        }


        private IQueryable<Product> ApplySpec(ISpecification<Product> spec)
        {
            return SpecificationEvaluator<Product>.GetQuery(_context.Products.AsQueryable(), spec);
        }

        public async Task<Product> GetEntityWithSpec(ISpecification<Product> spec)
        {
            return await ApplySpec(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<Product>> ListAsync(ISpecification<Product> spec)
        {
            return await ApplySpec(spec).ToListAsync();
        }

 
        public async Task<int> CountAsync(ISpecification<Product> spec)
        {
            return await ApplySpec(spec).CountAsync();
        }
    }
}
