using Flurl;
using Flurl.Http;
using MinecraftManager.Core.Models;

namespace MinecraftManager.Core.Repositories;

/// <inheritdoc />
public class ModRepository : IModRepository
{
    private readonly Url _curseApi;

    public ModRepository()
    {
        _curseApi = new Url("https://addons-ecs.forgesvc.net/api/v2/addon");
    }

    public async Task<Mod> GetAsync(uint id)
    {
        _ = id == 0 ? throw new ArgumentException("modId can't be 0.", nameof(id)) : 0;

        return await _curseApi.AppendPathSegment(id.ToString()).GetJsonAsync<Mod>();
    }

    public Task<Mod> GetAsync(string name)
    {
        throw new NotImplementedException();
    }

    public Task DownloadAsync(uint id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Mod>> SearchAsync(string name)
    {
        _ = name ?? throw new ArgumentNullException(nameof(name));

        try
        {
            return await _curseApi
                .AppendPathSegment("search")
                .SetQueryParams(new
                {
                    gameId = "432",
                    sectionId = "6",
                    searchFilter = name
                })
                .GetJsonAsync<IEnumerable<Mod>>();
        }
        catch (FlurlHttpTimeoutException e) {
            throw new TimeoutException(e.Message, e.InnerException);
        }
        catch (FlurlHttpException e) {
            throw new HttpRequestException(e.Message, e.InnerException);
        }
    }
}