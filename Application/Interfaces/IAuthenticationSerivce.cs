using Application.Dtos;

namespace Application.Interfaces
{
    public interface IAuthenticationSerivce
    {
        public Task<AuthenticateResponse> LoginAsync(AuthenticateRequest request);
        public Task<AuthenticateResponse> RegisterAsync(AuthenticateRequest request);
    }
}
