using ClotherStore.Services.Interfaces;
using ClothingStore.DAL.Repositories;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Response;
using ClothingStore.Domain.ViewModels.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using ClothingStore.Domain.Enums;
using ClothingStore.Domain.Helpers;
using System.Reflection.Metadata.Ecma335;
using ClothingStore.DAL.Interfaces;

namespace ClotherStore.Services.Implementions
{
    public class AccountService : IAccountService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IBaseRepository<User> userRepository, ILogger<AccountService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<IBaseResponse<ClaimsIdentity>> LogIn(LoginViewModel login)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == login.Login);
                if (user == null)
                {
                    return new BaseResponse<ClaimsIdentity>
                    {
                        Description = "User not found",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                if (user.Password != HashPasswordHelper.HashPassword(login.Password))
                {
                    return new BaseResponse<ClaimsIdentity>
                    {
                        Description = "Wrong password",
                    };
                }
                var result = Authenticate(user);
                return new BaseResponse<ClaimsIdentity>
                {
                    Description = "Welcome",
                    StatusCode = StatusCode.Ok,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[LogIn]: {ex.Message}");
                return new BaseResponse<ClaimsIdentity>
                {
                    Description = "Something went wrong",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<ClaimsIdentity>> Register(RegisterViewModel user)
        {
            try
            {
                var newUser = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == user.Login);
                if (newUser != null)
                {
                    return new BaseResponse<ClaimsIdentity>
                    {
                        Description = "User with this name already exists"
                    };
                }
                newUser = new User()
                {
                    Login = user.Login,
                    Password = HashPasswordHelper.HashPassword(user.Password),
                    Role = UserRole.Custumer
                };
                await _userRepository.Create(newUser);
                var result = Authenticate(newUser);

                return new BaseResponse<ClaimsIdentity>
                {
                    Data = result,
                    Description = "Account has succeflully created",
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Register]: {ex.Message}");
                return new BaseResponse<ClaimsIdentity>
                {
                    Description = "Something went wrong",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
