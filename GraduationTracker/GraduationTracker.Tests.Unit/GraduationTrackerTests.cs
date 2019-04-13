using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace GraduationTracker.Tests.Unit
{
    [TestClass]
    public class GraduationTrackerTests
    {
        private Diploma _diploma;
        private Student[] _students;

        public GraduationTrackerTests()
        {
            LoadData();
        }

        private void LoadData()
        {
            _diploma = new Diploma
            {
                Id = 1,
                Credits = 4,
                Requirements = new int[] { 100, 102, 103, 104 }
            };

            _students = new[]
            {
                new Student
                {
                   Id = 1,
                   Courses = new Course[]
                   {
                    new Course{Id = 1, Name = "Math", Mark=95 },
                    new Course{Id = 2, Name = "Science", Mark=95 },
                    new Course{Id = 3, Name = "Literature", Mark=95 },
                    new Course{Id = 4, Name = "Physichal Education", Mark=95 }
                   }
                },
                new Student
                {
                   Id = 2,
                   Courses = new Course[]
                   {
                        new Course{Id = 1, Name = "Math", Mark=80 },
                        new Course{Id = 2, Name = "Science", Mark=80 },
                        new Course{Id = 3, Name = "Literature", Mark=80 },
                        new Course{Id = 4, Name = "Physichal Education", Mark=80 }
                   }
                },
                new Student
                {
                    Id = 3,
                    Courses = new Course[]
                    {
                        new Course{Id = 1, Name = "Math", Mark=50 },
                        new Course{Id = 2, Name = "Science", Mark=50 },
                        new Course{Id = 3, Name = "Literature", Mark=50 },
                        new Course{Id = 4, Name = "Physichal Education", Mark=50 }
                    }
                },
                new Student
                {
                    Id = 4,
                    Courses = new Course[]
                    {
                        new Course{Id = 1, Name = "Math", Mark=40 },
                        new Course{Id = 2, Name = "Science", Mark=40 },
                        new Course{Id = 3, Name = "Literature", Mark=40 },
                        new Course{Id = 4, Name = "Physichal Education", Mark=40 }
                    }
                },
                                new Student
                {
                    Id = 5,
                    Courses = new Course[]
                    {
                        new Course{Id = 1, Name = "Math", Mark=80 },
                        new Course{Id = 2, Name = "Science", Mark=0 },
                        new Course{Id = 3, Name = "Literature", Mark=50 },
                        new Course{Id = 4, Name = "Physichal Education", Mark=40 }
                    }
                }
            };
        }

        [TestMethod]
        public void TestHasGraduated()
        {
            var tracker = new GraduationTracker();
            var graduated = new List<Tuple<bool, STANDING>>();

            foreach (var student in _students)
            {
                var result = tracker.HasGraduated(_diploma, student);
                if (result.Item1)
                    graduated.Add(result);
            }

            Assert.IsTrue(graduated.Any());
        }

        [TestMethod]
        public void TestGraduatedCount()
        {
            var tracker = new GraduationTracker();
            var graduated = new List<Tuple<bool, STANDING>>();

            foreach (var student in _students)
            {
                var result = tracker.HasGraduated(_diploma, student);
                if (result.Item1)
                    graduated.Add(result);
            }

            Assert.AreEqual(graduated.Count(), 3);
        }

        [TestMethod]
        public void TestNotGraduatedCount()
        {
            var tracker = new GraduationTracker();
            var notGraduated = new List<Tuple<bool, STANDING>>();

            foreach (var student in _students)
            {
                var result = tracker.HasGraduated(_diploma, student);
                if (!result.Item1)
                    notGraduated.Add(result);
            }

            Assert.AreEqual(notGraduated.Count(), 2);
        }

        [TestMethod]
        public void TestNull()
        {
            var tracker = new GraduationTracker();

            var notGraduated = new List<Tuple<bool, STANDING>>();
            var result = tracker.HasGraduated(null, null);
            if (!result.Item1 && result.Item2 == STANDING.None)
                notGraduated.Add(result);

            Assert.AreEqual(notGraduated.Count(), 1);
        }
    }
}
