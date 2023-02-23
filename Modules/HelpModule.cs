using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;
using Discord;
using Discord.Commands;

namespace RossBot2000.Modules;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class HelpModule : ModuleBase<SocketCommandContext>
{
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