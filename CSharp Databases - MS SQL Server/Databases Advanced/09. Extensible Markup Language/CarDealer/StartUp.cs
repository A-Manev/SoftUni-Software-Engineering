using System.Linq;

namespace CarDealer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;
    using AutoMapper;
    using CarDealer.Data;
    using CarDealer.Dtos.Export;
    using CarDealer.Dtos.Import;
    using CarDealer.Models;

    public class StartUp
    {
        private static string ResultDirectoryPath = "../../../Datasets/Results";

        public static void Main(string[] args)
        {
            CarDealerContext context = new CarDealerContext();

            //ResetDatabase(context);

            #region 09.Import Suppliers
            //string inputXml = File.ReadAllText("../../../Datasets/suppliers.xml");

            //string result = ImportSuppliers(context, inputXml);
            #endregion

            #region 10. Import Parts
            //string inputXml = File.ReadAllText("../../../Datasets/parts.xml");

            //string result = ImportParts(context, inputXml);
            #endregion

            #region 11. Import Cars
            //string inputXml = File.ReadAllText("../../../Datasets/cars.xml");

            //string result = ImportCars(context, inputXml);
            #endregion

            #region 12. Import Customers
            //string inputXml = File.ReadAllText("../../../Datasets/customers.xml");

            //string result = ImportCustomers(context, inputXml);
            #endregion

            #region 13. Import Sales
            //string inputXml = File.ReadAllText("../../../Datasets/sales.xml");

            //string result = ImportSales(context, inputXml);
            #endregion

            #region 14. Export Cars With Distance
            //string xml = GetCarsWithDistance(context);

            //EnsureDirectoryExists(ResultDirectoryPath);

            //File.WriteAllText(ResultDirectoryPath + "/cars.xml", xml);
            #endregion

            #region 15. Export Cars From Make BMW
            //string xml = GetCarsFromMakeBmw(context);

            //EnsureDirectoryExists(ResultDirectoryPath);

            //File.WriteAllText(ResultDirectoryPath + "/bmw-cars.xml", xml);
            #endregion

            #region 16. Export Local Suppliers
            //string xml = GetLocalSuppliers(context);

            //EnsureDirectoryExists(ResultDirectoryPath);

            //File.WriteAllText(ResultDirectoryPath + "/local-suppliers.xml", xml);
            #endregion

            #region 17. Export Cars With Their List Of Parts
            //string xml = GetCarsWithTheirListOfParts(context);

            //EnsureDirectoryExists(ResultDirectoryPath);

            //File.WriteAllText(ResultDirectoryPath + "/cars-and-parts.xml", xml);
            #endregion

            #region 18. Export Total Sales By Customer
            //string xml = GetTotalSalesByCustomer(context);

            //EnsureDirectoryExists(ResultDirectoryPath);

            //File.WriteAllText(ResultDirectoryPath + "/customers-total-sales.xml", xml);
            #endregion

            #region 19. Export Sales With Applied Discount
            string xml = GetSalesWithAppliedDiscount(context);

            EnsureDirectoryExists(ResultDirectoryPath);

            File.WriteAllText(ResultDirectoryPath + "/sales-discounts.xml", xml);
            #endregion

            //Console.WriteLine(xml);
            //Console.WriteLine(result);
        }

        private static void ResetDatabase(CarDealerContext db)
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

        //09. Import Suppliers
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(List<ImportSupplierDto>), new XmlRootAttribute("Suppliers"));

            var suppliersDto = (List<ImportSupplierDto>)xmlSerializer.Deserialize(new StringReader(inputXml));

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ImportSupplierDto, Supplier>();
            });

            IMapper mapper = configuration.CreateMapper();

            var suppliers = mapper.Map<List<Supplier>>(suppliersDto);

            context.Suppliers.AddRange(suppliers);

            context.SaveChanges();

            return $"Successfully imported {suppliers.Count}";
        }

        //10. Import Parts
        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(List<ImportPartDto>), new XmlRootAttribute("Parts"));

            var partsDto = (List<ImportPartDto>)xmlSerializer.Deserialize(new StringReader(inputXml));

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ImportPartDto, Part>();
            });

            IMapper mapper = configuration.CreateMapper();

            var parts = mapper.Map<List<Part>>(partsDto);

            int[] suppliers = context.Suppliers.Select(s => s.Id).ToArray();

            parts = parts.Where(p => suppliers.Contains(p.SupplierId)).ToList();

            context.Parts.AddRange(parts);

            context.SaveChanges();

            return $"Successfully imported {parts.Count}";
        }

        //11. Import Cars
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(List<ImportCarDto>), new XmlRootAttribute("Cars"));

            var carsDto = (List<ImportCarDto>)xmlSerializer.Deserialize(new StringReader(inputXml));

            var partsIdInDb = context.Parts.Select(c => c.Id).ToList();

            var cars = new List<Car>();

            var carParts = new List<PartCar>();

            foreach (var carDTO in carsDto)
            {
                var configuration = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<ImportCarDto, Car>();
                });

                IMapper mapper = configuration.CreateMapper();

                var newCar = mapper.Map<Car>(carDTO);

                var parts = carDTO.PartIds
                   .Where(pdto => partsIdInDb.Contains(pdto.Id))
                   .Select(p => p.Id)
                   .Distinct()
                   .ToList();

                cars.Add(newCar);

                foreach (var partId in parts)
                {
                    var newPartCar = new PartCar
                    {
                        PartId = partId,
                        Car = newCar
                    };

                    carParts.Add(newPartCar);
                }
            }

            context.Cars.AddRange(cars);

            context.PartCars.AddRange(carParts);

            context.SaveChanges();

            return $"Successfully imported {cars.Count}";
        }

        //12. Import Customers
        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(List<ImportCustomerDto>), new XmlRootAttribute("Customers"));

            var customersDto = (List<ImportCustomerDto>)xmlSerializer.Deserialize(new StringReader(inputXml));

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ImportCustomerDto, Customer>();
            });

            IMapper mapper = configuration.CreateMapper();

            var customers = mapper.Map<List<Customer>>(customersDto);

            context.Customers.AddRange(customers);

            context.SaveChanges();

            return $"Successfully imported {customers.Count}";
        }

        //13. Import Sales
        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(List<ImportSaleDto>), new XmlRootAttribute("Sales"));

            var salesDto = (List<ImportSaleDto>)xmlSerializer.Deserialize(new StringReader(inputXml));

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ImportSaleDto, Sale>();
            });

            IMapper mapper = configuration.CreateMapper();

            var sales = mapper.Map<List<Sale>>(salesDto);

            int[] cars = context.Cars.Select(s => s.Id).ToArray();

            sales = sales.Where(p => cars.Contains(p.CarId)).ToList();

            context.Sales.AddRange(sales);

            context.SaveChanges();

            return $"Successfully imported {sales.Count}";
        }

        //14. Export Cars With Distance
        public static string GetCarsWithDistance(CarDealerContext context)
        {
            var stringBuilder = new StringBuilder();

            var cars = context
                .Cars
                .Where(c => c.TravelledDistance > 2_000_000)
                .Select(c => new ExportCarWithDistanceDto
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .Take(10)
                .ToList();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ExportCarWithDistanceDto>), new XmlRootAttribute("cars"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            xmlSerializer.Serialize(new StringWriter(stringBuilder), cars, namespaces);

            return stringBuilder.ToString().TrimEnd();
        }

        //15. Export Cars From Make BMW
        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            var stringBuilder = new StringBuilder();

            var cars = context
                .Cars
                .Where(c => c.Make == "BMW")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .Select(c => new CarFromMakeBMWDto
                {
                    Id = c.Id,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .ToList();


            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<CarFromMakeBMWDto>), new XmlRootAttribute("cars"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            xmlSerializer.Serialize(new StringWriter(stringBuilder), cars, namespaces);

            return stringBuilder.ToString().TrimEnd();
        }

        //16. Export Local Suppliers
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var stringBuilder = new StringBuilder();

            var suppliers = context
                   .Suppliers
                   .Where(s => !s.IsImporter)
                   .Select(s => new LocalSupplierDto
                   {
                       Id = s.Id,
                       Name = s.Name,
                       PartsCount = s.Parts.Count
                   })
                   .ToList();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<LocalSupplierDto>), new XmlRootAttribute("suppliers"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            xmlSerializer.Serialize(new StringWriter(stringBuilder), suppliers, namespaces);

            return stringBuilder.ToString().TrimEnd();
        }

        //17. Export Cars With Their List Of Parts
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var stringBuilder = new StringBuilder();

            var cars = context
                .Cars
                .OrderByDescending(c => c.TravelledDistance)
                .ThenBy(c => c.Model)
                .Select(c => new ExportCarDto
                {

                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance,
                    Parts = c.PartCars
                    .Select(x => new ExportPartDto
                    {
                        Name = x.Part.Name,
                        Price = x.Part.Price
                    })
                    .OrderByDescending(p => p.Price)
                    .ToList()
                })
                .Take(5)
                .ToList();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ExportCarDto>), new XmlRootAttribute("cars"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            xmlSerializer.Serialize(new StringWriter(stringBuilder), cars, namespaces);

            return stringBuilder.ToString().TrimEnd();
        }

        //18. Export Total Sales By Customer
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var stringBuilder = new StringBuilder();

            var customers = context
                .Customers
                .Where(c => c.Sales.Count > 0)
                .Select(c => new TotalSaleByCustomerDto
                {
                    FullName = c.Name,
                    BoughtCars = c.Sales.Count,
                    SpentMoney = c.Sales.Sum(s => s.Car.PartCars.Sum(x => x.Part.Price))
                })
                .OrderByDescending(c => c.SpentMoney)
                .ToList();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<TotalSaleByCustomerDto>), new XmlRootAttribute("customers"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            xmlSerializer.Serialize(new StringWriter(stringBuilder), customers, namespaces);

            return stringBuilder.ToString().TrimEnd();
        }

        //19. Export Sales With Applied Discount
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var stringBuilder = new StringBuilder();

            var sales = context
             .Sales
             .Take(10)
             .Select(s => new SaleWithAppliedDiscount
             {
                 Car = new CarDto
                 {
                     Make = s.Car.Make,
                     Model = s.Car.Model,
                     TravelledDistance = s.Car.TravelledDistance,
                 },
                 CustomerName = s.Customer.Name,
                 Discount = s.Discount,
                 Price = s.Car.PartCars.Sum(x => x.Part.Price),
                 PriceWithDiscount = s.Car.PartCars.Sum(p => p.Part.Price) -
                    s.Car.PartCars.Sum(p => p.Part.Price) * s.Discount / 100
                 /*s.Car.PartCars.Sum(x => x.Part.Price) - (s.Car.PartCars.Sum(x => x.Part.Price) * (s.Discount * 0.01M))*/
             })
             .ToList();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<SaleWithAppliedDiscount>), new XmlRootAttribute("sales"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            xmlSerializer.Serialize(new StringWriter(stringBuilder), sales, namespaces);

            return stringBuilder.ToString().TrimEnd();
        }
    }
}