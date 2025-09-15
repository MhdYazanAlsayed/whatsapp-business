using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SprintBusiness.Shared.Configurations;
using SprintBusiness.Shared.Dtos;
using SprintBusiness.Shared.Helpers;

namespace SprintBusiness.Features.Account.Commands
{
    public class ValidateCommandHandler : IRequestHandler<ValidateCommand, ResultDto<ValidateCommandResult>>
    {
        private readonly OAuthConfigurations _oAuthConfigurations;
        private readonly ILogger<ValidateCommandHandler> _logger;

        public ValidateCommandHandler(OAuthConfigurations oAuthConfigurations , ILogger<ValidateCommandHandler> logger)
        {
            _oAuthConfigurations = oAuthConfigurations;
            _logger = logger;
        }

        public async Task<ResultDto<ValidateCommandResult>> Handle(ValidateCommand request, CancellationToken cancellationToken)
        {
            var httpClient = new HttpClient();

            var message = new HttpRequestMessage(HttpMethod.Get,
                _oAuthConfigurations.Url + $"/api/auth?Code={request.Code}&ClientId={_oAuthConfigurations.Id}&Secret={_oAuthConfigurations.Secret}");

            var response = await httpClient.SendAsync(message);

            _logger.LogError("Server respond with status code :" + response.StatusCode.ToString());

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Something went wrong while validate the code .");
                return ResultDto<ValidateCommandResult>.Failure();
            }

            var resultAsText = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ValidateCommandResult>(resultAsText);

            _logger.LogError("Response json : " + resultAsText);
            _logger.LogError("Validate successfully");

            return ResultDto<ValidateCommandResult>.Success(result);
        }
    }
}
