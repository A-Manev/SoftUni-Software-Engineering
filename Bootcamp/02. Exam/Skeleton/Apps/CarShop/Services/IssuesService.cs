namespace CarShop.Services
{
    using CarShop.Data;
    using CarShop.Data.Models;
    using CarShop.ViewModels.Issues;

    using System.Linq;

    public class IssuesService : IIssuesService
    {
        private readonly ApplicationDbContext db;

        public IssuesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Create(IssueInputModel inputModel)
        {
            var issue = new Issue
            {
                Description = inputModel.Description,
                CarId = inputModel.carId,
                IsFixed = false,
            };

            this.db.Issues.Add(issue);
            this.db.SaveChanges();
        }

        public IssuesForCarViewModel GetAll(string carId)
        {
            var issues =  this.db.Issues
                 .Where(x => x.CarId == carId)
                 .Select(x => new IssueViewModel
                 {
                     Id = x.Id,
                     IsFixed = x.IsFixed == true ? "Yes" : "Not yet",
                     Description = x.Description,
                     CarId = x.CarId
                 })
                 .ToList();

            var car = this.db.Cars
                .FirstOrDefault(x => x.Id == carId);

            var viewModel = new IssuesForCarViewModel 
            {
                CarId = carId,
                Year = car.Year,
                Model = car.Model,
                Issues = issues, 
            };

            return viewModel;
        }

        public void Delete(string id)
        {
            var issue = this.db.Issues
                .FirstOrDefault(x => x.Id == id);

            this.db.Issues.Remove(issue);
            this.db.SaveChanges();
        }

        public void Fix(string id)
        {
            var issues = this.db.Issues
                .FirstOrDefault(x => x.Id == id);

            issues.IsFixed = true;

            this.db.SaveChanges();
        }
    }
}
