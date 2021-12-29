namespace MinecraftManager.Core.Models;

public record ModFile
{
    public uint Id { get; init; }
    public string? DisplayName { get; init; }
    public string? FileName { get; init; }
    public DateTime FileDate { get; init; }
    public uint FileLength { get; init; }
    public byte ReleaseType { get; init; }
    public byte FileStatus { get; init; }
    public Uri? DownloadUrl { get; init; }
    public bool IsAlternate { get; init; }
    public uint AlternateFileId { get; init; }

    public IEnumerable<ModFileDependency>? Dependencies { get; init; }

    public bool IsAvailable { get; init; }

    //public IEnumerable<string> Modules { get;set;}

    public uint PackageFingerprint { get; init; }

    public IEnumerable<string>? GameVersion { get; init; }

    public string? InstallMetadata { get; init; }
    public string? ServerPackFileId { get; init; }
    public bool HasInstallScript { get; init; }
    public DateTime GameVersionDateReleased { get; init; }
    public string? GameVersionFlavor { get; init; }
}

public class ModFileDependency
{
    public uint AddonId { get; init; }
    public byte Type { get; init; }
}