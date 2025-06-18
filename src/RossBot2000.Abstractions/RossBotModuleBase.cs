using Discord.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace RossBot2000.Abstractions;

public class RossBotModuleBase(ILogger<RossBotModuleBase> logger, IConfiguration configuration) : ModuleBase<SocketCommandContext>, IRossBotModule
{
    protected IConfiguration Configuration { get; init; } = configuration
                                                            ?? throw new ArgumentNullException(nameof(configuration));

    public string Name { get; init; } = "<unnamed module>";

    public virtual void Initialize()
    {
        logger.LogInformation("Initializing {name}", Name);
    }
}