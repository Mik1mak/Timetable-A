using System;
using System.Collections.Generic;
using TimetableA.Models;

namespace TimetableA.Tests.Models
{
    public partial class LessonCollidingTest
    {
        private static IEnumerable<Lesson> GetNoCollidingLessons()
        {
            yield return new Lesson
            {
                Start = DateTime.MinValue.AddHours(10),
                Duration = TimeSpan.FromMinutes(30),
            };
            yield return new Lesson
            {
                Start = DateTime.MinValue.AddHours(12),
                Duration = TimeSpan.FromMinutes(30),
            };
            yield return new Lesson
            {
                Start = DateTime.MinValue.AddHours(13).AddMinutes(15),
                Duration = TimeSpan.FromMinutes(30),
            };
            yield return new Lesson
            {
                Start = DateTime.MinValue.AddHours(14),
                Duration = TimeSpan.FromMinutes(30),
            };
        }

        private static IEnumerable<Lesson> GetCollidingLessons()
        {
            yield return new Lesson
            {
                Start = DateTime.MinValue.AddHours(12).AddMinutes(5),
                Duration = TimeSpan.FromMinutes(30),
            };
            yield return new Lesson
            {
                Start = DateTime.MinValue.AddHours(12).AddMinutes(30),
                Duration = TimeSpan.FromMinutes(30),
            };
            yield return new Lesson
            {
                Start = DateTime.MinValue.AddHours(12).AddMinutes(40),
                Duration = TimeSpan.FromMinutes(30),
            };
            yield return new Lesson
            {
                Start = DateTime.MinValue.AddHours(13),
                Duration = TimeSpan.FromMinutes(30),
            };
            yield return new Lesson
            {
                Start = DateTime.MinValue.AddHours(12).AddMinutes(0),
                Duration = TimeSpan.FromMinutes(120),
            };
            yield return new Lesson
            {
                Start = DateTime.MinValue.AddHours(12).AddMinutes(0),
                Duration = TimeSpan.FromMinutes(75),
            };
        }
    }
}
