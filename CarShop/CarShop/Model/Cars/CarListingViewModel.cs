namespace CarShop.Model.Cars
{
    public class CarListingViewModel
    {
        public string Id { get; init; }

        public string Model { get; init; }

        public int Year { get; set; }

        public string Image { get; set; }

        public string PlateNumber { get; init; }

        public int RemainingIssues { get; init; }

        public int FixedIssues { get; init; }
    }
}
