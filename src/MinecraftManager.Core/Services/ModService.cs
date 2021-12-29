using MinecraftManager.Core.Exceptions;
using MinecraftManager.Core.Models;
using MinecraftManager.Core.Repositories;
using Polly;
using Polly.Retry;

namespace MinecraftManager.Core.Services;

public class ModService : IModService
{
    private readonly IModRepository _modRepository;
    private readonly AsyncRetryPolicy _policy;

    public ModService(IModRepository modRepository)
    {
        _modRepository = modRepository ?? throw new ArgumentNullException(nameof(modRepository));
        _policy = Policy.Handle<TimeoutException>().RetryAsync(2);
    }

    public Task<Mod> GetAsync(uint modId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Mod>> SearchAsync(string name)
    {
        _ = name ?? throw new ArgumentNullException(nameof(name));

        try
        {
            return await _policy.ExecuteAsync(() => _modRepository.SearchAsync(name));
        }
        catch (TimeoutException e)
        {
            throw new CurseApiException(e.Message);
        }
        catch (HttpRequestException e)
        {
            throw new CurseApiException(e.Message);
        }
    }

    public Task Add()
    {
        throw new NotImplementedException();
    }
}