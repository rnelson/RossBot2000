using System.Text;

namespace RossBot2000;

public class BotConfiguration
{
	private readonly ILogger<BotConfiguration> _logger;
	private readonly IConfiguration _configuration;

	internal string DiscordToken { get; private set; }
	public string BotName { get; private set; } = Constants.DEFAULT_BOT_NAME;
	public string CommandPrefix { get; private set; } = Constants.DEFAULT_COMMAND_PREFIX;
	
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
			CommandPrefix = Constants.DEFAULT_COMMAND_PREFIX;
		}
		
		try
		{
			BotName = _configuration["Bot:Name"]!.Trim();
		}
		catch
		{
			BotName = Constants.DEFAULT_BOT_NAME;
		}

		var message = new StringBuilder();
		message.AppendLine($"Loaded configuration for {BotName}:");
		message.AppendLine($"\tCommand Prefix: {CommandPrefix}");
		_logger.LogInformation(message!.ToString());
	}
}