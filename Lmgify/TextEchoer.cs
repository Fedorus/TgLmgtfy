using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using TgBotFramework;

namespace LmgifyBotHost.Lmgify
{
    public class TextEchoer : IUpdateHandler<LmgifyBotContext>
    {
        private readonly ILogger<TextEchoer> _logger;

        public TextEchoer(ILogger<TextEchoer> logger)
        {
            _logger = logger;
        }
        public async Task HandleAsync(LmgifyBotContext context, UpdateDelegate<LmgifyBotContext> next, CancellationToken ct = default)
        {
            if (context.Update.Type == UpdateType.Message)
                await context.Bot.Client.SendTextMessageAsync(context.Update.Message.Chat.Id,
                    "Write in any chat \"@lmgifyBot <search query>\" and choose one of the options.",
                    cancellationToken: ct);
        }
    }
}