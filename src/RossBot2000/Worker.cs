using RossBot2000.Bot;

namespace RossBot2000;

public class Worker(ILogger<Worker> logger, DiscordClient discord) : BackgroundService
{
	private readonly ILogger<Worker> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
	private readonly DiscordClient _discord = discord ?? throw new ArgumentNullException(nameof(discord));

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		_logger.LogInformation("Worker running at: {Time}", DateTimeOffset.Now);
		await _discord.Login();
		
		while (!stoppingToken.IsCancellationRequested)
		{
			await Task.Delay(1000, stoppingToken);
		}
	}
}