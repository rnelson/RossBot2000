using Discord.Commands;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace RossBot2000.Module;

public class RossBotModuleBase(ILogger<RossBotModuleBase> logger, IConfiguration configuration) : ModuleBase<SocketCommandContext>
{
    protected IConfiguration Configuration { get; } = configuration
                                                            ?? throw new ArgumentNullException(nameof(configuration));

    private const string ModuleName = "<unnamed module>";

    [UsedImplicitly]
    public virtual void Initialize()
    {
        logger.LogInformation("Initializing {name}", ModuleName);
    }
}