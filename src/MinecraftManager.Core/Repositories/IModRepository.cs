using MinecraftManager.Core.Models;

namespace MinecraftManager.Core.Repositories;

/// <summary>
/// A repository to interact with mods in CurseApi.
/// </summary>
public interface IModRepository
{
    Task<Mod> GetAsync(uint id);
    Task<Mod> GetAsync(string name);
    Task DownloadAsync(uint id);
    
    /// <summary>
    /// Search a mod by name.
    /// </summary>
    /// <param name="name">The name of mod.</param>
    /// <returns>A list of <see cref="Mod"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when no name is provided.</exception>
    /// <exception cref="TimeoutException">Thrown when the API don't respond.</exception>
    /// <exception cref="HttpRequestException">Thrown when the API respond with an error.</exception>
    Task<IEnumerable<Mod>> SearchAsync(string name);
}