using Authenticate.API.Data.DTOs;
using Authenticate.API.IService;
using BuildingBlocks.CQRS;

namespace Authenticate.API.Authentication.Login
{
    public record LoginResult(string Token);
    public record LoginCommand(LoginDTO Login):ICommand<LoginResult>;
    public class LoginCommandHandler : ICommandHandler<LoginCommand, LoginResult>
    {
        private readonly IAuthService _authService;
        public LoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<LoginResult> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var token = await _authService.Login(command.Login);
            if (token == null)
            {
                throw new Exception();

            }
            return new LoginResult(token);
        }
    }
}
