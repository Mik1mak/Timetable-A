﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableA.Entities.Models
{
    public class Lesson
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(64)")]
        public string Name { get; set; }

        public DateTime Start { get; set; }

        public TimeSpan Duration { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string Classroom { get; set; }

        [Column(TypeName = "nvarchar(512)")]
        public string Link { get; set; }

        [Required]
        [ForeignKey(nameof(Group))]
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
