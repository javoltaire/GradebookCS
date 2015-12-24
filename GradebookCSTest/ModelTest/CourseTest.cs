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
    public class CourseTest
    {
        [TestMethod]
        public void SetName_EmptyString_ThrowsArgumentNullException()
        {
            //arrange
            Course course = new Course();
            //act => assert
            Assert.ThrowsException<ArgumentNullException>(() => course.Name = "");
        }

        [TestMethod]
        public void SetName_WhiteSpaces_ThrowsArgumentNullException()
        {
            //arrange
            Course course = new Course();
            //act => assert
            Assert.ThrowsException<ArgumentNullException>(() => course.Name = "    ");
        }

        [TestMethod]
        public void SetName_NullString_ThrowsArgumentNullException()
        {
            //arrange
            Course course = new Course();
            //act => assert
            Assert.ThrowsException<ArgumentNullException>(() => course.Name = null);
        }

        [TestMethod]
        public void CollectionChanged_AddingComponents_UpdatesTheTotalGrade()
        {
            //arrange
            Course course = new Course();

            Component component1 = new Component();
            Component component2 = new Component();

            Assignment assignment1 = new Assignment("Assignment 1", 40, 50);
            Assignment assignment2 = new Assignment("Assignment 2", 93, 100);
            Assignment assignment3 = new Assignment("Assignment 3", 70, 100);
            Assignment assignment4 = new Assignment("Assignment 4", 40, 100);
            Assignment assignment5 = new Assignment("Assignment 5", 0, 50);
            Assignment assignment6 = new Assignment("Assignment 6", 100, 100);

            component1.Assignments.Add(assignment1);
            component1.Assignments.Add(assignment2);
            component2.Assignments.Add(assignment3);
            component2.Assignments.Add(assignment4);
            component2.Assignments.Add(assignment5);
            component2.Assignments.Add(assignment6);

            double expectedScore = component1.WeightedGrade.Score + component2.WeightedGrade.Score;
            double expectedMaxScore = component1.WeightedGrade.MaximumScore + component2.WeightedGrade.MaximumScore;
            //act
            course.Components.Add(component1);
            course.Components.Add(component2);
            //assert
            Assert.AreEqual(expectedScore, course.Grade.Score);
            Assert.AreEqual(expectedMaxScore, course.Grade.MaximumScore);
        }

        [TestMethod]
        public void CollectionChanged_RemovingComponents_UpdatesTheTotalGrade()
        {
            //arrange
            Course course = new Course();

            Component component1 = new Component();
            Component component2 = new Component();
            Component component3 = new Component();

            Assignment assignment1 = new Assignment("Assignment 1", 40, 50);
            Assignment assignment2 = new Assignment("Assignment 2", 93, 100);
            Assignment assignment3 = new Assignment("Assignment 3", 70, 100);
            Assignment assignment4 = new Assignment("Assignment 4", 40, 100);
            Assignment assignment5 = new Assignment("Assignment 5", 0, 50);
            Assignment assignment6 = new Assignment("Assignment 6", 100, 100);
            Assignment assignment7 = new Assignment("Assignment 7", 30, 50);
            Assignment assignment8 = new Assignment("Assignment 8", 105, 100);
            Assignment assignment9 = new Assignment("Assignment 9", 99, 100);


            component1.Assignments.Add(assignment1);
            component1.Assignments.Add(assignment2);
            component2.Assignments.Add(assignment3);
            component2.Assignments.Add(assignment4);
            component2.Assignments.Add(assignment5);
            component2.Assignments.Add(assignment6);
            component3.Assignments.Add(assignment7);
            component3.Assignments.Add(assignment8);
            component3.Assignments.Add(assignment9);

            course.Components.Add(component1);
            course.Components.Add(component2);
            course.Components.Add(component3);

            double expectedScore = component2.WeightedGrade.Score + component3.WeightedGrade.Score;
            double expectedMaxScore = component2.WeightedGrade.MaximumScore + component3.WeightedGrade.MaximumScore;
            //act
            course.Components.Remove(component1);
            //assert
            Assert.AreEqual(expectedScore, course.Grade.Score);
            Assert.AreEqual(expectedMaxScore, course.Grade.MaximumScore);
        }
    }
}
