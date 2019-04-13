using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationTracker
{
    public partial class GraduationTracker
    {
        public Tuple<bool, STANDING> HasGraduated(Diploma diploma, Student student)
        {
            try
            {
                var creditsAverage = GetCreditAverage(diploma, student);
                var standing = GetStanding(creditsAverage.Item2);

                // Not enough credits
                if (creditsAverage.Item1 < diploma.Credits)
                    return new Tuple<bool, STANDING>(false, STANDING.Remedial);
                else if (standing == STANDING.Remedial || standing == STANDING.None)
                    return new Tuple<bool, STANDING>(false, standing);
                else
                    return new Tuple<bool, STANDING>(true, standing);
            }
            catch (Exception ex)
            {
                return new Tuple<bool, STANDING>(false, STANDING.None);
            }
        }

        private STANDING GetStanding(int average)
        {
            try
            {
                if (average < 50)
                    return STANDING.Remedial;
                else if (average < 80)
                    return STANDING.Average;
                else if (average < 95)
                    return STANDING.MagnaCumLaude;
                else
                    return STANDING.SumaCumLaude;
            }
            catch (Exception)
            {
                return STANDING.None;
            }
        }

        private Tuple<int, int> GetCreditAverage(Diploma diploma, Student student)
        {
            var credits = 0;
            var average = 0;

            var result = new Tuple<int, int>(0, 0);
            if (diploma == null || student == null)
                return result;

            try
            {
                for (int i = 0; i < diploma.Requirements.Length; i++)
                {
                    for (int j = 0; j < student.Courses.Length; j++)
                    {
                        var requirement = Repository.GetRequirement(diploma.Requirements[i]);

                        for (int k = 0; k < requirement.CourseIDs.Length; k++)
                        {
                            if (requirement.CourseIDs[k] == student.Courses[j].Id)
                            {
                                average += student.Courses[j].Mark;
                                if (student.Courses[j].Mark >= requirement.MinimumMark)
                                {
                                    credits += requirement.Credits;
                                }
                            }
                        }
                    }
                }
                average = average / student.Courses.Length;
                return new Tuple<int, int>(credits, average);
            }
            catch (Exception ex)
            {
                return result;
            }
        }
    }
}
