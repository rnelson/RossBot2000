using System.Diagnostics.CodeAnalysis;
using Discord.Commands;

namespace RossBot2000.Bot.Modules;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class FridayNightsModule : ModuleBase<SocketCommandContext>
{
	[Command("aioli")]
	[Alias("aaiga")]
	[Summary("Informs Austin that all aioli is, in fact, garlic aioli.")]
	public Task AioliAsync()
		=> ReplyAsync("Well actually, ALL aioli is garlic aioli!");

	[Command("mayonnaise")]
	[Alias("mayo")]
	[Summary("Provide some poignant thoughts on that condiment.")]
	public Task MayoAsync()
		=> ReplyAsync("https://tenor.com/view/disappointed-sigh-whatever-not-surprised-gif-22918448");
}