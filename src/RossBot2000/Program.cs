using RossBot2000;
using RossBot2000.Bot;

var host = Host.CreateDefaultBuilder(args)
	.UseSystemd()
	.ConfigureServices(services =>
	{
		services.AddHostedService<Worker>();
		services.AddSingleton<BotConfiguration>();
		services.AddSingleton<DiscordClient>();
		services.AddSingleton<ModuleLoader>();
	})
	.Build();

host.Run();