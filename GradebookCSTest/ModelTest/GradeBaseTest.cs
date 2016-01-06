using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradebookCS.Model;

namespace GradebookCSTest.ModelTest
{
    [TestClass]
    public class GradeBaseTest
    {
        [TestMethod]
        public void PercentProperty_PerformsTheRightCalculations_ReturnsTheCalculatedPercentage()
        {
            //arrange
            GradeBase grade = new ComputedGrade(45,50);
            double percent = 100 * 45 / 50;
            //act
            double gradePercent = grade.Percent;
            //assert
            Assert.AreEqual(percent, gradePercent);
        }

        //[TestMethod]
        //public void LetterProperty_GetTheLetterForTheScore_ReturnsTheLetterAssociatedWithTheScore()
        //{
        //    //arrange
        //    ComputedGrade grade = new ComputedGrade(45, 50);
        //    LetterGradeRange letterRange = new LetterGradeRange("A", grade.ARangeLowEnd, grade.ARangeHighEnd);
        //    string expected = letterRange.Letter;
        //    //act
        //    string actual = grade.Letter;
        //    //assert
        //    Assert.AreEqual(expected, actual);
        //}

        [TestMethod]
        public void Add_AddingTwoGrades_ValuesAreUpdated()
        {
            //arrange
            ComputedGrade grade = new ComputedGrade(45, 50);
            ComputedGrade gradeToAdd = new ComputedGrade(9, 10);
            double scoreTotal = grade.Score + gradeToAdd.Score;
            double maximumScoreTotal = grade.MaximumScore + gradeToAdd.MaximumScore;
            //act
            //grade.Add(gradeToAdd);
            //assert
            Assert.AreEqual(grade.Score, scoreTotal);
            Assert.AreEqual(grade.MaximumScore, maximumScoreTotal);
        }
        

    }
}
