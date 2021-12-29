namespace MinecraftManager.Core.Models;

public record Mod
{
    public uint Id { get; init; }
    public string? Name { get; init; }

    public IEnumerable<Author>? Authors { get; init; }
    public IEnumerable<Attachment>? Attachments { get; init; }

    public Uri? WebsiteUrl { get; init; }
    public ushort GameId { get; init; }
    public string? Summary { get; init; }
    public uint DefaultFileId { get; init; }
    public float DownloadCount { get; init; } // A float for download count ??? Really Curse ????!!!!

    //public IEnumerable<string> LatestFiles { get; set; }
    //public IEnumerable<string> Categories { get; set; }

    public byte Status { get; init; }
    public ushort PrimaryCategoryId { get; init; }

    //public IEnumerable<string> CategorySection { get; set; }

    public string? Slug { get; init; }

    //public IEnumerable<string> GameVersionLatestFiles { get; set; }

    public bool IsFeatured { get; init; }
    public float PopularityScore { get; init; }
    public uint GamePopularityRank { get; init; }
    public string? PrimaryLanguage { get; init; }
    public string? GameSlug { get; init; }
    public string? GameName { get; init; }
    public string? PortalName { get; init; }
    public DateTime DateModified { get; init; }
    public DateTime DateCreated { get; init; }
    public DateTime DateReleased { get; init; }
    public bool IsAvailable { get; init; }
    public bool IsExperiemental { get; init; }
}

public class Author
{
    public string? Name { get; init; }
    public Uri? Url { get; init; }
    public uint ProjectId { get; init; }
    public uint Id { get; init; }
    public string? ProjectTitleTitle { get; init; }
    public uint UserId { get; init; }
    public uint? TwitchId { get; init; }
}

public class Attachment
{
    public uint Id { get; init; }
    public uint ProjectId { get; init; }
    public string? Description { get; init; }
    public bool IsDefault { get; init; }
    public Uri? ThumbnailUrl { get; init; }
    public string? Title { get; init; }
    public Uri? Url { get; init; }
    public byte Status { get; init; }
}