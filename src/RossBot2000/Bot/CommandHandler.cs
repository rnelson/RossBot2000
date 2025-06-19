using System.Reflection;
using Discord.Commands;
using Discord.WebSocket;

namespace RossBot2000.Bot;

public class CommandHandler(
    BotConfiguration configuration,
    IServiceProvider services,
    DiscordSocketClient client,
    CommandService commands)
{
    private readonly BotConfiguration _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    private readonly IServiceProvider _services = services ?? throw new ArgumentNullException(nameof(services));
    private readonly DiscordSocketClient _client = client ?? throw new ArgumentNullException(nameof(client));
    private readonly CommandService _commands = commands ?? throw new ArgumentNullException(nameof(commands));

    public async Task InstallCommandsAsync()
    {
        _client.MessageReceived += HandleCommandAsync;
        await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(),
                                        services: _services);
    }

    private async Task HandleCommandAsync(SocketMessage messageParam)
    {
        // Don't process the command if it was a system message
        if (messageParam is not SocketUserMessage { Channel: SocketGuildChannel } message)
            return;

        // Create a number to track where the prefix ends and the command begins
        var argPos = 0;

        // Determine if the message is a command based on the prefix and make sure no bots trigger commands
        if (!(message.HasStringPrefix($"{_configuration.CommandPrefix} ", ref argPos) ||
            message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
            message.Author.IsBot)
            return;

        // Create a WebSocket-based command context based on the message
        var context = new SocketCommandContext(_client, message);

        // Execute the command with the command context we just
        // created, along with the service provider for precondition checks.
        await _commands.ExecuteAsync(
            context: context,
            argPos: argPos,
            services: _services);
    }
}