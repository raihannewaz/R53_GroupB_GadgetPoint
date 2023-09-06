using R53_GroupB_GadgetPoint.Models;
using System.Linq.Expressions;

namespace R53_GroupB_GadgetPoint.DAL.SpecificQuery
{
    public class SpecificProduct : BaseSpecification<Product>
    {
        public SpecificProduct(string sort, int? brandId, int? categoryId, int? subCatId)
            :base(x=>
            (!brandId.HasValue || x.BrandId==brandId) && (!categoryId.HasValue ||x.CategoryId==categoryId) && (!subCatId.HasValue ||x.SubCategoryId==subCatId)
            )
        {
            AddInclude(x => x.Category);
            AddInclude(x => x.SubCategory);
            AddInclude(x => x.Brand);
            AddOrderBy(x=>x.ProductName);

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.ProductName);
                        break;
                }

            }
        }

        public SpecificProduct(int id) : base(x => x.ProductId == id)
        {
            AddInclude(x => x.Category);
            AddInclude(x => x.SubCategory);
            AddInclude(x => x.Brand);
        }
    }
}
