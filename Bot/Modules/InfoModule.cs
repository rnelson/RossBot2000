using System.Diagnostics.CodeAnalysis;
using System.Text;
using Discord;
using Discord.Commands;

namespace RossBot2000.Bot.Modules;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class InfoModule : ModuleBase<SocketCommandContext>
{
	private readonly BotConfiguration _configuration;

	public InfoModule(BotConfiguration configuration)
	{
		_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
	}
	
	[Command("about")]
	[Summary("About this bot.")]
	public async Task AboutAsync()
	{
		var description = new StringBuilder();
		description.AppendLine();
		description.AppendLine("**About**");
		description.AppendLine("Someone added Mee6 to a Discord server and");
		description.AppendLine("created a custom command. When asked to create");
		description.AppendLine("a second, they pointed out that you are limited");
		description.AppendLine("to just one. After pointing out that they could");
		description.AppendLine("easily make something to handle multiple commands,");
		description.AppendLine("I figured I should probably make sure that it's");
		description.AppendLine("actually easy. And thus RossBot2000 was born.");
		description.AppendLine("\n");
		description.AppendLine("**Author**");
		description.AppendLine("This useless little bot was written by Ross");
		description.AppendLine("Nelson. rnelson#2876");
		description.AppendLine("\n");
		description.AppendLine("**License**");
		description.AppendLine("The RossBot2000 source code is available under");
		description.AppendLine("the MIT license. See !rbsource.");
		description.AppendLine("\n");
		
		var embed = new EmbedBuilder
		{
			Title = _configuration.BotName,
			Color = new Color(87, 5, 116),
			Description = description.ToString()
		};

		await ReplyAsync(embed: embed.Build());
	}
	
	[Command("source")]
	[Summary("Links to the source code for this bot.")]
	public Task SourceAsync()
		=> ReplyAsync("The original bot source code (MIT licensed) is available at https://github.com/rnelson/RossBot2000");
}