namespace Cinema.DataProcessor
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using System.Collections.Generic;

    using Data;
    using DataProcessor.ExportDto;

    using Newtonsoft.Json;

    public class Serializer
    {
        public static string ExportTopMovies(CinemaContext context, int rating)
        {
            var movies = context
                .Movies
                .ToList()
                .Where(m => m.Rating >= rating && m.Projections.Any(x => x.Tickets.Count > 0))
                .OrderByDescending(x => x.Rating)
                .ThenByDescending(p => p.Projections.Sum(t => t.Tickets.Sum(pc => pc.Price)))
                .Select(m => new
                {
                    MovieName = m.Title,
                    Rating = m.Rating.ToString("F2"),
                    TotalIncomes = m.Projections.Sum(x => x.Tickets.Sum(y => y.Price)).ToString("F2"),
                    Customers = m.Projections.SelectMany(t => t.Tickets)
                       .Select(c => new
                       {
                           FirstName = c.Customer.FirstName,
                           LastName = c.Customer.LastName,
                           Balance = c.Customer.Balance.ToString("F2")
                       })
                        .OrderByDescending(b => b.Balance)
                        .ThenBy(f => f.FirstName)
                        .ThenBy(l => l.LastName)
                        .ToList()
                })
                .Take(10)
                .ToList();

            string json = JsonConvert.SerializeObject(movies, Formatting.Indented);

            return json;
        }

        public static string ExportTopCustomers(CinemaContext context, int age)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var customers = context
                .Customers
                .ToList()
                .Where(c => c.Age >= age)
                .OrderByDescending(c => c.Tickets.Sum(x => x.Price))
                .Take(10)
                .Select(c => new ExportCustomerDto
                {
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    SpentMoney = c.Tickets.Sum(x => x.Price).ToString("F2"),
                    SpentTime = TimeSpan.FromSeconds(c.Tickets.Sum(st => st.Projection.Movie.Duration.TotalSeconds)).ToString(@"hh\:mm\:ss")
                })
                .ToList();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ExportCustomerDto>), new XmlRootAttribute("Customers"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            xmlSerializer.Serialize(new StringWriter(stringBuilder), customers, namespaces);

            return stringBuilder.ToString().TrimEnd();
        }
    }
}