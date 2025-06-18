using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace RossBot2000.Bot;

public class DiscordClient
{
	private readonly BotConfiguration _configuration;
	private readonly ILogger<DiscordClient> _logger;
	private readonly DiscordSocketClient _client;
	private readonly IServiceProvider _services;
	private readonly ModuleLoader _moduleLoader;

	public DiscordClient(BotConfiguration configuration,
		ILogger<DiscordClient> logger,
		IServiceProvider services,
		ModuleLoader moduleLoader)
	{
		_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		_services = services ?? throw new ArgumentNullException(nameof(services));
		_moduleLoader = moduleLoader ?? throw new ArgumentNullException(nameof(moduleLoader));

		_client = new(new() { GatewayIntents = GatewayIntents.All });
		_client.Log += ClientOnLog;
	}

	public async Task Login()
	{
		var commandService = new CommandService();
		await _moduleLoader.LoadModules(commandService);
		
		var handler = new CommandHandler(_configuration, _services, _client, commandService);
		
		await handler.InstallCommandsAsync();
		
		await _client.LoginAsync(TokenType.Bot, _configuration.DiscordToken);
		await _client.StartAsync();
		
		_client.MessageUpdated += MessageUpdated;
		_client.MessageReceived += MessageReceived;
	}

	private async Task ClientOnLog(LogMessage arg)
	{
		_logger.LogDebug("{Message}", arg.Message);
		await Task.Delay(0);
	}
	
	private static async Task MessageReceived(SocketMessage message)
	{
		await Task.Run(() =>
		{
			Console.WriteLine($"[{message.Timestamp.ToString()}] #{message.Channel} {message.Author}): {message.Content}");
			return Task.CompletedTask;
		});
	}

	private static async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
	{
		await Task.Run(async () =>
		{
			var message = await before.GetOrDownloadAsync();
			Console.WriteLine($"[{message.Timestamp.ToString()}] #{message.Channel} {message.Author}): {message} -> {after}");
		});
	}
}