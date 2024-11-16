using DSharpPlus;
using DSharpPlus.CommandsNext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Commands;
using TestApplication.config;

namespace TestApplication
{
    internal class Program
    {
        private static DiscordClient Client { get; set; }
        private static CommandsNextExtension Commands { get; set; }

        static async Task Main(string[] args)
        {
            var jsonReader = new JSONReader();
            await jsonReader.ReadJSON();

            var discordConfig = new DiscordConfiguration()
            {
                Intents=DiscordIntents.All,
                Token=jsonReader.token,
                TokenType=TokenType.Bot,
                AutoReconnect=true,
            };

            Client= new DiscordClient(discordConfig);


            Client.Ready += Client_Ready;
            Client.MessageCreated += Client_MessageCreated;


            var CommandsConfig =new CommandsNextConfiguration() 
            {
            
                StringPrefixes =new string[] {jsonReader.prefix},
                EnableMentionPrefix =true,
                EnableDms=true, 
                EnableDefaultHelp=false,

            };

            Commands =Client.UseCommandsNext(CommandsConfig);

            Commands.RegisterCommands<TestsCommands>();
            await Client.ConnectAsync();
            

            await Task.Delay(-1);   

        }

        private static Task Client_Ready(DiscordClient sender, DSharpPlus.EventArgs.ReadyEventArgs args)
        {
           return Task.CompletedTask;
        }
        private static async Task Client_MessageCreated(DiscordClient sender, DSharpPlus.EventArgs.MessageCreateEventArgs args)
        {
            // Ignore messages from bots
            if (args.Author.IsBot) return;

            // Check if the message is the name of the command
            if (args.Message.Content.Equals("Fazzy", StringComparison.OrdinalIgnoreCase) || args.Message.Content.Equals("louay", StringComparison.OrdinalIgnoreCase)|| args.Message.Content.Equals("loay", StringComparison.OrdinalIgnoreCase))
            {
                // Respond to the command
                await args.Message.RespondAsync($"Wach {args.Author.Username}");
            }
            // Check if the message is the name of the command
            if (args.Message.Content.Equals("Fazzy Z Goat", StringComparison.OrdinalIgnoreCase))
            {
                // Respond to the command
                await args.Message.RespondAsync("🐐");
            }
        }
    }
}
