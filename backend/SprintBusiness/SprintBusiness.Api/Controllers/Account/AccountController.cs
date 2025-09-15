using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Serilog;
using SprintBuisness.Contracts.Authentication;
using SprintBusiness.Api.Requests.Account;
using SprintBusiness.Features.Account.Commands;
using SprintBusiness.Features.Account.Queries.GetInfo;
using SprintBusiness.Features.Users.Commands.ActivateAccount.ActivateAccount;
using SprintBusiness.Features.Users.Commands.Edit;
using SprintBusiness.Features.Users.Commands.EditPassword;
using SprintBusiness.Features.Users.Queries.Details;
using SprintBusiness.Features.Users.Queries.GetUsers;
using SprintBusiness.Features.Users.Queries.GetUserWorkGroups;
using SprintBusiness.Features.Users.Queries.Pagination;
using SprintBusiness.Features.Users.Queries.Status;
using SprintBusiness.Shared.Dtos;

namespace SprintBusiness.Api.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userSerivce;
        private readonly IMediator _mediator;

        public AccountController(IUserService userSerivce, IMediator mediator )
        {
            _userSerivce = userSerivce;
            _mediator = mediator;
        }

        [HttpGet("validate")]
        public async Task<IActionResult> ValidateAsync([FromQuery, BindRequired] string code)
        {
            Log.Information("ValidateAsync. {code}", code);
            
            var result = await _mediator.Send(new ValidateCommand(code));

            Log.Information("ValidateAsync. {result}", result);

            if (!result.Succeeded)
            {
                Log.Error("ValidateAsync. {code}", code);
                return BadRequest();
            }

            Response.Cookies.Append("access_token", result.Entity!.AccessToken.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // مطلوب عند استخدام SameSite=None
                SameSite = SameSiteMode.None, // للسماح بالإرسال عبر المواقع
                Expires = result.Entity!.AccessToken.ExpireDate,
            });

            Response.Cookies.Append("refresh_token", result.Entity!.RefreshToken.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // مطلوب عند استخدام SameSite=None
                SameSite = SameSiteMode.None, // للسماح بالإرسال عبر المواقع
                Expires = result.Entity!.RefreshToken.ExpireDate,
            });

            Log.Information("GetInformationUserQuery");
            var userInformation = await _mediator
                .Send(new GetInformationUserQuery(result.Entity.AccessToken.Token!));

            Log.Information("GetInformationUserQuery. {userInformation}", userInformation);
            return Ok(new
            {
                ValidTo = result.Entity.RefreshToken.ExpireDate , 
                UserName = result.Entity.User.UserName,
                EnglishName = userInformation?.EnglishName,
                ArabicName = userInformation?.ArabicName,
                Email = userInformation?.Email,
                Id = userInformation?.Id,
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] string? keyword)
        {
            return Ok(await _mediator.Send(new GetEmployeesQuery(keyword)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> DetailsAsync(int id)
        {
            return Ok(await _mediator.Send(new GetEmployeeDetailsQuery(id)));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetAsync([FromQuery] GetEmployeesPaginationDto request)
        {
            return Ok(await _mediator.Send(
                    new GetEmployeesPaginationQuery(request.Page, request.Keyword)));
        }

        // [HttpPost("login")]
        // public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        // {
        //     var result = await _userSerivce.LoginAsync(request);

        //     if (!result.Succeeded)
        //         return BadRequest(result);

        //     return Ok(result);
        // }

        [HttpPut("activate")]
        public async Task<IActionResult> ActivateAsync()
        {
            await _mediator.Send(new ActivateAccountCommand());

            return Ok();
        }

        [HttpPut("deactivate")]
        public async Task<IActionResult> DeactivateAsync()
        {
            await _mediator.Send(new DeactivateAccountCommand());

            return Ok();
        }

        [HttpGet("status")]
        public async Task<IActionResult> AccountStatusAsync ()
        {
            return Ok(await _mediator.Send(new GetAccountStatusQuery()));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, [FromBody] EditEmployeeDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(new EditEmployeeCommand(
                id,
                request.UserName,
                request.EnglishName,
                request.ArabicName,
                request.Email));

            if (!result.Succeeded)
            {
                return BadRequest(result.Message);
            }

            return Ok();
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(new EditPasswordCommand(
                    request.Password, request.ConfirmPassword, request.UserId));

            if (!result.Succeeded)
            {
                return BadRequest(result.Message);
            }

            return Ok();
        }

        [HttpGet("workgroups")]
        public async Task<IActionResult> GetUserWorkGroupsAsync ()
        {
            return Ok(await _mediator.Send(new GetUserWorkGroupsQuery()));
        }
    }
}
