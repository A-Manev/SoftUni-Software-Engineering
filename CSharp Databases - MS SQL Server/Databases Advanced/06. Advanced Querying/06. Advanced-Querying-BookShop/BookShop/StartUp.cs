namespace BookShop
{
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Internal;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            //DbInitializer.ResetDatabase(db);

            ////1. Age Restriction
            //string inputCommand = Console.ReadLine();

            //string result = GetBooksByAgeRestriction(db, inputCommand);

            ////2. Golden Books
            //string result = GetGoldenBooks(db);

            ////3. Books by Price
            //string result = GetBooksByPrice(db);

            ////4.Not Released In
            //int inputYear = int.Parse(Console.ReadLine());

            //string result = GetBooksNotReleasedIn(db, inputYear);

            ////5.Book Titles by Category
            //string inputCategorys = Console.ReadLine();

            //string result = GetBooksByCategory(db, inputCategorys);

            ////6. Released Before Date
            //string inputDate = Console.ReadLine();

            //string result = GetBooksReleasedBefore(db, inputDate);

            ////7. Author Search
            //string inputString = Console.ReadLine();

            //string result = GetAuthorNamesEndingIn(db, inputString);

            ////8. Book Search
            //string inputString = Console.ReadLine();

            //string result = GetBookTitlesContaining(db, inputString);

            ////9. Book Search by Author
            //string inputString = Console.ReadLine();

            //string result = GetBooksByAuthor(db, inputString);

            ////10. Count Books
            //int inputLenght = int.Parse(Console.ReadLine());

            //int result = CountBooks(db, inputLenght);

            ////11.Total Book Copies

            //string result = CountCopiesByAuthor(db);

            ////12.Profit by Category

            //string result = GetTotalProfitByCategory(db);

            ////13. Most Recent Books
            //string result = GetMostRecentBooks(db);

            ////14. Increase Prices

            //IncreasePrices(db);

            //15. Remove Books

            int result = RemoveBooks(db);

            Console.WriteLine(result);
        }

        //1. Age Restriction
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var books = context
               .Books
               .Where(b => b.AgeRestriction.ToString().ToLower() == command.ToLower())
               .Select(b => new
               {
                   b.Title
               })
               .OrderBy(x => x.Title)
               .ToList();

            foreach (var book in books)
            {
                stringBuilder.AppendLine(book.Title);
            }

            return stringBuilder.ToString().TrimEnd();
        }

        //2. Golden Books
        public static string GetGoldenBooks(BookShopContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var books = context
                .Books
                .Where(b => b.EditionType.ToString() == "Gold" && b.Copies < 5000)
                .OrderBy(b => b.BookId)
                .Select(b => new
                {
                    b.Title
                })
                .ToList();

            foreach (var book in books)
            {
                stringBuilder.AppendLine(book.Title);
            }

            return stringBuilder.ToString().TrimEnd();
        }

        //3. Books by Price
        public static string GetBooksByPrice(BookShopContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var books = context
                .Books
                .Where(b => b.Price > 40)
                .Select(b => new
                {
                    b.Title,
                    b.Price
                })
                .OrderByDescending(b => b.Price)
                .ToList();

            foreach (var book in books)
            {
                stringBuilder.AppendLine($"{book.Title} - ${book.Price:F2}");
            }

            return stringBuilder.ToString().TrimEnd();
        }

        //4. Not Released In
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var books = context
                .Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b => new
                {
                    b.Title
                })
                .ToList();

            foreach (var book in books)
            {
                stringBuilder.AppendLine(book.Title);
            }

            return stringBuilder.ToString().TrimEnd();
        }

        //5. Book Titles by Category
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            StringBuilder stringBuilder = new StringBuilder();

            string[] categories = input
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(c => c.ToLower())
                .ToArray();

            List<string> allBooksTitles = new List<string>();

            foreach (var category in categories)
            {
                List<string> currentBooksCategories = context
                    .Books
                    .Where(b => b.BookCategories.Any(bc => bc.Category.Name.ToLower() == category))
                    .Select(b => b.Title)
                    .ToList();

                allBooksTitles.AddRange(currentBooksCategories);
            }

            foreach (var abook in allBooksTitles.OrderBy(x => x))
            {
                stringBuilder.AppendLine(abook);
            }

            return stringBuilder.ToString().TrimEnd();
        }

        //6. Released Before Date
        public static string GetBooksReleasedBefore(BookShopContext context, string inputDate)
        {
            StringBuilder stringBuilder = new StringBuilder();

            DateTime date = DateTime.ParseExact(inputDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var books = context
                .Books
                .Where(b => b.ReleaseDate < date)
                .Select(b => new
                {
                    b.Title,
                    b.EditionType,
                    b.Price,
                    b.ReleaseDate
                })
                .OrderByDescending(b => b.ReleaseDate)
                .ToList();

            foreach (var book in books)
            {
                stringBuilder.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:F2}");
            }

            return stringBuilder.ToString().TrimEnd();
        }

        //7. Author Search
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var authors = context
                .Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => new
                {
                    a.FirstName,
                    a.LastName
                })
                .OrderBy(a => a.FirstName)
                .ThenBy(a => a.LastName)
                .ToList();

            foreach (var author in authors)
            {
                stringBuilder.AppendLine($"{author.FirstName} {author.LastName}");
            }

            return stringBuilder.ToString().TrimEnd();
        }

        //8. Book Search
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var books = context
                .Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .Select(b => new
                {
                    b.Title
                })
                .OrderBy(b => b.Title)
                .ToList();

            foreach (var book in books)
            {
                stringBuilder.AppendLine(book.Title);
            }

            return stringBuilder.ToString().TrimEnd();
        }

        //9. Book Search by Author
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var authors = context
                .Books
                .Select(b => new
                {
                    b.BookId,
                    b.Title,
                    b.Author.FirstName,
                    b.Author.LastName
                })
                .Where(a => a.LastName.ToLower().StartsWith(input.ToLower()))
                .OrderBy(b => b.BookId)
                .ToList();

            foreach (var book in authors)
            {
                stringBuilder.AppendLine($"{book.Title} ({book.FirstName} {book.LastName})");
            }

            return stringBuilder.ToString().TrimEnd();
        }

        //10. Count Books
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var books = context
                .Books
                .Select(b => new
                {
                    b.Title
                })
                .Where(b => b.Title.Length > lengthCheck)
                .ToList();

            return books.Count;
        }

        //11. Total Book Copies
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var authors = context
                .Authors
                .Select(a => new
                {
                    a.FirstName,
                    a.LastName,
                    CopiesCount = a.Books.Select(b => b.Copies).Sum()
                })
                .OrderByDescending(b => b.CopiesCount)
                .ToList();

            foreach (var author in authors)
            {
                stringBuilder.AppendLine($"{author.FirstName} {author.LastName} - {author.CopiesCount}");
            }

            return stringBuilder.ToString().TrimEnd();
        }

        //12. Profit by Category
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var categories = context
                .Categories
                .Select(c => new
                {
                    c.Name,
                    TotalProfit = c.CategoryBooks
                                   .Select(bc => bc.Book.Price * bc.Book.Copies).Sum()
                })
                .OrderByDescending(c => c.TotalProfit)
                .ThenBy(c => c.Name)
                .ToList();

            foreach (var category in categories)
            {
                stringBuilder.AppendLine($"{category.Name} ${category.TotalProfit:F2}");
            }

            return stringBuilder.ToString().TrimEnd();
        }

        //13. Most Recent Books
        public static string GetMostRecentBooks(BookShopContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var categories = context
                .Categories
                .Select(c => new
                {
                    c.Name,
                    MostRecsentBooks = c.CategoryBooks
                                          .OrderByDescending(cb => cb.Book.ReleaseDate)
                                          .Take(3)
                                          .Select(bc => new
                                          {
                                              Title = bc.Book.Title,
                                              ReleaseDate = bc.Book.ReleaseDate.Value.Year
                                          })
                                          .ToList()
                })
                .OrderBy(c => c.Name)
                .ToList();

            foreach (var category in categories)
            {
                stringBuilder.AppendLine($"--{category.Name}");

                foreach (var book in category.MostRecsentBooks)
                {
                    stringBuilder.AppendLine($"{book.Title} ({book.ReleaseDate})");
                }
            }

            return stringBuilder.ToString().TrimEnd();
        }

        //14. Increase Prices
        public static void IncreasePrices(BookShopContext context)
        {
            const int year = 2010;
            const int increasementRate = 5;

            var booksForPriceIncreasment = context
                .Books
                .Where(b => b.ReleaseDate.Value.Year < year)
                .ToList();

            foreach (var book in booksForPriceIncreasment)
            {
                book.Price += increasementRate;
            }

            context.SaveChanges();
        }

        //15. Remove Books
        public static int RemoveBooks(BookShopContext context)
        {
            const int count = 4200;

            var booksToDelete = context
                .Books
                .Where(b => b.Copies < count)
                .ToList();

            context.RemoveRange(booksToDelete);
            context.SaveChanges();

            return booksToDelete.Count;
        }
    }
}
