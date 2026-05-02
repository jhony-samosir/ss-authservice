using System.Threading.Channels;
using SS.AuthService.Application.Interfaces;

namespace SS.AuthService.Infrastructure.Services;

public class EmailQueue : IEmailQueue
{
    private readonly Channel<EmailTask> _queue;

    public EmailQueue()
    {
        // Unbounded channel untuk simplicity, bisa di-limit jika perlu
        _queue = Channel.CreateUnbounded<EmailTask>(new UnboundedChannelOptions
        {
            SingleReader = true // Karena cuma ada 1 BackgroundService yang baca
        });
    }

    public async ValueTask QueueEmailAsync(EmailTask emailTask)
    {
        await _queue.Writer.WriteAsync(emailTask);
    }

    public async ValueTask<EmailTask> DequeueEmailAsync(CancellationToken cancellationToken)
    {
        return await _queue.Reader.ReadAsync(cancellationToken);
    }
}
