using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using Discord.WebSocket;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using Discord;

namespace DiscordBotSharp
{
    class Program
    {
        static void Main(string[] args) => new Program().BotAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private CommandService _Commands;
        private IServiceProvider _Services;
        public async Task BotAsync()
        {
            _client = new DiscordSocketClient();
            _Commands = new CommandService();
            _Services = new ServiceCollection()
            .AddSingleton(_client)
            .AddSingleton(_Commands)
            .BuildServiceProvider();

            string botToken = "NTE0NDU3NDUwNTUyNzU0MTc4.DtW1nQ.x6dmt9MUCJIJe3Sf_rpxElbw8Dw";
            // event subscibtion
            _client.Log += Log;

            await RegisterCommandsSync();
            await _client.LoginAsync(Discord.TokenType.Bot, botToken);
            await _client.StartAsync();
            await Task.Delay(-1);
           
           

        }

        private Task Log(LogMessage arg)
        {

            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        public async Task RegisterCommandsSync()
        {
            _client.MessageReceived += HandleCommandAsyc;
            await _Commands.AddModulesAsync(Assembly.GetEntryAssembly());

        }


        private async Task HandleCommandAsyc(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            if (message is null || message.Author.IsBot) return;

            int argPostion = 0;

            if (!message.HasMentionPrefix(_client.CurrentUser, ref argPostion) || message.HasMentionPrefix(_client.CurrentUser, ref argPostion))
            {
                var context = new SocketCommandContext(_client, message);
                var result = await _Commands.ExecuteAsync(context, argPostion, _Services);

                if (result.IsSuccess)
                    Console.WriteLine(message.Author.Username + " slapped Billy");

            }




        }



    }
}
