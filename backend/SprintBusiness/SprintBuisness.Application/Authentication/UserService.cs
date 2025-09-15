using Microsoft.AspNetCore.Identity;
using SprintBuisness.Contracts.Authentication;
using SprintBusiness.Domain.Users;
using SprintBusiness.Shared.Dtos;

namespace SprintBuisness.Application.Authentication
{
    public class UserService : IUserService
    {
        //private readonly UserManager<ApplicationUser> _userManager;
        private readonly IToken _tokenService;

        public UserService(IToken tokenService)
        {
            //_userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<ResultDto<LoginResult>> LoginAsync(LoginRequest request)
        {
            throw new NotImplementedException();
            //var user = await _userManager.Users
            //    .SingleOrDefaultAsync(x => x.UserName == request.UserName);

            //if (user is null)
            //    return FcResults.Failure<LoginResult>();

            //bool result = await _userManager.CheckPasswordAsync(user, request.Password);
            //if (!result)
            //    return FcResults.Failure<LoginResult>();

            //var roles = await _userManager.GetRolesAsync(user);
            //var claims = new List<Claim>();
            //claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));
            //claims.AddRange(new List<Claim>()
            //{
            //    new Claim("UserName" , request.UserName),
            //    new Claim(ClaimTypes.NameIdentifier , user.Id),
            //});

            //var accessToken = _tokenService.GenerateAccessToken(claims);
            ////var refreshToken = _tokenService.GenerateRefreshToken();

            //return FcResults.Success(new LoginResult()
            //{
            //    AccessToken = accessToken,
            //    RefreshToken = null,
            //    UserName = request.UserName ,
            //    UserId = user.Id ,
            //    Email = user.Email ,
            //    FullName = user.FullName
            //});
        }

        public async Task<IdentityResult> RegisterAsync(Employee user, string password)
        {
            throw new NotImplementedException();
            //return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> UpdateAsync(Employee user)
        {
            throw new NotImplementedException();
            //return await _userManager.UpdateAsync(user);
        }

        public string HashPassword (Employee user , string password)
        {
            throw new NotImplementedException();
            //return _userManager.PasswordHasher.HashPassword(user, password);
        }
    }
}
