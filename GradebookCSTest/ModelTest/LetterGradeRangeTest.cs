﻿using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradebookCS.Model;

namespace GradebookCSTest.ModelTest
{
    [TestClass]
    public class LetterGradeRangeTest
    {
        [TestMethod]
        public void SetLetter_MoreThanTwoCharacter_ThrowsException()
        {
            //arrange
            LetterGradeRange range = new LetterGradeRange();
            //act => assert
            Assert.ThrowsException<ArgumentException>(() => range.Letter = "This has more Characters");
        }

        [TestMethod]
        public void SetLetter_EmptyString_ThrowsArgumentNullException()
        {
            //arrange
            LetterGradeRange range = new LetterGradeRange();
            //act => assert
            Assert.ThrowsException<ArgumentNullException>(() => range.Letter = "");
        }

        [TestMethod]
        public void SetLetter_WhiteSpaces_ThrowsArgumentNullException()
        {
            //arrange
            LetterGradeRange range = new LetterGradeRange();
            //act => assert
            Assert.ThrowsException<ArgumentNullException>(() => range.Letter = "    ");
        }

        [TestMethod]
        public void SetLetter_NullString_ThrowsArgumentNullException()
        {
            //arrange
            LetterGradeRange range = new LetterGradeRange();
            //act => assert
            Assert.ThrowsException<ArgumentNullException>(() => range.Letter = null);
        }

        [TestMethod]
        public void IsInRange_CheckIfNumberIsInRange_ReturnsTrue()
        {
            //arrange
            LetterGradeRange range = new LetterGradeRange("A", 90, 100);
            //act 
            bool result = range.IsInRange(95);
            //assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsInRange_CheckIfNumberIsInRange_ReturnsFalse()
        {
            //arrange
            LetterGradeRange range = new LetterGradeRange("A", 90, 100);
            //act 
            bool result = range.IsInRange(40);
            //assert
            Assert.IsFalse(result);
        }
    }
}
