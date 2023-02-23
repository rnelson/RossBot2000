using System.Diagnostics.CodeAnalysis;
using Discord.Commands;

namespace RossBot2000.Modules;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class InfoModule : ModuleBase<SocketCommandContext>
{
	[Command("source")]
	[Summary("Links to the source code for this bot.")]
	public Task SourceAsync()
		=> ReplyAsync("My source is available at https://github.com/rnelson/RossBot2000");
}