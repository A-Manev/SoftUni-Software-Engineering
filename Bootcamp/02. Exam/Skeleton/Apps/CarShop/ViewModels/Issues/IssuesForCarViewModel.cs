namespace CarShop.ViewModels.Issues
{
    using System.Collections.Generic;

    public class IssuesForCarViewModel
    {
        public string CarId { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public ICollection<IssueViewModel> Issues { get; set; }
    }
}
