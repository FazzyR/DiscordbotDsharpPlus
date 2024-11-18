using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
using DSharpPlus.VoiceNext;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestApplication.config;
using TestApplication.Other;

namespace TestApplication.Commands
{
    public class TestsCommands:BaseCommandModule    
    {

        [Command("spamjoin")]
        public async Task ToggleJoinLeaveAsync(CommandContext ctx)
        {

            try
            {
                // Get the member from the guild using the provided user
                var member = ctx.Member;
                var voiceChannel = member.VoiceState?.Channel;
                if (voiceChannel == null)
                {
                    await ctx.Channel.SendMessageAsync($"{member.Username} is not in a voice channel.");
                    return;
                }
                var vnext = ctx.Client.GetVoiceNext();

                var vnc = vnext.GetConnection(ctx.Guild);
                if (vnc == null)
                {
                    vnc = await vnext.ConnectAsync(voiceChannel);
                    await ctx.Channel.SendMessageAsync($"Joined {voiceChannel.Name}.");
                }

                // Perform the join/leave action multiple times
                for (int i = 0; i < 100; i++) // Example: join/leave 3 times
                {


                    // Disconnect from the voice channel
                    vnc.Disconnect();

                    // Wait before reconnecting

                    // Reconnect to the voice channel
                    vnc = await vnext.ConnectAsync(voiceChannel);

                    // Wait before repeating
                }
            }
            catch (Exception ex)
            {
                // Log any error that might occur
                await ctx.Channel.SendMessageAsync($"An error occurred: {ex.Message}");
            }
        }

        [Command("join")]
        public async Task Join(CommandContext ctx)
        {
            var vnext = ctx.Client.GetVoiceNext();

            var vnc = vnext.GetConnection(ctx.Guild);
            if (vnc != null)
                throw new InvalidOperationException("Already connected in this guild.");

            var chn = ctx.Member?.VoiceState?.Channel;
            if (chn == null)
                throw new InvalidOperationException("You need to be in a voice channel.");


            for (int i = 0; i < 10; i ++) 
            {


                vnc = await vnext.ConnectAsync(chn);
                await ctx.RespondAsync($"Connected to {chn.Name} - Iteration {i + 1}");


                vnc.Disconnect();
                await ctx.RespondAsync($"Disconnected from {chn.Name} - Iteration {i + 1}");


            }




            await ctx.RespondAsync("👌");
        }
        [Command("leave")]

