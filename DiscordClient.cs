using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace RossBot2000;

public class DiscordClient
{
	private readonly ILogger<DiscordClient> _logger;
	private readonly DiscordSocketClient _client;
	private readonly IServiceProvider _services;
	private readonly string _token;

	public DiscordClient(ILogger<DiscordClient> logger, IServiceProvider services)
	{
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		_services = services ?? throw new ArgumentNullException(nameof(services));

		_token = File.ReadAllText(".discord.token").Trim();

		_client = new DiscordSocketClient(new DiscordSocketConfig { GatewayIntents = GatewayIntents.All });
		_client.Log += ClientOnLog;
	}

	public async Task Login()
	{
		var commandService = new CommandService();
		var handler = new CommandHandler(_services, _client, commandService);
		
		await handler.InstallCommandsAsync();
		await _client.LoginAsync(TokenType.Bot, _token);
		await _client.StartAsync();
		
		_client.MessageUpdated += MessageUpdated;
		_client.MessageReceived += MessageReceived;
	}

	private async Task ClientOnLog(LogMessage arg)
	{
		_logger.LogDebug(arg.Message);
		await Task.Delay(0);
	}
	
	private async Task MessageReceived(SocketMessage msg)
	{
		await Task.Run(() =>
		{
			Console.WriteLine($"[{msg.Timestamp.ToString()}] #{msg.Channel} {msg.Author}): {msg.Content}");
			return Task.CompletedTask;
		});
	}

	private async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
	{
		await Task.Run(async () =>
		{
			var message = await before.GetOrDownloadAsync();
			Console.WriteLine($"({message.Channel}, {message.Author}): {message} -> {after}");
		});
	}
}