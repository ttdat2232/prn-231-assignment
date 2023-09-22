namespace Application.Dtos
{
    public class StatictisResult
    {
        public int Year { get; set; }
        public Dictionary<int, decimal> MonthIncomes { get; set; } = new Dictionary<int, decimal>()
        {
            {1, 0},
            {2, 0},
            {3, 0},
            {4, 0},
            {5, 0},
            {6, 0},
            {7, 0},
            {8, 0},
            {9, 0},
            {10, 0},
            {11, 0},
            {12, 0},
        };
    }
}
