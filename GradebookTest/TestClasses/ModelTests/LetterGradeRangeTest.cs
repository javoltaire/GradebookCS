using GradebookCS.Model;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradebookTest
{
    [TestClass]
    public class LetterGradeRangeTest
    {
        [TestMethod]
        public void UpdateRangeValues_OutOfRangeValues_ThrowArgsOutOfRangeException()
        {
            //arrange
            LetterGradeRange letterRange = new LetterGradeRange("A", 90, 100);
            //act => assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => letterRange.updateRangeValues(95, 90));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => letterRange.updateRangeValues(-9, 90));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => letterRange.updateRangeValues(95, -90));
        }

        [TestMethod]
        public void IsInRange_ReturnedValue_ReturnTrue()
        {
            //arrange
            LetterGradeRange letterRange = new LetterGradeRange("A", 90, 100);
            //act
            bool isInRage = letterRange.IsInRange(95);
            //assert
            Assert.IsTrue(isInRage);
        }

        [TestMethod]
        public void IsInRange_ReturnedValue_ReturnFalse()
        {
            //arrange
            LetterGradeRange letterRange = new LetterGradeRange("A", 90, 100);
            //act
            bool isInRage = letterRange.IsInRange(40);
            //assert
            Assert.IsFalse(isInRage);
        }
    }
}
