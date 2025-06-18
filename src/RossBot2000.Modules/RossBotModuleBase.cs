using Discord.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RossBot2000.Abstractions;

namespace RossBot2000.Module;

public class RossBotModuleBase(ILogger<RossBotModuleBase> logger, IConfiguration configuration) : ModuleBase<SocketCommandContext>
{
    protected IConfiguration Configuration { get; init; } = configuration
                                                            ?? throw new ArgumentNullException(nameof(configuration));

    private const string ModuleName = "<unnamed module>";

    public virtual void Initialize()
    {
        logger.LogInformation("Initializing {name}", ModuleName);
    }
}