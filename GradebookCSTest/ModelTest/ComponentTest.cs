using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradebookCS.Model;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace GradebookCSTest.ModelTest
{
    [TestClass]
    public class ComponentTest
    {
        [TestMethod]
        public void SetName_EmptyString_ThrowsArgumentNullException()
        {
            //arrange
            Component component = new Component();
            //act => assert
            Assert.ThrowsException<ArgumentNullException>(() => component.Name = "");
        }

        [TestMethod]
        public void SetName_WhiteSpaces_ThrowsArgumentNullException()
        {
            //arrange
            Component component = new Component();
            //act => assert
            Assert.ThrowsException<ArgumentNullException>(() => component.Name = "    ");
        }

        [TestMethod]
        public void SetName_NullString_ThrowsArgumentNullException()
        {
            //arrange
            Component component = new Component();
            //act => assert
            Assert.ThrowsException<ArgumentNullException>(() => component.Name = null);
        }

        [TestMethod]
        public void SetWeight_NegativeValue_ThrowsArgumentOutOfRangeException()
        {
            //arrange
            Component component = new Component();
            //act => assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => component.Weight = -15);
        }

        [TestMethod]
        public void GetWeightedGrade_ReturnsTheRightValues()
        {
            //arrange
            //Component component = new Component();
            //Assignment assignment1 = new Assignment("Assignment 1", 40, 50);
            //Assignment assignment2 = new Assignment("Assignment 2", 93, 100);
            //Assignment assignment3 = new Assignment("Assignment 3", 9, 10);
            //const double weightValue = 50;
            //component.Weight = weightValue;
            //component.Assignments.Add(assignment1);
            //component.Assignments.Add(assignment2);
            //component.Assignments.Add(assignment3);
            //ComputedGrade expectedGrade = component.TotalGrade.Scale(weightValue);
            ////act
            //ComputedGrade actualGrade = component.WeightedGrade;
            ////Assert
            //Assert.AreEqual(expectedGrade.Score, actualGrade.Score);
            //Assert.AreEqual(expectedGrade.MaximumScore, actualGrade.MaximumScore);

        }

        [TestMethod]
        public void CollectionChanged_AddingAssignments_UpdatesTheTotalGrade()
        {
            //arrange
            Component component = new Component();
            Assignment assignment1 = new Assignment("Assignment 1", 40, 50);
            Assignment assignment2 = new Assignment("Assignment 2", 93, 100);
            double expectedTotalScore = assignment1.Grade.Score + assignment2.Grade.Score;
            double expectedTotalMaxScore = assignment1.Grade.MaximumScore + assignment2.Grade.MaximumScore;
            //act
            component.Assignments.Add(assignment1);
            component.Assignments.Add(assignment2);
            //assert
            Assert.AreEqual(expectedTotalScore, component.TotalGrade.Score);
            Assert.AreEqual(expectedTotalMaxScore, component.TotalGrade.MaximumScore);
        }

        [TestMethod]
        public void CollectionChanged_RemovingAssignments_UpdatesTheTotalGrade()
        {
            //arrange
            Component component = new Component();
            Assignment assignment1 = new Assignment("Assignment 1", 40, 50);
            Assignment assignment2 = new Assignment("Assignment 2", 93, 100);
            Assignment assignment3 = new Assignment("Assignment 3", 9, 10);
            component.Assignments.Add(assignment1);
            component.Assignments.Add(assignment2);
            component.Assignments.Add(assignment3);
            double expectedTotalScore = assignment1.Grade.Score + assignment3.Grade.Score;
            double expectedTotalMaxScore = assignment1.Grade.MaximumScore + assignment3.Grade.MaximumScore;
            //act
            component.Assignments.Remove(assignment2);
            //assert
            Assert.AreEqual(expectedTotalScore, component.TotalGrade.Score);
            Assert.AreEqual(expectedTotalMaxScore, component.TotalGrade.MaximumScore);
        }
    }
}
