using RossBot2000;

var host = Host.CreateDefaultBuilder(args)
	.ConfigureServices(services =>
	{
		services.AddHostedService<Worker>();
		services.AddSingleton<DiscordClient>();
	})
	.Build();

host.Run();