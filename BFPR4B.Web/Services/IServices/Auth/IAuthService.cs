using BFPR4B.Web.Models.DTO.Auth;

namespace BFPR4B.Web.Services.IServices.Auth
{
    public interface IAuthService
    {
		Task<T> LoginAsync<T>(LoginRequestDTO parameters);
		Task<T> RegisterAsync<T>(RegistrationRequestDTO parameters);
	}
}
