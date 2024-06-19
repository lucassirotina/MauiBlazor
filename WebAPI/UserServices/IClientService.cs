using ApiClient.Models;

namespace WebAPI.UserServices;

public interface IClientService
{
    ServiceResponse RegisterUser(RegistrationModel model);
    Task<ServiceResponse> LoginUserAsync(HomeModel model);
}
