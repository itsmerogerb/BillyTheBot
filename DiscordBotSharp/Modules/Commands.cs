using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotSharp.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {

        [Command("purge", RunMode = RunMode.Async)]
        [Alias("Purge")]
        [Remarks("Purges An Amount Of Messages")]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        public async Task Clear(int amountOfMessagesToDelete = 100000)
        {

            await (Context.Message.Channel as SocketTextChannel).DeleteMessagesAsync(await Context.Message.Channel.GetMessagesAsync(amountOfMessagesToDelete + 1).FlattenAsync());


            if (amountOfMessagesToDelete == 100000)
                await ReplyAsync("Cleared all messages");

        }

        [Command("!help")]
        public async Task help()
        {
            IUser user = null;
            user = user ?? Context.User;
            await ReplyAsync("Hello " + user.Username + " here is what I can do for you \n" + "!ping = pong \n !poop = poop? " +
                "\n !userInfo = get user info \n !multiply = mutiply two numbers \n !add = add two numbers");
        }

        [Command("Hi Billy")]
        [Alias("hi billy", "Hi billy")]
        public async Task PingAsync()
        {

            IUser user = null;
            user = user ?? Context.User;
            await ReplyAsync("Hello " + user.Username);
        }

        [Command("!ping")]
        [Alias("pong", "hello")]
        public Task PongAsync()
            => ReplyAsync("pong!");


        [Command("!poop")]
        [Alias("!Poop", "!POOP")]
        public async Task Poop()
        {
            await Clear(1);
            await ReplyAsync(":poop:");
        }
      
        [Command("!userinfo")]
        public async Task UserInfoAsync(IUser user = null)
        {
            user = user ?? Context.User;

            await ReplyAsync(user.ToString());
        }

        [Command("!multiply")]
        [Summary("Get the product of two numbers.")]
        public async Task Say(int a, int b)
        {
            int product = a * b;
            await ReplyAsync($"The product of `{a} * {b}` is `{product}`.");
        }

        [Command("!add")]
        [Summary("Get the product of two numbers.")]
        public async Task Sa2y(int a, int b)
        {
            int product = a + b;
            await ReplyAsync($"The product of `{a} + {b}` is `{product}`.");
        }




    }
}
