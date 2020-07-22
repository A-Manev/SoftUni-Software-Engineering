namespace ProductShop
{
    using System;
    using System.IO;
    using System.Text;
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using System.Collections.Generic;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using ProductShop.Data;
    using ProductShop.Dtos.Export;
    using ProductShop.Dtos.Import;
    using ProductShop.Models;

    public class StartUp
    {
        private static string ResultDirectoryPath = "../../../Datasets/Results";

        //private static readonly IMapper mapper ;

        public static void Main(string[] args)
        {
            ProductShopContext context = new ProductShopContext();

            //Mapper.Initialize(cfg => cfg.AddProfile<ProductShopProfile>());

            //ResetDatabase(context);

            #region 01. Import Users
            //var inputXml = XDocument.Load(@"../../../Datasets/users.xml").ToString();

            //string result = ImportUsers(context, inputXml);
            #endregion

            #region 02. Import Products
            //var inputXml = XDocument.Load(@"../../../Datasets/products.xml").ToString();
            //var inputXml = File.ReadAllText(@"./../../../Datasets/products.xml");

            //string result = ImportProducts(context, inputXml);
            #endregion

            #region 03. Import Categories
            //var inputXml = XDocument.Load(@"../../../Datasets/categories.xml").ToString();

            //string result = ImportCategories(context, inputXml);
            #endregion

            #region 04. Import Categories and Products
            //var inputXml = XDocument.Load(@"../../../Datasets/categories-products.xml").ToString();

            //string result = ImportCategoryProducts(context, inputXml);
            #endregion

            #region 05. Export Products In Range
            //string xml = GetProductsInRange(context);

            //EnsureDirectoryExists(ResultDirectoryPath);

            //File.WriteAllText(ResultDirectoryPath + "/products-in-range.xml", xml);
            #endregion

            #region 06. Export Sold Products
            //string xml = GetSoldProducts(context);

            //EnsureDirectoryExists(ResultDirectoryPath);

            //File.WriteAllText(ResultDirectoryPath + "/users-sold-products.xml", xml);
            #endregion

            #region 07. Export Categories By Products Count
            //string xml = GetCategoriesByProductsCount(context);

            //EnsureDirectoryExists(ResultDirectoryPath);

            //File.WriteAllText(ResultDirectoryPath + "/categories-by-products.xml", xml);
            #endregion

            #region 08. Export Users and Products
            //string xml = GetUsersWithProducts(context);

            //EnsureDirectoryExists(ResultDirectoryPath);

            //File.WriteAllText(ResultDirectoryPath + "/users-and-products.xml", xml);
            #endregion

            //Console.WriteLine(xml);
        }

        private static void ResetDatabase(ProductShopContext db)
        {
            db.Database.EnsureDeleted();
            Console.WriteLine("Database was successfully deleted!");
            db.Database.EnsureCreated();
            Console.WriteLine("Database was successfully created!");
        }

        private static void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        //01. Import Users
        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(List<UserDto>), new XmlRootAttribute("Users"));

            var usersDto = (List<UserDto>)xmlSerializer.Deserialize(new StringReader(inputXml));

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDto, User>();
            });

            IMapper mapper = config.CreateMapper();

            var users = mapper.Map<List<User>>(usersDto);

            context.Users.AddRange(users);

            context.SaveChanges();

            return $"Successfully imported {users.Count}";
        }

        //02. Import Products
        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(List<ProductDto>), new XmlRootAttribute("Products"));

            var productsDto = (List<ProductDto>)xmlSerializer.Deserialize(new StringReader(inputXml));

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductDto, Product>();
            });

            IMapper mapper = config.CreateMapper();

            var products = mapper.Map<List<Product>>(productsDto);

            context.Products.AddRange(products);

            context.SaveChanges();

            return $"Successfully imported {products.Count}";
        }

        //03. Import Categories
        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(List<CategoryDto>), new XmlRootAttribute("Categories"));

            var categoriesDto = (List<CategoryDto>)xmlSerializer.Deserialize(new StringReader(inputXml));

            categoriesDto = categoriesDto.Where(x => x.Name != null).ToList();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CategoryDto, Category>();
            });

            IMapper mapper = config.CreateMapper();

            var categories = mapper.Map<List<Category>>(categoriesDto);

            context.Categories.AddRange(categories);

            context.SaveChanges();

            return $"Successfully imported {categories.Count}"; ;
        }

        //04. Import Categories and Products
        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(List<CategoryProductDto>), new XmlRootAttribute("CategoryProducts"));

            var categoriesIds = context.Categories
                .Select(c => c.Id)
                .ToList();

            var productsIds = context.Products
                .Select(p => p.Id)
                .ToList();

            var categoryProductsDto = (List<CategoryProductDto>)xmlSerializer.Deserialize(new StringReader(inputXml));

            categoryProductsDto = categoryProductsDto
                .Where(cp => categoriesIds.Contains(cp.CategoryId) && productsIds.Contains(cp.ProductId))
                .ToList();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CategoryProductDto, CategoryProduct>();
            });

            IMapper mapper = config.CreateMapper();

            var categoryProducts = mapper.Map<List<CategoryProduct>>(categoryProductsDto);

            context.CategoryProducts.AddRange(categoryProducts);

            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Count}";
        }

        //05. Export Products In Range
        public static string GetProductsInRange(ProductShopContext context)
        {
            var stringBuilder = new StringBuilder();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductInRangeDto>()
                .ForMember(x => x.BuyerFullName, y => y.MapFrom(p => $"{p.Buyer.FirstName} {p.Buyer.LastName}"));
            });

            var products = context
                .Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .Take(10)
                .ProjectTo<ProductInRangeDto>(config)
                .ToList();

            //XmlWriterSettings settings = new XmlWriterSettings();
            //settings.Encoding = Encoding.GetEncoding("UTF-8");

            var xmlSerializer = new XmlSerializer(typeof(List<ProductInRangeDto>), new XmlRootAttribute("Products"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            xmlSerializer.Serialize(new StringWriter(stringBuilder), products, namespaces);

            //XmlWriter writer = XmlWriter.Create("books.xml", settings);
            //xmlSerializer.Serialize(writer, products, namespaces);

            return stringBuilder.ToString().TrimEnd();
        }

        //06. Export Sold Products
        public static string GetSoldProducts(ProductShopContext context)
        {
            var stringBuilder = new StringBuilder();

            var users = context
              .Users
              .Where(u => u.ProductsSold.Any(/*x => x.Buyer != null*/))
              .Select(u => new SoldProductDto
              {
                  FirstName = u.FirstName,
                  LastName = u.LastName,
                  SoldProducts = u.ProductsSold
                                  //.Where(x => x.Buyer != null)
                                  .Select(x => new UserSoldProductDto
                                  {
                                      Name = x.Name,
                                      Price = x.Price
                                  })
                                  .ToList()
              })
              .OrderBy(x => x.LastName)
              .ThenBy(x => x.FirstName)
              .Take(5)
              .ToList();

            var xmlSerializer = new XmlSerializer(typeof(List<SoldProductDto>), new XmlRootAttribute("Users"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            xmlSerializer.Serialize(new StringWriter(stringBuilder), users, namespaces);

            return stringBuilder.ToString().TrimEnd();
        }

        // 07. Export Categories By Products Count
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var stringBuilder = new StringBuilder();

            var categories = context
                .Categories
                .Select(c => new CategoryByProductDto
                {
                    Name = c.Name,
                    Count = c.CategoryProducts.Count,
                    AveragePrice = c.CategoryProducts.Select(x => x.Product).Average(x => x.Price),
                    TotalRevenue = c.CategoryProducts.Select(x => x.Product).Sum(x => x.Price),
                })
                .OrderByDescending(c => c.Count)
                .ThenBy(x => x.TotalRevenue)
                .ToList();

            var xmlSerializer = new XmlSerializer(typeof(List<CategoryByProductDto>), new XmlRootAttribute("Categories"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            xmlSerializer.Serialize(new StringWriter(stringBuilder), categories, namespaces);

            return stringBuilder.ToString().TrimEnd();
        }

        //08. Export Users and Products
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var stringBuilder = new StringBuilder();

            var users = context
                .Users
                .AsEnumerable()
                .Where(u => u.ProductsSold.Any())
                .OrderByDescending(u => u.ProductsSold.Count)
                .Select(u => new ExportUserDto
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age,
                    SoldProducts = new ExportSoldProductDto
                    {
                        Count = u.ProductsSold.Count,
                        Products = u.ProductsSold
                                                 .Select(p => new ExportProductDto
                                                 {
                                                     Name = p.Name,
                                                     Price = p.Price
                                                 })
                                                 .OrderByDescending(p => p.Price)
                                                 .ToList()
                    }
                })
                .Take(10)
                .ToList();

            var resultUsers = new ExportUserAndProductDto
            {
                UsersCount = context.Users.Count(p => p.ProductsSold.Any()),
                Users = users
            };

            var xmlSerializer = new XmlSerializer(typeof(ExportUserAndProductDto), new XmlRootAttribute("Users"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            xmlSerializer.Serialize(new StringWriter(stringBuilder), resultUsers, namespaces);

            return stringBuilder.ToString().TrimEnd();
        }
    }
}