using ApiClient.Models;

namespace WebAPI.UserServices;

public interface IClientService
{
    //Task<ServiceResponse> RegisterUserAsync(Registration model);
    Task<ServiceResponse> LoginUserAsync(HomeModel model);
}
