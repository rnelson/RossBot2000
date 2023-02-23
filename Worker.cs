namespace RossBot2000;

public class Worker : BackgroundService
{
	private readonly ILogger<Worker> _logger;
	private readonly DiscordClient _discord;

	public Worker(ILogger<Worker> logger, DiscordClient discord)
	{
		_logger = logger;
		_discord = discord ?? throw new ArgumentNullException(nameof(discord));
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
		await _discord.Login();
		
		while (!stoppingToken.IsCancellationRequested)
		{
			await Task.Delay(1000, stoppingToken);
		}
	}
}