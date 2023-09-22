namespace Application.Dtos
{
    public class RentingDetailDto
    {
        public int RentingTransactionId { get; set; }
        public int CarId { get; set; }
        public string CarName { get; set; } = string.Empty; 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal? Price { get; set; }
    }
}
