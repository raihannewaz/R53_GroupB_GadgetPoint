
using AutoMapper;
using R53_GroupB_GadgetPoint.DTOs;
using R53_GroupB_GadgetPoint.Models;

namespace R53_GroupB_GadgetPoint.HelperAutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>()
            .ForMember(b => b.Brand, d => d.MapFrom(p => p.Brand.BrandName))
            .ForMember(t => t.Category, d => d.MapFrom(p => p.Category.CategoryName))
            .ForMember(t => t.SubCategory, d => d.MapFrom(p => p.SubCategory.SubCategoryName));

            CreateMap<Address, AddressDTO>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();

            CreateMap<AddressDTO, ShippingAddress>();
        }
    }
}