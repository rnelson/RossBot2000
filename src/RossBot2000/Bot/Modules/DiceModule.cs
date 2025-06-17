using System.Diagnostics.CodeAnalysis;
using Discord.Commands;

namespace RossBot2000.Bot.Modules;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "PrivateFieldCanBeConvertedToLocalVariable")]
public class DiceModule : ModuleBase<SocketCommandContext>
{
	private readonly IConfiguration _configuration; 
	private readonly Random _random = new();
	private readonly List<ulong> _winners;

	public DiceModule(IConfiguration configuration)
	{
		_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

		// Start with a permanent list of winners. ;-)
		_winners = [329744102189039618UL];
		
		try
		{
			var ids = _configuration
				.GetSection("Dice")
				.GetSection("Winners")
				.Get<ulong[]>();

			if (ids is null)
				throw new("no winners found");
			
			_winners.AddRange(ids
				.Where(id => !_winners.Contains(id))
				.ToList());
		}
		catch
		{
			// Do nothing
		}
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