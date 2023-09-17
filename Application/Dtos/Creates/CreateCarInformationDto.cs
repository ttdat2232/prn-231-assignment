using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Creates
{
    public class CreateCarInformationDto
    {
        [MinLength(1)]
        public string CarName { get; set; } = null!;
        public string? CarDescription { get; set; }
        public DoorNumber? NumberOfDoors { get; set; }
        public int? SeatingCapacity { get; set; }
        public FuelType? FuelType { get; set; }
        public int? Year { get; set; }
        public int ManufacturerId { get; set; }
        public int SupplierId { get; set; }
        public byte? CarStatus { get; set; }
        public decimal? CarRentingPricePerDay { get; set; }
    }   
    public enum DoorNumber
    {
        Two = 2,
        Four = 4,
    }
    public enum FuelType
    {
        Gasoline,
        Electricity
    }
}
