using Authenticate.API.IService;
using BuildingBlocks.CQRS;

namespace Authenticate.API.Authentication.AssignRole
{
    public record AssignRoleResult(bool IsSuccess);
    public record AssignRoleCommand(string Email, string RoleName) :ICommand<AssignRoleResult>;
    public class AssignRoleCommandHandler : ICommandHandler<AssignRoleCommand, AssignRoleResult>
    {
        private readonly IAuthService  _authService;
        public AssignRoleCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<AssignRoleResult> Handle(AssignRoleCommand command, CancellationToken cancellationToken)
        {
            await _authService.AssignRole(command.Email, command.RoleName);
            return new AssignRoleResult(true);
        }
    }
}
