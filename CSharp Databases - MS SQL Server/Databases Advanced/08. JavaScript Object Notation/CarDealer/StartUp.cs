using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using AutoMapper;
using CarDealer.Data;
using CarDealer.DTO;
using CarDealer.Models;
using Newtonsoft.Json;

namespace CarDealer
{
    public class StartUp
    {
        private static string ResultDirectoryPath = "../../../Datasets/Results";

        public static void Main(string[] args)
        {
            CarDealerContext context = new CarDealerContext();

            //ResetDatabase(context);

            #region 09.Import Suppliers
            //string inputJson = File.ReadAllText("../../../Datasets/suppliers.json");

            //string result = ImportSuppliers(context, inputJson);
            #endregion

            #region 10. Import Parts
            //string inputJson = File.ReadAllText("../../../Datasets/parts.json");

            //string result = ImportParts(context, inputJson);
            #endregion

            #region 11. Import Cars
            //string inputJson = File.ReadAllText("../../../Datasets/cars.json");

            //string result = ImportCars(context, inputJson);
            #endregion

            #region 12. Import Customers
            //string inputJson = File.ReadAllText("../../../Datasets/customers.json");

            //string result = ImportCustomers(context, inputJson);
            #endregion

            #region 13. Import Sales
            //string inputJson = File.ReadAllText("../../../Datasets/sales.json");

            //string result = ImportSales(context, inputJson);
            #endregion

            #region 14. Export Ordered Customers
            //string json = GetOrderedCustomers(context);

            //EnsureDirectoryExists(ResultDirectoryPath);

            //File.WriteAllText(ResultDirectoryPath + "/ordered-customers.json", json);
            #endregion

            #region 15. Export Cars From Make Toyota
            //string json = GetCarsFromMakeToyota(context);

            //EnsureDirectoryExists(ResultDirectoryPath);

            //File.WriteAllText(ResultDirectoryPath + "/toyota-cars.json", json);
            #endregion

            #region 16. Export Local Suppliers
            //string json = GetLocalSuppliers(context);

            //EnsureDirectoryExists(ResultDirectoryPath);

            //File.WriteAllText(ResultDirectoryPath + "/local-suppliers.json", json);
            #endregion

            #region 17. Export Cars With Their List Of Parts
            //string json = GetCarsWithTheirListOfParts(context);

            //EnsureDirectoryExists(ResultDirectoryPath);

            //File.WriteAllText(ResultDirectoryPath + "/cars-and-parts.json", json);
            #endregion

            #region 18. Export Total Sales By Customer
            //string json = GetTotalSalesByCustomer(context);

            //EnsureDirectoryExists(ResultDirectoryPath);

            //File.WriteAllText(ResultDirectoryPath + "/customers-total-sales.json", json);
            #endregion

            #region 19. Export Sales With Applied Discount
            //string json = GetSalesWithAppliedDiscount(context);

            //EnsureDirectoryExists(ResultDirectoryPath);

            //File.WriteAllText(ResultDirectoryPath + "/sales-discounts.json", json);
            #endregion

            //Console.WriteLine(json);
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
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            List<Supplier> suppliers = JsonConvert.DeserializeObject<List<Supplier>>(inputJson);

            context.Suppliers.AddRange(suppliers);

            context.SaveChanges();

            return $"Successfully imported {suppliers.Count}.";
        }

        //10. Import Parts
        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            List<Part> parts = JsonConvert.DeserializeObject<List<Part>>(inputJson);

            List<int> supplier = context.Suppliers
                .Select(s => s.Id)
                .ToList();

            List<Part> validEntities = parts
                .Where(p => supplier.Contains(p.SupplierId))
                .ToList();

            context.Parts.AddRange(validEntities);

            context.SaveChanges();

            return $"Successfully imported {validEntities.Count}.";
        }

        //11. Import Cars
        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var carDtos = JsonConvert.DeserializeObject<List<CarDTO>>(inputJson);

            var cars = new List<Car>();

