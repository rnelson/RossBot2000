namespace RossBot2000.Abstractions;

public interface IBotConfiguration
{
    /// <summary>
    /// The bot's name.
    /// </summary>
    string BotName { get; }
    
    /// <summary>
    /// The command prefix.
    /// </summary>
    /// <remarks>
    /// This goes between "!" and the command name.
    /// </remarks>
    string CommandPrefix { get; }
}