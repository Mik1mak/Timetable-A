using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableA.Entities.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(64)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(7)")]
        public string HexColor { get; set; }

        [ForeignKey(nameof(Timetable))]
        public int TimetableId { get; set; }
        public Timetable Timetable { get; set; }

        public ICollection<Lesson> Lessons { get; set; }

        [NotMapped]
        public ICollection<Group> ExcludingGroups { get; set; }
    }
}
