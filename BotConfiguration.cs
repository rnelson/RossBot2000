namespace RossBot2000;

public class BotConfiguration
{
	private readonly IConfiguration _configuration;

	public string BotName { get; private set; } = Constants.DEFAULT_BOT_NAME;
	public string CommandPrefix { get; private set; } = Constants.DEFAULT_COMMAND_PREFIX;
	
	public BotConfiguration(IConfiguration configuration)
	{
		_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

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
	}
}