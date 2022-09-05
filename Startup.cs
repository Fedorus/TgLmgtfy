using System.Collections.Generic;
using System.Linq;
using LmgifyBotHost.Lmgify;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TgBotFramework;

namespace LmgifyBotHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddSingleton<InlineQueryHandler>();
            services.AddSingleton<TextEchoer>();
            services.AddSingleton<ExceptionCatcher<LmgifyBotContext>>();
            services.AddScoped<ChosenInlineResultHandler>();
            
            services.AddBotService<LmgifyBotContext>(Configuration.GetSection("LmgifyBot")["ApiToken"],
                builder => builder.UseLongPolling()
                    .SetPipeline(pipe => pipe.Use<ExceptionCatcher<LmgifyBotContext>>()
                        .MapWhen<TextEchoer>(On.Message)
                        .MapWhen<InlineQueryHandler>(On.InlineQuery)
                        .MapWhen<ChosenInlineResultHandler>(On.ChosenInlineResult)
                    ));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!"); });
            });
        }
    }
}