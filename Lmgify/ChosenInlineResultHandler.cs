using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TgBotFramework;

namespace LmgifyBotHost.Lmgify
{
    public class ChosenInlineResultHandler : IUpdateHandler<LmgifyBotContext>
    {
        public async Task HandleAsync(LmgifyBotContext context, UpdateDelegate<LmgifyBotContext> next, CancellationToken cancellationToken)
        {
            Console.WriteLine($"[{DateTime.Now}] User {context.Update.ChosenInlineResult.From} chosen {context.Update.ChosenInlineResult.ResultId} {context.Update.ChosenInlineResult.Query}" );
        }
    }
}