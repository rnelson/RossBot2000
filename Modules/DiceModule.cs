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
			
			Winners.AddRange(ids.ToList());
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
}