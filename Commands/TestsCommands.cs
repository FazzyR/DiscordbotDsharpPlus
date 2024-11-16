using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Other;

namespace TestApplication.Commands
{
    public class TestsCommands:BaseCommandModule    
    {
        

        [Command("add")]
        public async Task add(CommandContext ctx,int num1,int num2)
        {
            int result = num1 + num2;
            await ctx.Channel.SendMessageAsync(result.ToString());
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

            // Send the response
            await ctx.Channel.SendMessageAsync(response);
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
        public TestsCommands() { }
    }
}
