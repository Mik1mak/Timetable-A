using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TimetableA.Models
{
    public class Timetable : IModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Column(TypeName = "nvarchar(64)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(16)")]
        public string ReadKey { get; set; }

        [Column(TypeName = "nvarchar(16)")]
        public string EditKey { get; set; }

        public int Cycles { get; set; }

        public bool DisplayEmptyDays { get; set; }

        public DateTime CreateDate { get; set; }

        public ICollection<Group> Groups { get; set; } = new List<Group>();
    }
}
