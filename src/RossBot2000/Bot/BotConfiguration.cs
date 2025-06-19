using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace RossBot2000.Bot;

[SuppressMessage("ReSharper", "PrivateFieldCanBeConvertedToLocalVariable")]
public class BotConfiguration
{
	private readonly ILogger<BotConfiguration> _logger;
	private readonly IConfiguration _configuration;

	internal string DiscordToken { get; }
	
	/// <summary>
	/// The bot's name.
	/// </summary>
	public string BotName { get; }
	
	/// <summary>
	/// The command prefix.
	/// </summary>
	/// <remarks>
	/// This goes between "!" and the command name.
	/// </remarks>
	public string CommandPrefix { get; }
	
	public BotConfiguration(ILogger<BotConfiguration> logger, IConfiguration configuration)
	{
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

		try
		{
			DiscordToken = _configuration["Discord:Token"]!.Trim();
		}
		catch
		{
			DiscordToken = string.Empty;
		}

		try
		{
			CommandPrefix = _configuration["Bot:CommandPrefix"]!.Trim();
		}
		catch
		{
			CommandPrefix = Constants.DefaultCommandPrefix;
		}
		
		try
		{
			BotName = _configuration["Bot:Name"]!.Trim();
		}
		catch
		{
			BotName = Constants.DefaultBotName;
		}

		var message = new StringBuilder();
		message.AppendLine($"Loaded configuration for {BotName}:");
		message.AppendLine($"\tCommand Prefix: {CommandPrefix}");
		_logger.LogInformation("{Message}", message.ToString());
	}
}