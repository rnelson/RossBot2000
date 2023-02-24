using System.Diagnostics.CodeAnalysis;
using Discord.Commands;

namespace RossBot2000.Modules;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "PrivateFieldCanBeConvertedToLocalVariable")]
public class DiceModule : ModuleBase<SocketCommandContext>
{
	private readonly IConfiguration _configuration; 
	private readonly Random _random = new();
	private readonly List<ulong> Winners;

	public DiceModule(IConfiguration configuration)
	{
		_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

		// Start with a permanent list of winners. ;-)
		Winners = new List<ulong>
		{
			329744102189039618UL // rnelson#2876
		};
		
		try
		{
			var ids = _configuration
				.GetSection("Dice")
				.GetSection("Winners")
				.Get<ulong[]>();

			if (ids is null)
				throw new Exception("no winners found");
			
			Winners.AddRange(ids
				.Where(id => !Winners.Contains(id))
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
		if (Winners.Contains(Context.User.Id))
		{
			return ReplyAsync($"You rolled a: {_random.Next(16, 21)}");
		}

		var luck = _random.NextDouble();
		return ReplyAsync(luck >= 0.75
			? $"You rolled a: {_random.Next(1, 21)}"
			: $"You rolled a: {_random.Next(1, 10)}");
	}
	
	[Command("d")]
	[Alias("roll")]
	[Summary("Rolls an N-sided die.")]
	public Task RollAsync([Remainder] string args = "")
	{
		try
		{
			var bits = args.Split(new[] {' ', '\t'});
			var sides = long.Parse(bits[0]);
			
			return ReplyAsync($"You rolled a: {_random.NextInt64(1L, sides + 1)}");
		}
		catch (Exception e)
		{
			return ReplyAsync($"You broke me! {e.Message}");
		}
	}
}