        public async Task Leave(CommandContext ctx)
        {
            var vnext = ctx.Client.GetVoiceNext();
            var vnc = vnext.GetConnection(ctx.Guild);

            // If not connected, attempt to connect to the voice channel
            if (vnc == null)
            {
                var chn = ctx.Member?.VoiceState?.Channel;
                if (chn != null)
                {
                    try
                    {
                        // Try to connect to the voice channel
                        vnc = await vnext.ConnectAsync(chn);

                        if (vnc == null)
                        {
                            await ctx.RespondAsync("Failed to connect to the voice channel.");
                            return; // Ensure early exit if unable to connect
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions that occur during connection
                        await ctx.RespondAsync($"Error occurred while connecting: {ex.Message}");
                        Console.WriteLine($"Connection error: {ex.Message}");
                        return; // Exit early if an error occurs during connection
                    }
                }
                else
                {
                    await ctx.RespondAsync("You must be in a voice channel to disconnect.");
                    return; // Early return if no voice channel is found
                }
            }

            // Introduce a delay before disconnecting (e.g., 3 seconds)
            await Task.Delay(3000); // Wait for 3 seconds

            // Disconnect the bot from the voice channel
            vnc.Disconnect();

            // Respond that the bot has left the voice channel
            await ctx.RespondAsync("👌 Left the voice channel.");
        }









        [Command("r")]
        public async Task AssignRole(CommandContext ctx)
        {
            
            var targetUser = ctx.Member; // This will refer to the user who triggered the command

            // Find the role by name (you can replace "RoleName" with the actual role name)
            var role = ctx.Guild.GetRole(1281685501119696920); // Replace with the role ID, or use ctx.Guild.GetRole("RoleName")

            // Check if the bot has the permission to assign the role
            if (role == null)
            {
                await ctx.Channel.SendMessageAsync("Rnf");
                return;
            }

            try
            {
                // Assign the role to the user (or yourself)
                await targetUser.GrantRoleAsync(role);
                await ctx.Channel.SendMessageAsync($"{targetUser.Username} h b g t re .");
            }
            catch (Exception ex)
            {
                await ctx.Channel.SendMessageAsync($"E a r: {ex.Message}");
            }
        }



        [Command("poll")]
        public async Task poll(CommandContext ctx)
        {

            await ctx.Channel.SendMessageAsync("s");



        }




        [Command("تحية")]
        public async Task تحية(CommandContext ctx)
        {
           
                await ctx.Channel.SendMessageAsync("تحياتنا ليك ");

            
        
        }
        [Command("AD7K3LIH")]
        public async Task AD7K3LIH(CommandContext ctx)
        {
            if (ctx.User.Username == "fazzyri")
            {
                await ctx.Channel.SendMessageAsync("HHHHHHHHHHHHH");

            }
            else
            {
                await ctx.Channel.SendMessageAsync("LALA 3IB ");

            }
        }


   

        [Command("qst")]
        public async Task Qst(CommandContext ctx,[RemainingText]string qst=null)
        {
            // Check if the user didn't provide a question
            if (string.IsNullOrWhiteSpace(qst))
            {
                await ctx.Channel.SendMessageAsync("AKTB QUESTION YA MONGOL");
                return; // Exit the method early
            }

            // Create a random number generator
            Random random = new Random();

            // Generate a random number: 0 or 1
            int choice = random.Next(0, 2); // 0 for "No", 1 for "Yes"

            // Determine the response
            string response = choice == 1 ? "Yes" : "No";
            if (ctx.User.Username == "fazzyri") 
            {

                response = "Yes Sir";
                
            }
           

            // Send the response
            await ctx.Message.RespondAsync(response);
        }


        [Command("cardgame")]
        public async Task Qst(CommandContext ctx)
        {
            var userCard = new CardSystem();

            var userCardEmbed = new DiscordEmbedBuilder 
            {
                Title=$"{ctx.User.Username.ToUpper()} Card is {userCard.SelectedCard} ",
                Color=DiscordColor.Lilac,

            
            };
            
            await ctx.Channel.SendMessageAsync(embed: userCardEmbed);

            var botCard = new CardSystem();
            var botCardEmbed = new DiscordEmbedBuilder
            {
                Title = $"The bot drew {botCard.SelectedCard} ",
                Color = DiscordColor.Red,


            };
            await ctx.Channel.SendMessageAsync(embed: botCardEmbed);

            if(userCard.SelectedNumber > botCard.SelectedNumber)
            {
                var winMessage = new DiscordEmbedBuilder
                {
                    Title = $" Congratulations {ctx.User.Username.ToUpper()} WIN!!! ",
                    Color = DiscordColor.Green,


                };
                await ctx.Channel.SendMessageAsync(embed: winMessage);

            }
            else if (userCard.SelectedNumber == botCard.SelectedNumber) 
            {
                var drawMessage = new DiscordEmbedBuilder
                {
                    Title = $"DRAAAAAAAAAAW !!!!!!!!! ",
                    Color = DiscordColor.Gray,


                };
                await ctx.Channel.SendMessageAsync(embed: drawMessage);
            }
            else
            {
                var loseMessage = new DiscordEmbedBuilder
                {
                    Title = $" {ctx.User.Username.ToUpper()} Have lost the game HAHAHAHAHAHHA!!! ",
                    Color = DiscordColor.Red,


                };
                await ctx.Channel.SendMessageAsync(embed: loseMessage);

            }

        }
        [Command("quiz")]
        public async Task Quiz(CommandContext ctx)
        {

            var quiz = await new QuizSystem().CreateAsync();
            var Category = quiz.Category;

            string category = Category.Category;

            Random random = new Random();

            int randomindex = random.Next(Category.Questions.Count);

            Question questionstruct = Category.Questions[randomindex];
            string question = questionstruct.Text;

            List<string> options = questionstruct.Options;
            string answer = questionstruct.Answer;

            var embed = new DiscordEmbedBuilder()
          .WithTitle($"{category} Quiz")
          .WithDescription(question)
          .AddField("Options",
    $"- **{options[0]}**\n- **{options[1]}**\n- **{options[2]}**\n- **{options[3]}**")

          .WithColor(DiscordColor.Blurple)
          .Build();

            var message = await ctx.Channel.SendMessageAsync(embed:embed);

            bool quizOver = false;

            // Handle reaction added event
            ctx.Client.MessageCreated += async (client, e) =>
            {
                // Make sure it's not the bot's own message
                if (e.Author.IsBot) return;

                // Ensure the message was sent in the same channel
                if (e.Channel.Id != ctx.Channel.Id) return;
                if (e.Message.ReferencedMessage != message) return;

                if (quizOver == true) return;


                // Check if the message content matches one of the possible answers
                string userAnswer = e.Message.Content.ToUpper(); // Convert to uppercase for consistency


                if (userAnswer == answer.ToUpper())
                {
                    // Correct answer

                    await ctx.Channel.SendMessageAsync($"MABROOOOOOK{e.Author.Mention}!");

                    await message.ModifyAsync(msg => msg.Content = "**KHLASS L QUIZ !**");
                    quizOver = true;

                }
                else
                {
                    // Incorrect answer
                    await ctx.Channel.SendMessageAsync($"MONGOL  {e.Author.Mention} HHHH.");
                }


            };
        }
    }
}
