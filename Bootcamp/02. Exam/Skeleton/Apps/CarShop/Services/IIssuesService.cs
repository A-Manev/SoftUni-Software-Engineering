namespace CarShop.Services
{
    using CarShop.ViewModels.Issues;

    public interface IIssuesService
    {
        void Create(IssueInputModel inputModel);

        void Delete(string id);

        void Fix(string id);

        IssuesForCarViewModel GetAll(string carId);
    }
}
