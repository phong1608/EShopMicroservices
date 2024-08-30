using BuildingBlocks.CQRS;
using Mapster;
using Marten;
using Notification.API.Model;
using System.Windows.Input;

namespace Notification.API.Notification.Command.CreateNotification
{
    public record CreateNotificationCommand(string Title, string Content, Guid RecipientId) :ICommand<CreateNotificationResult>;
    public record CreateNotificationResult(Guid Id);
    public class CreateNotificationCommandHandler : ICommandHandler<CreateNotificationCommand, CreateNotificationResult>
    {
        private readonly IDocumentSession _session;
        public CreateNotificationCommandHandler(IDocumentSession session)
        {
            _session = session;
        }

        public async Task<CreateNotificationResult> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var newNotification = new Notifications(request.Title, request.Content, request.RecipientId);
            _session.Store(newNotification);
            await _session.SaveChangesAsync(cancellationToken);
            return new CreateNotificationResult(newNotification.Id);
        }
    }
}
