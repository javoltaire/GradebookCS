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
    public class AssignmentTest
    {
        [TestMethod]
        public void SetName_EmptyString_ThrowsArgumentNullException()
        {
            //arrange
            Assignment assignment = new Assignment();
            //act => assert
            Assert.ThrowsException<ArgumentNullException>(() => assignment.Name = "");
        }

        [TestMethod]
        public void SetName_WhiteSpaces_ThrowsArgumentNullException()
        {
            //arrange
            Assignment assignment = new Assignment();
            //act => assert
            Assert.ThrowsException<ArgumentNullException>(() => assignment.Name = "    ");
        }

        [TestMethod]
        public void SetName_NullString_ThrowsArgumentNullException()
        {
            //arrange
            Assignment assignment = new Assignment();
            //act => assert
            Assert.ThrowsException<ArgumentNullException>(() => assignment.Name = null);
        }
    }
}
