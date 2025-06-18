using System.Diagnostics.CodeAnalysis;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RossBot2000.Abstractions;
using RossBot2000.Module;

namespace DiceModule;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "PrivateFieldCanBeConvertedToLocalVariable")]
public class DiceModule(ILogger<DiceModule> logger, IConfiguration configuration) : RossBotModuleBase(logger, configuration), IRossBotModule
{
	private string Name { get; init; } = "DiceModule";
	
	private readonly Random _random = new();
	private readonly List<ulong> _winners = [329744102189039618UL];
	

	public override void Initialize()
	{
		logger.LogInformation("Initializing {ModuleName}", Name);
		
		logger.LogDebug("Loading list of winners from the configuration");
		var ids = Configuration
			.GetSection("Dice")
			.GetSection("Winners")
			.Get<ulong[]>();

		if (ids is null)
			throw new("no winners found");
		
		logger.LogTrace("Moving list of winner IDs to _winners");
		_winners.AddRange(ids
			.Where(id => !_winners.Contains(id))
			.ToList());
		
		logger.LogDebug("{ModuleName} initialized", Name);
	}
	
	[Command("SavingThrow")]
	[Alias("d20st")]
	[Summary("Rolls a saving throw.")]
	public Task SavingThrowAsync()
	{
		if (_winners.Contains(Context.User.Id))
		{
			return ReplyAsync($"You rolled a {_random.Next(16, 21)}");
		}

		var luck = _random.NextDouble();
		return ReplyAsync(luck >= 0.75
			? $"You rolled a {_random.Next(1, 21)}"
			: $"You rolled a {_random.Next(1, 10)}");
	}
	
	[Command("d")]
	[Alias("roll")]
	[Summary("Rolls an N-sided die.")]
	public Task RollAsync([Remainder] string args = "")
	{
		try
		{
			if (args.Trim().Length == 0)
				return ReplyAsync("Please provide the number of sides for the N-sided die to roll.");
			
			var bits = args.Split(' ', '\t');
			var sides = long.Parse(bits[0]);
			
			return ReplyAsync($"You rolled a {_random.NextInt64(1L, sides + 1)}.");
		}
		catch (Exception e)
		{
			return ReplyAsync($"You broke me! {e.Message}");
		}
	}
}