namespace ProductShop
{
    using ProductShop.Models;
    using ProductShop.Dtos.Import;

    using AutoMapper;

    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            //Users
            this.CreateMap<UserDto, User>();

            //Products
            this.CreateMap<ProductDto, Product>();

            //Categories
            this.CreateMap<CategoryDto, Category>();

            //CategoryProducts
            this.CreateMap<CategoryProductDto, CategoryProduct>();
        }
    }
}
