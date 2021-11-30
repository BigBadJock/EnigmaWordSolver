using EnigmaWordSolver.Contracts;
using EnigmaWordSolver.DTO;
using EnigmaWordSolver.miscellaneous;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnigmaWordSolver.Tests
{
    [TestClass]
    public class EnigmaWordSolver_Tests
    {

        EnigmaWord solver;

        [TestInitialize]
        public void Setup()
        {
            List<string> wordList = new List<string>
            {
                "act",
                "cat",
                //"cut",
                "dog",
                "tuc",

            };

            var mockWordListLoader = new Mock<IWordListLoader>();
            mockWordListLoader.Setup(x => x.LoadWords()).Returns(wordList);

            this.solver = new EnigmaWord(mockWordListLoader.Object);
        }

        [TestCleanup]
        public void TearDown()
        {
            solver = null;
        }


        [TestMethod]
        public async Task SolverReturnsFalseIfNotSolved()
        {
            solver.WordsToSolve.Add(new WordToSolve("1,2,3,4"));
            bool result = await solver.Solve();
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(NoWordsException))]
        public async Task SolverThrowsExceptionIfNoWords()
        {
            bool result = await solver.Solve();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddEmptyCodeWord_ThrowsException()
        {
            solver.AddWord(String.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(NotCommaSeparatedException))]
        public void AddNonCommaCodeWord_ThrowsException()
        {
            solver.AddWord("1234");
        }

        [TestMethod]
        [ExpectedException(typeof(WordCannotBeginWithCommaException))]
        public void CodeWordCannotBeginWithComma_ThrowsException()
        {
            solver.AddWord(",1234");
        }

        [TestMethod]
        [ExpectedException(typeof(WordCannotHaveDoubleCommasException))]
        public void CodeWordCannotHaveRepeatingCommas_ThrowsException()
        {
            solver.AddWord("1,,234");
        }

        [DataRow("1,27")]
        [DataRow("1,0")]
        [DataTestMethod]
        [ExpectedException(typeof(WordCannotInvalidNumberException))]
        public void CodeWordCannotHaveInvalidNumber_ThrowsException(string codeWord)
        {
            solver.AddWord(codeWord);
        }

        [DataRow(0, "a")]
        [DataRow(0, "A")]
        [DataRow(27, "B")]
        [DataRow(27, "b")]
        [DataRow(1, "1")]
        [DataRow(0, "-")]
        [DataTestMethod]
        [ExpectedException(typeof(GivenValueInvalidException))]
        public void GivenValuesMustBeValid(int number, string value)
        {
            solver.AddGivenValue(number, value);
        }

        [DataRow(1, "A", 1, "a")]
        [DataRow(1, "a", 1, "a")]
        [DataRow(1, "a", 2, "a")]
        [DataRow(1, "a", 1, "b")]
        [DataTestMethod]
        [ExpectedException(typeof(GivenValueInvalidException))]
        public void GivenValuesCannotBeDuplicates(int number1, string value1, int number2, string value2)
        {
            solver.AddGivenValue(number1, value1);
            solver.AddGivenValue(number2, value2);
        }


        [DataRow(1, "A")]
        [DataRow(2, "B")]
        [DataTestMethod]
        public void GivenValueAddSuccess(int number, string value)
        {
            solver.AddGivenValue(number, value);

            Assert.AreEqual(1, solver.GivenValues.Count);
        }


        [TestMethod]
        public async Task Solver_Simple1_Success1()
        {
            solver.AddGivenValue(1, "C");
            solver.AddGivenValue(2, "A");
            solver.AddGivenValue(3, "T");

            solver.AddWord("1,2,3");

            bool result = await solver.Solve();

            Assert.IsTrue(result);
        }


        [TestMethod]
        public async Task Solver_Simple2_Success1()
        {
            solver.AddGivenValue(1, "C");
            solver.AddGivenValue(2, "A");
            solver.AddGivenValue(3, "T");
            solver.AddGivenValue(4, "D");
            solver.AddGivenValue(5, "O");
            solver.AddGivenValue(6, "G");

            solver.AddWord("1,2,3");
            solver.AddWord("4,5,6");

            bool result = await solver.Solve();

            Assert.IsTrue(result);
        }


        [TestMethod]
        public async Task Solver_Missing1_Success1()
        {
            solver.AddGivenValue(1, "C");
            solver.AddGivenValue(3, "T");

            solver.AddWord("1,2,3");

            bool result = await solver.Solve();

            Assert.IsTrue(result);
        }


        [TestMethod]
        public async Task Solver_Missing2_Success1()
        {
            solver.AddGivenValue(1, "C");
            solver.AddGivenValue(3, "T");

            solver.AddWord("1,2,3");
            solver.AddWord("2,1,3");

            bool result = await solver.Solve();

            Assert.IsTrue(result);
        }


        [TestMethod]
        public async Task Solver_Missing3_Success1()
        {
            solver.AddGivenValue(1, "C");
            solver.AddGivenValue(3, "T");

            solver.AddWord("1,2,3");
            solver.AddWord("3,4,1");

            bool result = await solver.Solve();

            Assert.IsTrue(result);
        }


    }
}