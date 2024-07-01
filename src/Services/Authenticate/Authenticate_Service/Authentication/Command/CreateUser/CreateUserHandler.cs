using Authenticate.API.Data.DTOs;
using Authenticate.API.IService;
using Authenticate.API.Models;
using Authenticate_Service.Models;
using BuildingBlocks.CQRS;
using BuildingBlocks.Messaging.Events;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace Authenticate.API.Authentication.Command.CreateUser
{
    public record CreateUserResult(Guid UserId);
    public record CreateUserCommand(RegisterDTO RegisterDTO):ICommand<CreateUserResult>;
    public class CreateUserCommandValidator: AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x=>x.RegisterDTO.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.RegisterDTO.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x=>x.RegisterDTO.Password).NotEmpty().WithMessage("Password is required");


        }
    }
    
    public class CreateUserHandler : ICommandHandler<CreateUserCommand, CreateUserResult>
    {
        private readonly IAuthService _authService;
        private readonly IPublishEndpoint _publishEndpoint;

        public CreateUserHandler(IAuthService authService,IPublishEndpoint publishEndpoint)
        {
            _authService = authService;
            _publishEndpoint= publishEndpoint;

        }
        public async Task<CreateUserResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var userId = await _authService.Register(command.RegisterDTO);
            var userEvent = new UserCreatedEvent { UserId = userId,UserName=command.RegisterDTO.Name };
            var message = userEvent;
            await _publishEndpoint.Publish(message,cancellationToken);
            return new CreateUserResult(userId);
        }
    }
}
