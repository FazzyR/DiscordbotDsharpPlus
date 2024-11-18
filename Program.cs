using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
using DSharpPlus.VoiceNext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Commands;
using TestApplication.Commands.Slash;
using TestApplication.config;

namespace TestApplication
{
    internal class Program
    {
        public static DiscordClient Client { get; set; }
        private static VoiceNextExtension _voiceNext; 

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
            Client.UseInteractivity(new InteractivityConfiguration()
            {
                Timeout = TimeSpan.FromMinutes(2)
            });

            Client.Ready += Client_Ready;
            //Client.SocketClosed += Client_SocketClosed;
            Client.MessageCreated += Client_MessageCreated;


            var CommandsConfig =new CommandsNextConfiguration() 
            {
            
                StringPrefixes =new string[] {jsonReader.prefix},
                EnableMentionPrefix =true,
                EnableDms=false, 
                EnableDefaultHelp=false,

            };

            Commands =Client.UseCommandsNext(CommandsConfig);
            var voiceNext = Client.UseVoiceNext();
            var SlashCommands = Client.UseSlashCommands();

            Commands.RegisterCommands<TestsCommands>();
            SlashCommands.RegisterCommands<SlashCommands>();
            SlashCommands.RegisterCommands<VoiceCommands>();

            await Client.ConnectAsync();
            Console.WriteLine("Press any key to exit...");

            Console.ReadKey();
            await Client.DisconnectAsync(); // Manually disconnect the bot
            Console.WriteLine("Bot disconnected.");

            await Task.Delay(-1);   

        }

        private static async Task Client_SocketClosed(DiscordClient sender, DSharpPlus.EventArgs.SocketCloseEventArgs args)
         {
            try
            {
                // Specify the ID of the channel where you want to send the shutdown message
                ulong channelId = 1281685501916483610; // Replace with the actual channel ID

                // Get the channel using the channel ID
                var channel = await sender.GetChannelAsync(channelId);

                // Send a message to the specified channel
                await channel.SendMessageAsync("LBOT Ra7 Yar9d Salam");

                // Optionally, log additional information about the socket closure
                Console.WriteLine($"Socket closed. Code: {args.CloseCode}, Reason: {args.CloseMessage}");
            }
            catch (Exception ex)
            {
                // Log any exceptions that occur
                Console.WriteLine($"An error occurred while handling the SocketClosed event: {ex.Message}");
            }
        }

        private static Task Client_Ready(DiscordClient sender, DSharpPlus.EventArgs.ReadyEventArgs args)
        {
           return Task.CompletedTask;
        }
        private static async Task Client_MessageCreated(DiscordClient sender, DSharpPlus.EventArgs.MessageCreateEventArgs args)
        {



            bool kmltprogram = false;



            // Ignore messages from bots
            if (args.Author.IsBot) return;

            // Check if the message is the name of the command
            if (args.Message.Content.IndexOf("Fazzy", StringComparison.OrdinalIgnoreCase) >= 0 || args.Message.Content.IndexOf("louay", StringComparison.OrdinalIgnoreCase) >= 0 || args.Message.Content.IndexOf("loay", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                // Get the guild member by ID
                var member = await args.Guild.GetMemberAsync(args.Author.Id);

                // Use nickname if available, fallback to username
                string displayName = member?.Nickname ?? args.Author.Username;
                // Respond to the command
                await args.Message.RespondAsync($"Wach {displayName.ToUpper()} kachma 3ndk m3a sidi?");
            }
            // Check if the message is the name of the command
            if (args.Message.Content.Equals("Goat", StringComparison.OrdinalIgnoreCase)|| args.Message.Content.IndexOf("Gwat", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                // Respond to the command
                await args.Message.RespondAsync("Fazzy 🐐");
            }

            if (args.Author.IsBot) return;

            var targetUser = await sender.GetUserAsync(582189010516967426);
            // Check if the message mentions the bot (case-insensitive)
            if (args.Message.MentionedUsers.Contains(targetUser))
            {
                await args.Message.RespondAsync($"EY L7OMA KACHMA 3NDK M3A SIDI ????");
            }
            var targetUserName =  args.Message.Author.Username;
            // Check if the message mentions the bot (case-insensitive)
             if ((targetUserName== "rach_aaa_80540"|| targetUserName == "__yasmne"|| targetUserName == " naila_bk")  )
             {
                 await args.Message.RespondAsync($"KMLTI PROGRAM?");

                 kmltprogram = true;
             }
        }
    }
}
