namespace BookShop.DataProcessor
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Globalization;
    using System.Xml.Serialization;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Data;
    using Data.Models;
    using Data.Models.Enums;
    using ImportDto;
    
    using Newtonsoft.Json;

    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedBook
            = "Successfully imported book {0} for {1:F2}.";

        private const string SuccessfullyImportedAuthor
            = "Successfully imported author - {0} with {1} books.";

        public static string ImportBooks(BookShopContext context, string xmlString)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var xmlSerializer = new XmlSerializer(typeof(List<ImportBookDto>), new XmlRootAttribute("Books"));

            var bookDtos = (List<ImportBookDto>)xmlSerializer.Deserialize(new StringReader(xmlString));

            List<Book> books = new List<Book>();

            foreach (var bookDto in bookDtos)
            {
                if (!IsValid(bookDto))
                {
                    stringBuilder.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime bookPublishedOn;

                bool isBookPublishedOn = DateTime.TryParseExact(bookDto.PublishedOn, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out bookPublishedOn);

                if (!isBookPublishedOn)
                {
                    stringBuilder.AppendLine(ErrorMessage);
                    continue;
                }

                Book book = new Book()
                {
                    Name = bookDto.Name,
                    Genre = (Genre)bookDto.Genre,
                    Price = bookDto.Price,
                    Pages = bookDto.Pages,
                    PublishedOn = bookPublishedOn
                };

                books.Add(book);
                stringBuilder.AppendLine(string.Format(SuccessfullyImportedBook, book.Name, book.Price));
            }

            context.Books.AddRange(books);
            context.SaveChanges();

            return stringBuilder.ToString().TrimEnd();
        }

        public static string ImportAuthors(BookShopContext context, string jsonString)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var authorDtos = JsonConvert.DeserializeObject<List<ImportAuthorDto>>(jsonString);

            List<Author> authors = new List<Author>();

            List<AuthorBook> authorBooks = new List<AuthorBook>();

            int[] books = context.Books.Select(x => x.Id).ToArray();

            foreach (var authorDto in authorDtos)
            {
                if (!IsValid(authorDto))
                {
                    stringBuilder.AppendLine(ErrorMessage);
                    continue;
                }

                if (authors.Any(x => x.Email == authorDto.Email))
                {
                    stringBuilder.AppendLine(ErrorMessage);
                    continue;
                }

                Author author = new Author()
                {
                    FirstName = authorDto.FirstName,
                    LastName = authorDto.LastName,
                    Phone = authorDto.Phone,
                    Email = authorDto.Email
                };

                int booksCount = 0;

                foreach (var bookId in authorDto.Books)
                {
                    if (bookId.Id == null || !books.Contains((int)bookId.Id) )
                    {
                        continue;
                    }

                    AuthorBook authorBook = new AuthorBook()
                    {
                        Author = author,
                        BookId = (int)bookId.Id
                    };

                    authorBooks.Add(authorBook);
                    booksCount++;
                }

                if (booksCount != 0)
                {
                    authors.Add(author);
                    stringBuilder.AppendLine(string.Format(SuccessfullyImportedAuthor, $"{author.FirstName} {author.LastName}", booksCount));
                }
                else
                {
                    stringBuilder.AppendLine(ErrorMessage);
                }
            }

            context.Authors.AddRange(authors);
            context.AuthorsBooks.AddRange(authorBooks);
            context.SaveChanges();

            return stringBuilder.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}