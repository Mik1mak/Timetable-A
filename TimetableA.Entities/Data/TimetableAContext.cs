using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableA.Models;

namespace TimetableA.DataAccessLayer.EntityFramework.Data;

public class TimetableAContext : DbContext
{
    public TimetableAContext(DbContextOptions<TimetableAContext> options) : base(options)
    {

    }

    public DbSet<Timetable> Timetables { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
}
