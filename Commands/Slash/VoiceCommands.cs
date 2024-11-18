using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.VoiceNext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace TestApplication.Commands
{
   

    public class VoiceCommands : ApplicationCommandModule
    {
        private readonly VoiceNextExtension _voiceNext;

        public VoiceCommands(VoiceNextExtension voiceNext)
        {
            _voiceNext = voiceNext;
        }

        [SlashCommand("join","sadas")]
        public async Task JoinVoiceChannelAsync(InteractionContext ctx)
        {
            await ctx.DeferAsync();
            // Get the voice channel the user is currently in
            var voiceChannel = ctx.Member?.VoiceState?.Channel;
            if (voiceChannel == null)
            {
                
                await ctx.Channel.SendMessageAsync("You need to be in a voice channel first.");
                return;
            }

            // Connect to the voice channel
            var voiceConnection = await _voiceNext.ConnectAsync(voiceChannel);
            await ctx.Channel.SendMessageAsync($"Joined {voiceChannel.Name}.");
        }

        [SlashCommand("leave","sadada")]
        public async Task LeaveVoiceChannelAsync(InteractionContext ctx)
        {
            await ctx.DeferAsync();
            // Get the current voice connection
            var voiceConnection = _voiceNext.GetConnection(ctx.Guild);
            if (voiceConnection == null)
            {
                await ctx.Channel.SendMessageAsync("I am not currently in a voice channel.");
                return;
            }

            // Disconnect the bot from the voice channel
            voiceConnection.Disconnect();
            await ctx.Channel.SendMessageAsync("Left the voice channel.");
        }

        [SlashCommand("togglejoinleave", "Make the bot join and leave the voice channel multiple times")]
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
    }

}
