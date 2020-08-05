namespace BookShop.DataProcessor
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Globalization;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    using Data;
    using Data.Models.Enums;
    using DataProcessor.ExportDto;

    using Newtonsoft.Json;

    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportMostCraziestAuthors(BookShopContext context)
        {
            var authors = context
                .Authors
                .Select(a => new
                {
                    AuthorName = a.FirstName + " " + a.LastName,
                    Books = a.AuthorsBooks
                        .OrderByDescending(x => x.Book.Price)
                        .Select(ab => new
                        {
                            BookName = ab.Book.Name,
                            BookPrice = ab.Book.Price.ToString("F2")
                        })
                        .ToList()
                })
                .ToList()
                .OrderByDescending(b => b.Books.Count)
                .ThenBy(a => a.AuthorName)
                .ToList();

            string json = JsonConvert.SerializeObject(authors, Formatting.Indented);

            return json;
        }

        public static string ExportOldestBooks(BookShopContext context, DateTime date)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var books = context
                .Books
                .ToList()
                .Where(x => x.PublishedOn < date && x.Genre == Genre.Science)
                .OrderByDescending(x => x.Pages)
                .ThenByDescending(x => x.PublishedOn)
                .Select(b => new ExportBookDto
                {
                    Pages = b.Pages,
                    Name = b.Name,
                    Date = b.PublishedOn.ToString("d", CultureInfo.InvariantCulture)
                })
                .Take(10)
                .ToList();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ExportBookDto>), new XmlRootAttribute("Books"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            xmlSerializer.Serialize(new StringWriter(stringBuilder), books, namespaces);

            return stringBuilder.ToString().TrimEnd();
        }
    }
}