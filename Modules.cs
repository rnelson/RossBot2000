using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;
using Discord;
using Discord.Commands;

namespace RossBot2000;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class Modules : ModuleBase<SocketCommandContext>
{
	private readonly Random _random = new();
	private readonly ulong[] Winners =
	{
		329744102189039618UL // Ross
	};

	[Command("source")]
	[Summary("Links to the source code for this bot.")]
	public Task SourceAsync()
		=> ReplyAsync("My source is available at https://github.com/rnelson/RossBot2000");

	[Command("aioli")]
	[Alias("aaiga")]
	[Summary("Informs Austin that all aioli is, in fact, garlic aioli.")]
	public Task AioliAsync()
		=> ReplyAsync("Well actually, ALL aioli is garlic aioli!");

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

	[Command("mayonnaise")]
	[Alias("mayo")]
	[Summary("Provide some poignant thoughts on that condiment.")]
	public Task MayoAsync()
		=> ReplyAsync("https://tenor.com/view/disappointed-sigh-whatever-not-surprised-gif-22918448");

	[Command("help")]
	[Summary("Gets help on using the bot.")]
	[SuppressMessage("ReSharper", "UnusedParameter.Global")]
	public async Task HelpCommand([Remainder] string args = "")
	{
		var embed = new EmbedBuilder
		{
			Title = "This is the list of all commands for this bot",
			Color = new Color(10, 180, 10)
		};

		var modules = Assembly
			.GetExecutingAssembly()
			.GetTypes()
			.Where(t => t.IsSubclassOf(typeof(ModuleBase<SocketCommandContext>)))
			.ToList();

		var description = new StringBuilder();
		description.AppendLine();

		modules.ForEach(t =>
		{
			var moduleName = t.Name.Remove(t.Name.IndexOf("Module", StringComparison.CurrentCulture));
			description.AppendLine($"**{moduleName} Commands**");

			var methods = t.GetMethods(BindingFlags.Public | BindingFlags.Instance).ToList();

			var group = t.GetCustomAttribute<GroupAttribute>();

			methods.ForEach(mi =>
			{
				var command = mi.GetCustomAttribute<CommandAttribute>();
				if (command == null)
					return;

				var summary = mi.GetCustomAttribute<SummaryAttribute>();
				var aliases = mi.GetCustomAttribute<AliasAttribute>();
				var groupName = "";

				if (group != null) groupName = group.Prefix + " ";

				description.Append($"**{Constants.COMMAND_PREFIX}{groupName}{command.Text}**");

				if (aliases != null)
					Array.ForEach(aliases.Aliases,
						a => description.Append($" or **{Constants.COMMAND_PREFIX}{groupName}{(a == "**" ? "\\*\\*" : a)}**"));

				if (summary != null)
					description.Append($"\n{summary.Text}");

				description.AppendLine("\n");
			});
		});

		embed.Description = description.ToString();

		await ReplyAsync(embed: embed.Build());
	}
}