            var carParts = new List<PartCar>();

            foreach (var carDTO in carDtos)
            {
                var newCar = new Car()
                {
                    Make = carDTO.Make,
                    Model = carDTO.Model,
                    TravelledDistance = carDTO.TravelledDistance
                };

                cars.Add(newCar);

                foreach (var partId in carDTO.PartsId.Distinct())
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

            return $"Successfully imported {cars.Count}.";
        }

        //12. Import Customers
        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(inputJson);

            context.Customers.AddRange(customers);

            context.SaveChanges();

            return $"Successfully imported {customers.Count}.";
        }

        //13. Import Sales
        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            List<Sale> sales = JsonConvert.DeserializeObject<List<Sale>>(inputJson);

            context.Sales.AddRange(sales);

            context.SaveChanges();

            return $"Successfully imported {sales.Count}.";
        }

        //14. Export Ordered Customers
        public static string GetOrderedCustomers(CarDealerContext context)
        {
            string datеFormat = "dd/MM/yyyy";

            var customers = context
                .Customers
                .OrderBy(c => c.BirthDate)
                .ThenBy(c => c.IsYoungDriver)
                .Select(c => new
                {
                    Name = c.Name,
                    BirthDate = c.BirthDate.ToString(datеFormat, CultureInfo.InvariantCulture),
                    IsYoungDriver = c.IsYoungDriver
                })
                .ToList();

            string json = JsonConvert.SerializeObject(customers, Formatting.Indented);

            return json;
        }

        //15. Export Cars From Make Toyota
        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var cars = context
                .Cars
                .Where(c => c.Make == "Toyota")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .Select(c => new
                {
                    c.Id,
                    c.Make,
                    c.Model,
                    c.TravelledDistance
                })
                .ToList();

            string json = JsonConvert.SerializeObject(cars, Formatting.Indented);

            return json;
        }

        //16. Export Local Suppliers
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context
                .Suppliers
                .Where(s => !s.IsImporter)
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    PartsCount = s.Parts.Count
                })
                .ToList();

            string json = JsonConvert.SerializeObject(suppliers, Formatting.Indented);

            return json;
        }

        //17. Export Cars With Their List Of Parts
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context
                .Cars
                .Select(c => new
                {
                    car = new
                    {
                        c.Make,
                        c.Model,
                        c.TravelledDistance
                    },
                    parts = c.PartCars.Select(x => new
                    {
                        Name = x.Part.Name,
                        Price = x.Part.Price.ToString("F2")
                    })
                })
                .ToList();

            string json = JsonConvert.SerializeObject(cars, Formatting.Indented);

            return json;
        }

        //18. Export Total Sales By Customer
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.
                Customers
                .Where(x => x.Sales.Count > 0)
                .Select(c => new
                {
                    fullName = c.Name,
                    boughtCars = c.Sales.Count,
                    spentMoney = c.Sales.Sum(x => x.Car.PartCars.Sum(y => y.Part.Price))
                })
                .OrderByDescending(x => x.spentMoney)
                .ThenByDescending(x => x.boughtCars)
                .ToList();

            string json = JsonConvert.SerializeObject(customers, Formatting.Indented);

            return json;
        }

        //19. Export Sales With Applied Discount
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context
                .Sales
                .Take(10)
                .Select(s => new
                {
                    car = new
                    {
                        s.Car.Make,
                        s.Car.Model,
                        s.Car.TravelledDistance
                    },
                    customerName = s.Customer.Name,
                    Discount = s.Discount.ToString("F2"),
                    price = s.Car.PartCars.Sum(x => x.Part.Price).ToString("F2"),
                    priceWithDiscount = $"{s.Car.PartCars.Sum(x => x.Part.Price) - (s.Car.PartCars.Sum(x => x.Part.Price) * (s.Discount * 0.01M)):F2}"
                })
                .ToList();

            string json = JsonConvert.SerializeObject(sales, Formatting.Indented);

            return json;
        }
    }
}