namespace CarShop.Services
{
    using CarShop.Data;
    using CarShop.Data.Models;
    using CarShop.ViewModels.Cars;

    using System.Linq;
    using System.Collections.Generic;

    public class CarsService : ICarsService
    {
        private readonly ApplicationDbContext db;

        public CarsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Create(CarInputModel inputModel, string userId)
        {
            var car = new Car
            {
                Model = inputModel.Model,
                Year = inputModel.Year,
                PictureUrl = inputModel.Image,
                PlateNumber = inputModel.PlateNumber,
                OwnerId = userId,
            };

            this.db.Cars.Add(car);
            this.db.SaveChanges();
        }

        public IEnumerable<CarViewModel> GetAll(string userId)
        {
            return this.db.Cars
                .Where(x => x.OwnerId == userId)
                .Select(x => new CarViewModel
                {
                    Id = x.Id,
                    Year = x.Year,
                    Model = x.Model,
                    Image = x.PictureUrl,
                    PlateNumber = x.PlateNumber,
                    RemainingIssues = x.Issues.Where(x => x.IsFixed == false).Count(),
                    FixedIssues = x.Issues.Where(x => x.IsFixed == true).Count(),
                })
                .ToList();
        }

        public IEnumerable<CarViewModel> GetAllWithUnfixedIssues()
        {
            return this.db.Cars
                .Where(x => x.Issues.Any(x => !x.IsFixed))
                .Select(x => new CarViewModel
                {
                    Id = x.Id,
                    Year = x.Year,
                    Model = x.Model,
                    Image = x.PictureUrl,
                    PlateNumber = x.PlateNumber,
                    RemainingIssues = x.Issues.Where(x => x.IsFixed == false).Count(),
                    FixedIssues = x.Issues.Where(x => x.IsFixed == true).Count(),
                })
                .ToList();
        }
    }
}
