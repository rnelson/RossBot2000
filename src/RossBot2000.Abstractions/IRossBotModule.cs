namespace RossBot2000.Abstractions;

public interface IRossBotModule
{
    /// <summary>
    /// The module's name.
    /// </summary>
    string Name { get; init; }
}