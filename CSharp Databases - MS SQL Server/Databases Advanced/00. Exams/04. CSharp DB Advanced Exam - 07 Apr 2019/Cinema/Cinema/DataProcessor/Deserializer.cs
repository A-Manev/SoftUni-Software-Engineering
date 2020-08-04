namespace Cinema.DataProcessor
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
    using DataProcessor.ImportDto;

    using Newtonsoft.Json;

    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";
        private const string SuccessfulImportMovie
            = "Successfully imported {0} with genre {1} and rating {2}!";
        private const string SuccessfulImportHallSeat
            = "Successfully imported {0}({1}) with {2} seats!";
        private const string SuccessfulImportProjection
            = "Successfully imported projection {0} on {1}!";
        private const string SuccessfulImportCustomerTicket
            = "Successfully imported customer {0} {1} with bought tickets: {2}!";

        public static string ImportMovies(CinemaContext context, string jsonString)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var movieDtos = JsonConvert.DeserializeObject<List<ImportMovieDto>>(jsonString);

            List<Movie> movies = new List<Movie>();

            foreach (var movieDto in movieDtos)
            {
                if (!IsValid(movieDto))
                {
                    stringBuilder.AppendLine(ErrorMessage);
                    continue;
                }

                Movie movie = new Movie()
                {
                    Title = movieDto.Title,
                    Genre = (Genre)Enum.Parse(typeof(Genre), movieDto.Genre.ToString()),
                    Duration = TimeSpan.ParseExact(movieDto.Duration, "c", CultureInfo.InvariantCulture),
                    Rating = movieDto.Rating,
                    Director = movieDto.Director
                };

                if (!movies.Any(x => x.Title == movie.Title))
                {
                    movies.Add(movie);
                    stringBuilder.AppendLine(string.Format(SuccessfulImportMovie, movie.Title, movie.Genre, movie.Rating.ToString("F2")));
                }
                else
                {
                    stringBuilder.AppendLine(ErrorMessage);
                    continue;
                }
            }

            context.Movies.AddRange(movies);
            context.SaveChanges();

            return stringBuilder.ToString().TrimEnd();
        }

        public static string ImportHallSeats(CinemaContext context, string jsonString)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var hallAndSeatDtos = JsonConvert.DeserializeObject<List<ImportHallAndSeat>>(jsonString);

            List<Hall> halls = new List<Hall>();

            List<Seat> seats = new List<Seat>();

            foreach (var hallAndSeatDto in hallAndSeatDtos)
            {
                if (!IsValid(hallAndSeatDto))
                {
                    stringBuilder.AppendLine(ErrorMessage);
                    continue;
                }

                Hall hall = new Hall()
                {
                    Name = hallAndSeatDto.Name,
                    Is4Dx = hallAndSeatDto.Is4Dx,
                    Is3D = hallAndSeatDto.Is3D
                };

                for (int i = 0; i < hallAndSeatDto.Seats; i++)
                {
                    Seat seat = new Seat()
                    {
                        Hall = hall
                    };

                    seats.Add(seat);
                }

                halls.Add(hall);

                string status = "";

                if (hall.Is4Dx)
                {
                    status = hall.Is3D ? "4Dx/3D" : "4Dx";
                }
                else if (hall.Is3D)
                {
                    status = "3D";
                }
                else
                {
                    status = "Normal";
                }

                stringBuilder.AppendLine(string.Format(SuccessfulImportHallSeat, hall.Name, status, hallAndSeatDto.Seats));
            }

            context.Halls.AddRange(halls);
            context.Seats.AddRange(seats);
            context.SaveChanges();

            return stringBuilder.ToString().TrimEnd();
        }

        public static string ImportProjections(CinemaContext context, string xmlString)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var xmlSerializer = new XmlSerializer(typeof(List<ImportProjection>), new XmlRootAttribute("Projections"));

            var projectionDtos = (List<ImportProjection>)xmlSerializer.Deserialize(new StringReader(xmlString));

            List<Projection> projections = new List<Projection>();

            int[] movies = context.Movies.Select(x => x.Id).ToArray();

            int[] halls = context.Halls.Select(x => x.Id).ToArray();

            foreach (var projectionDto in projectionDtos)
            {
                if (!IsValid(projectionDto))
                {
                    stringBuilder.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime projectionDateTime;

                bool isProjectionDateTime = DateTime.TryParseExact(projectionDto.DateTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out projectionDateTime);

                //if (!isProjectionDateTime)
                //{
                //    stringBuilder.AppendLine(ErrorMessage);
                //    continue;
                //}

                if (!movies.Contains(projectionDto.MovieId) || !halls.Contains(projectionDto.HallId))
                {
                    stringBuilder.AppendLine(ErrorMessage);
                    continue;
                }

                Projection projection = new Projection()
                {
                    MovieId = projectionDto.MovieId,
                    HallId = projectionDto.HallId,
                    DateTime = projectionDateTime
                };
                
                projections.Add(projection);

                Movie movie = context.Movies.FirstOrDefault(x => x.Id == projection.MovieId);

                stringBuilder.AppendLine(string.Format(SuccessfulImportProjection, movie.Title, projection.DateTime.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)));
            }

            context.Projections.AddRange(projections);

            context.SaveChanges();

            return stringBuilder.ToString().TrimEnd();
        }

        public static string ImportCustomerTickets(CinemaContext context, string xmlString)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var xmlSerializer = new XmlSerializer(typeof(List<ImportCustomerTicketDto>), new XmlRootAttribute("Customers"));

            var customerDtos = (List<ImportCustomerTicketDto>)xmlSerializer.Deserialize(new StringReader(xmlString));

            List<Customer> customers = new List<Customer>();

            List<Ticket> tickets = new List<Ticket>();

            foreach (var customerDto in customerDtos)
            {
                if (!IsValid(customerDto))
                {
                    stringBuilder.AppendLine(ErrorMessage);
                    continue;
                }

                Customer customer = new Customer()
                {
                    FirstName = customerDto.FirstName,
                    LastName = customerDto.LastName,
                    Age = customerDto.Age,
                    Balance = customerDto.Balance
                };

                int ticketsCount = 0;

                foreach (var ticketDto in customerDto.Tickets)
                {
                    if (!IsValid(ticketDto))
                    {
                        stringBuilder.AppendLine(ErrorMessage);
                        continue;
                    }

                    Ticket ticket = new Ticket()
                    {
                        ProjectionId = ticketDto.ProjectionId,
                        Price = ticketDto.Price,
                        Customer = customer
                    };

                    tickets.Add(ticket);
                    ticketsCount++;
                }

                customers.Add(customer);

                stringBuilder.AppendLine(string.Format(SuccessfulImportCustomerTicket, customer.FirstName, customer.LastName, ticketsCount));
            }

            context.Customers.AddRange(customers);
            context.Tickets.AddRange(tickets);
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