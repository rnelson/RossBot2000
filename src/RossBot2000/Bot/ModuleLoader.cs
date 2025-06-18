using System.Reflection;
using Discord.Commands;
using RossBot2000.Abstractions;

namespace RossBot2000.Bot;

public class ModuleLoader(ILogger<ModuleLoader> logger, IServiceProvider provider)
{
    private readonly ILogger<ModuleLoader> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task LoadModules(CommandService commandService)
    {
        var path = AppDomain.CurrentDomain.BaseDirectory;
        path = "C:\\dev\\RossBot2000\\dist";
        var moduleDlls = Directory.GetFiles(path, "*Module.dll");

        foreach (var dll in moduleDlls)
        {
            try
            {
                _logger.LogInformation($"Loading module {dll}");
                var assembly = Assembly.LoadFile(dll);
                var types = assembly.GetTypes();

                foreach (var type in types)
                {
                    if (type.GetInterfaces().Contains(typeof(IRossBotModule)))
                    {
                        await commandService.AddModuleAsync(type, provider);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to load module {dll}", dll);
            }
        }
    }
}