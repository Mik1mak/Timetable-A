using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TimetableA.Models
{
    public class Group : IModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Column(TypeName = "nvarchar(64)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(7)")]
        public string HexColor { get; set; }

        [Required]
        [ForeignKey(nameof(Timetable))]
        public string TimetableId { get; set; }
        [JsonIgnore]
        public Timetable Timetable { get; set; }

        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

        public bool CollidesWith(Group group2)
        {
            foreach (var lesson in Lessons)
                foreach (var lesson2 in group2.Lessons)
                    if (lesson.CollideWith(lesson2))
                        return true;

            return false;
        }

        public IEnumerable<string> CollidingGroupsIds(IEnumerable<Group> groups) => groups?.Where(x => CollidesWith(x))?.Select(x => x.Id);
    }
}
