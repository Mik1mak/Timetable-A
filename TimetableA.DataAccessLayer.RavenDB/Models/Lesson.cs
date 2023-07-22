namespace TimetableA.DataAccessLayer.RavenDB.Models;

internal class Lesson : IDocument
{
    public string? Id { get; set; }

    public Lesson() { }

    public Lesson(int duration)
    {
        Duration = TimeSpan.FromMinutes(duration);
    }

    public string Name { get; set; }

    public DateTime Start { get; set; }

    public TimeSpan Duration { get; set; }

    public string Classroom { get; set; }

    public string Link { get; set; }

    public string? GroupId { get; set; }
}
