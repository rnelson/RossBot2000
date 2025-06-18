using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;
using Discord;
using Discord.Commands;
using RossBot2000.Module;

namespace RossBot2000.Bot.Modules;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class HelpModule(BotConfiguration configuration, IServiceProvider provider) : ModuleBase<SocketCommandContext>
{
	private readonly BotConfiguration _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

	[Command("help")]
	[Summary("Gets help on using the bot.")]
	[SuppressMessage("ReSharper", "UnusedParameter.Global")]
	public async Task HelpCommand([Remainder] string args = "")
	{
		var embed = new EmbedBuilder
		{
			Title = $"This is the list of all commands for {_configuration.BotName}",
			Color = new Color(10, 180, 10)
		};

		var modules = new List<Type>();
		foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
			modules.AddRange(assembly.GetTypes().Where(t => !t.IsInterface
			                                                && t != typeof(RossBotModuleBase)
			                                                && t.IsSubclassOf(typeof(ModuleBase<SocketCommandContext>))));

		var description = new StringBuilder();
		description.AppendLine();

		modules.ForEach(t =>
		{
			var moduleName = t.Name.Remove(t.Name.IndexOf("Module", StringComparison.CurrentCulture));
			description.AppendLine($"## **{moduleName} Commands**");

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

				description.Append($"**{_configuration.CommandPrefix} {groupName}{command.Text}**");

				if (aliases != null)
					Array.ForEach(aliases.Aliases,
						a => description.Append($" or **{_configuration.CommandPrefix} {groupName}{(a == "**" ? @"\*\*" : a)}**"));

				if (summary != null)
					description.Append($"\n{summary.Text}");

				description.AppendLine("\n");
			});
		});

		embed.Description = description.ToString();

		await ReplyAsync(embed: embed.Build());
	}
}