using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TimetableA.Entities.Models
{
    public class Group : IModel
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(64)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(7)")]
        public string HexColor { get; set; }

        [Required]
        [ForeignKey(nameof(Timetable))]
        public int TimetableId { get; set; }
        [JsonIgnore]
        public Timetable Timetable { get; set; }

        public ICollection<Lesson> Lessons { get; set; }
    }
}
