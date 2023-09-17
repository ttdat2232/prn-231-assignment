namespace Application.Dtos
{
    public class RentingTransactionDto
    {
        public int RentingTransationId { get; set; }
        public DateTime? RentingDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public int CustomerId { get; set; }
        public IList<RentingDetailDto> RentingDetails { get; set; } = new List<RentingDetailDto>();
    }
}
