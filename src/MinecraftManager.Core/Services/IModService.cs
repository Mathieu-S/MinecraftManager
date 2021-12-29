using MinecraftManager.Core.Models;

namespace MinecraftManager.Core.Services;

public interface IModService
{
    Task<Mod> GetAsync(uint modId);
    Task<IEnumerable<Mod>> SearchAsync(string name);
    Task Add();
}