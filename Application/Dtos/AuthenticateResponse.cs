namespace Application.Dtos
{
    public class AuthenticateResponse
    {
        public bool IsAdmin { get; set; } = false;
        public int CustomerId { get; set; }
    }
}
