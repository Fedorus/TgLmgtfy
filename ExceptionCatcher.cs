using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TgBotFramework;

namespace LmgifyBotHost;

public class ExceptionCatcher<T> : IUpdateHandler<LmgifyBotContext>
{
    private readonly ILogger<ExceptionCatcher<T>> _logger;

    public ExceptionCatcher(ILogger<ExceptionCatcher<T>> logger)
    {
        _logger = logger;
    }
    public async Task HandleAsync(LmgifyBotContext context, UpdateDelegate<LmgifyBotContext> next, CancellationToken cancellationToken)
    {
        try
        {
            await next(context, cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "[{Now}]", DateTime.Now);
        }
    }
}