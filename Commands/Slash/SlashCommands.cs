using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.config;
using TestApplication.Other;
using DSharpPlus.VoiceNext;


namespace TestApplication.Commands.Slash
{
    public class SlashCommands : ApplicationCommandModule
    {

       

        [SlashCommand("Say", "Tell the slave what to say")]
        public async Task Say(InteractionContext ctx,
        [Option("user", "The user you want to say to")] DiscordUser user,
        [Option("message", "The message the bot should say")] string say)
        {
            await ctx.DeferAsync();

            // Retrieve the member from the guild using the user ID
            

            // Check if the user is authorized to make the command
            if (ctx.User.Username == "fazzyri" ||ctx.User.Username== "sa3ge")
            {
                // Use the user's nickname, or fallback to username if no nickname exists
                var memberName = user.Mention;
                
                await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent($"{memberName} {say}"));
            }
            else
            {
                await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Man9olch hhh"));
            }
        }

        [SlashCommand("Taswira","Tswira ta3o")]

        public async Task taswira(InteractionContext ctx,[Option("user","chkon nadilo tswira")]DiscordUser user) 
        {

            var discordMember=ctx.Channel.Guild.GetMemberAsync(user.Id).Result;
            await ctx.DeferAsync();
             
            var embedmessage = new DiscordEmbedBuilder()
            {
                ImageUrl=discordMember.AvatarUrl,
            };

            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embedmessage));
        
        
        }

        [SlashCommand("quiz", "Start a quiz")]
        public async Task Quiz(InteractionContext ctx)
        {
            string[] ReactionEmojis = { "A", "B", "C", "D" }; // Corresponding to options A, B, C, D
    
             var quiz = await new QuizSystem().CreateAsync();
            var Category =  quiz.Category;

            string category = Category.Category;

            Random random = new Random();

            int randomindex=random.Next(Category.Questions.Count);

            Question questionstruct = Category.Questions[randomindex];
            string question= questionstruct.Text;

            List<string> options = questionstruct.Options;
            string answer = questionstruct.Answer;

            var embed = new DiscordEmbedBuilder()
          .WithTitle($"{category} Quiz")
          .WithDescription(question)
          .AddField("Options",
    $"- **{options[0]}**\n- **{options[1]}**\n- **{options[2]}**\n- **{options[3]}**")

          .WithColor(DiscordColor.Blurple)
          .Build();
            await ctx.DeferAsync();

            var message =await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));

            bool quizOver = false;

            // Handle reaction added event
            ctx.Client.MessageCreated += async (client, e) =>
            {
                // Make sure it's not the bot's own message
                if (e.Author.IsBot) return;

                 if(e.Message.ReferencedMessage != message) return;

                // Ensure the message was sent in the same channel
                if (e.Channel.Id != ctx.Channel.Id) return;
                if (quizOver == true) return;


                // Check if the message content matches one of the possible answers
                string userAnswer = e.Message.Content.ToUpper(); // Convert to uppercase for consistency

                
                    if (userAnswer == answer.ToUpper())
                    {
                        // Correct answer
                        await ctx.Channel.SendMessageAsync($"MABROOOOOOK{e.Author.Mention}!");
                        await message.ModifyAsync(msg => msg.Content = "KHLASS L QUIZ !");
                        quizOver = true;

                }
                else
                    {
                        // Incorrect answer
                        await ctx.Channel.SendMessageAsync($"MONGOL  {e.Author.Mention} HHHH.");
                    }
                
                
            };
        }

  /*      [SlashCommand("togglejoin", "Make the bot join and leave the voice channel multiple times")]
        public async Task ToggleJoinLeaveAsync(InteractionContext ctx, [Option("user", "The user whose voice channel the bot will join and leave")] DiscordUser user)
        {
            await ctx.DeferAsync();

            try
            {
                // Get the member from the guild using the provided user
                var member = await ctx.Guild.GetMemberAsync(user.Id);
                var voiceChannel = member.VoiceState?.Channel;
                if (voiceChannel == null)
                {
                    await ctx.Channel.SendMessageAsync($"{user.Username} is not in a voice channel.");
                    return;
                }

                // Perform the join/leave action multiple times
                for (int i = 0; i < 3; i++) // Example: join/leave 3 times
                {
                    var voiceConnection = await _voiceNext.ConnectAsync(voiceChannel);
                    await ctx.Channel.SendMessageAsync($"Joined {voiceChannel.Name}.");

                    // Wait for 3 seconds before disconnecting
                    await Task.Delay(3000);

                    // Disconnect from the voice channel
                    voiceConnection.Disconnect();
                    await ctx.Channel.SendMessageAsync($"Left {voiceChannel.Name}.");

                    // Wait before repeating
                    await Task.Delay(3000); // Wait for 3 seconds before repeating
                }
            }
            catch (Exception ex)
            {
                // Log any error that might occur
                await ctx.Channel.SendMessageAsync($"An error occurred: {ex.Message}");
            }
        }


        */
    }
}
