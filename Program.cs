using RossBot2000;
using RossBot2000.Bot;

var host = Host.CreateDefaultBuilder(args)
	.ConfigureServices(services =>
	{
		services.AddHostedService<Worker>();
		services.AddSingleton<BotConfiguration>();
		services.AddSingleton<DiscordClient>();
	})
	.Build();

host.Run();