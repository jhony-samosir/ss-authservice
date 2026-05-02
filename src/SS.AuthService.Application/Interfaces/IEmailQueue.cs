namespace SS.AuthService.Application.Interfaces;

public record EmailTask(string To, string Token);

public interface IEmailQueue
{
    ValueTask QueueEmailAsync(EmailTask emailTask);
    ValueTask<EmailTask> DequeueEmailAsync(CancellationToken cancellationToken);
}
