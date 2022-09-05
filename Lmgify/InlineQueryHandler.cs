using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types.InlineQueryResults;
using TgBotFramework;

namespace LmgifyBotHost.Lmgify
{
    public class InlineQueryHandler :  IUpdateHandler<LmgifyBotContext>
    {
        public async Task HandleAsync(LmgifyBotContext context, UpdateDelegate<LmgifyBotContext> next, CancellationToken cancellationToken)
        {
            var text =  HttpUtility.UrlEncode(context.Update.InlineQuery.Query);
            
            await context.Bot.Client.AnswerInlineQueryAsync(context.Update.InlineQuery.Id,
                new InlineQueryResult[3]
                {
                    new InlineQueryResultArticle("lmgtfy.com", "lmgtfy.com", new InputTextMessageContent("https://lmgtfy.com/?q="+text)),
                    new InlineQueryResultArticle("google.com", "Google.com", new InputTextMessageContent("https://www.google.com/search?q="+text)),
                    new InlineQueryResultArticle("wikipedia.com", "Wikipedia.org", new InputTextMessageContent("https://en.wikipedia.org/wiki/"+context.Update.InlineQuery.Query.Replace(' ', '_')))
                }, cancellationToken: cancellationToken);
        }
    }
}