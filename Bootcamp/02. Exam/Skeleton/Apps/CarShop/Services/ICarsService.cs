namespace CarShop.Services
{
    using CarShop.ViewModels.Cars;

    using System.Collections.Generic;

    public interface ICarsService
    {
        void Create(CarInputModel inputModel, string userId);

        IEnumerable<CarViewModel> GetAll(string userId);

        IEnumerable<CarViewModel> GetAllWithUnfixedIssues();
    }
}
