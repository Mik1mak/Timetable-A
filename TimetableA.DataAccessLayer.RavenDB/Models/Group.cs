namespace TimetableA.DataAccessLayer.RavenDB.Models;

internal class Group : IDocument
{
    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? HexColor { get; set; }

    public string? TimetableId { get; set; }

    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
