using NUnit.Framework;
using System;
using System.Collections.Generic;
using TimetableA.Models;

namespace TimetableA.Tests.Models
{
    public partial class LessonCollidingTest
    {
        private Lesson lesson1;

        [SetUp]
        public void Setup()
        {
            lesson1 = new Lesson
            {
                Name = nameof(lesson1),
                Start = DateTime.MinValue
                    .AddHours(12)
                    .AddMinutes(30),
                Duration = TimeSpan.FromMinutes(45),
            };
        }

        [Test]
        [TestCaseSource(nameof(GetCollidingLessons))]
        [TestCaseSource(nameof(GetNoCollidingLessons))]
        public void Commutative(Lesson lesson2)
        {
            Assert.IsTrue(lesson1.CollideWith(lesson2) == lesson2.CollideWith(lesson1));
        }

        [Test]
        [TestCaseSource(nameof(GetCollidingLessons))]
        public void ProperColliding(Lesson lesson2)
        {
            Assert.IsTrue(lesson1.CollideWith(lesson2));
        }

        [Test]
        [TestCaseSource(nameof(GetNoCollidingLessons))]
        public void ProperNoColliding(Lesson lesson2)
        {
            Assert.IsFalse(lesson1.CollideWith(lesson2));
        }
    }
}