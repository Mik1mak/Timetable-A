namespace TimetableA.DataAccessLayer.RavenDB.StoreHolder;

public sealed class RavenDbSettings
{
    public const string Position = "RavenDBSettings";
    public string[] Urls { get; set; } = Array.Empty<string>();
    public string Database { get; set; } = string.Empty;
    public string? CertificatePath { get; set; }
}