using GradeBook.Enums;
using System;
using System.Linq;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");

            var index = (int)Math.Ceiling(Students.Count * 0.20);

            var orderedGrades = Students
                .OrderByDescending(e => e.AverageGrade)
                .Select(e => e.AverageGrade)
                .ToList();

            if (orderedGrades[index - 1] <= averageGrade)
                return 'A';
            if (orderedGrades[index * 2 - 1] <= averageGrade)
                return 'B';
            if (orderedGrades[index * 3 - 1] <= averageGrade)
                return 'C';
            if (orderedGrades[index * 4 - 1] <= averageGrade)
                return 'D';
            return 'F';
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            
            base.CalculateStatistics();
        }
    }
}
