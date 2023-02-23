using System.Diagnostics.CodeAnalysis;
using Discord.Commands;

namespace RossBot2000.Modules;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class DiceModule : ModuleBase<SocketCommandContext>
{
	private readonly Random _random = new();
	private readonly ulong[] Winners =
	{
		329744102189039618UL // Ross
	};
	
	[Command("SavingThrow")]
	[Alias("d20st")]
	[Summary("Rolls a saving throw.")]
	public Task SavingThrowAsync()
	{
		if (Winners.Contains(Context.User.Id))
		{
			return ReplyAsync($"You rolled a: {_random.Next(16, 21)}");
		}

		var luck = _random.NextDouble();
		return ReplyAsync(luck >= 0.75
			? $"You rolled a: {_random.Next(1, 21)}"
			: $"You rolled a: {_random.Next(1, 10)}");
	}
}