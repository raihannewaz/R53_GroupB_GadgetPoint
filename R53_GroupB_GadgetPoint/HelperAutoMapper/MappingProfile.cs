
using AutoMapper;
using R53_GroubB_GadgetPoint.Models;
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

            CreateMap<OrderItem, OrderItemDto>()
                 .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ItemOrdered.ProductItemId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ItemOrdered.ProductName))
                .ForMember(d => d.ProductImage, o => o.MapFrom(s => s.ItemOrdered.PictureUrl));



            CreateMap<Order, OrderToReturnDto>()
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate.UtcDateTime))
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));


        }
    }
}