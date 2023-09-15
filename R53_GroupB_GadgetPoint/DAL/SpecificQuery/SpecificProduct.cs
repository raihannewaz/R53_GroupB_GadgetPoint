using R53_GroupB_GadgetPoint.Models;
using System.Linq.Expressions;

namespace R53_GroupB_GadgetPoint.DAL.SpecificQuery
{
    public class SpecificProduct : BaseSpecification<Product>
    {
        public SpecificProduct(ProductSpecParams productParams)
                  : base(x =>
                  (string.IsNullOrEmpty(productParams.Search) || x.ProductName.ToLower().Contains(productParams.Search)) &&
                  (!productParams.BrandId.HasValue || x.BrandId == productParams.BrandId) &&
                  (!productParams.CategoryId.HasValue || x.CategoryId == productParams.CategoryId) &&
                  (!productParams.SubCategoryId.HasValue || x.SubCategoryId == productParams.SubCategoryId)

                  )
        {
            AddInclude(x => x.Category);
            AddInclude(x => x.SubCategory);
            AddInclude(x => x.Brand);
            AddOrderBy(x => x.ProductName);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1),
               productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort))
            {

                switch (productParams.Sort)
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
