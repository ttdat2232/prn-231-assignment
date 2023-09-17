using Application.Dtos.Creates;

namespace Application.Dtos.Updates
{
    public class UpdateCarInformationDto
    {
        public int CarId { get; set; }
        public string? CarName { get; set; } = null!;
        public string? CarDescription { get; set; }
        public DoorNumber? NumberOfDoors { get; set; }
        public int? SeatingCapacity { get; set; }
        public FuelType? FuelType { get; set; }
        public int? Year { get; set; }
        public int? ManufacturerId { get; set; }
        public int? SupplierId { get; set; }
        public byte? CarStatus { get; set; }
        public decimal? CarRentingPricePerDay { get; set; }
    }
}
