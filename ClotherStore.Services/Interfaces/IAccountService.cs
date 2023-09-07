using ClothingStore.Domain.Response;
using ClothingStore.Domain.ViewModels.Account;
using System.Security.Claims;

namespace ClotherStore.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IBaseResponse<ClaimsIdentity>> Register(RegisterViewModel user);
        Task<IBaseResponse<ClaimsIdentity>> LogIn(LoginViewModel user);

    }
}
