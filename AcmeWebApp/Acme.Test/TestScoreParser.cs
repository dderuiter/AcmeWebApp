using Acme.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Acme.Test
{
    [TestClass]
    public class ScoreParserTests
    {
        [TestMethod]
        public void TestParseScore()
        {
            string scoreString = "8-3/";

            List<int> expected = new List<int>();
            expected.Add(8);
            expected.Add(0);
            expected.Add(3);
            expected.Add(7);

            ScoreParser parser = new ScoreParser();
            List<int> actual = parser.ParseScore(scoreString);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestCalcFramesScoreWithZeros()
        {
            List<int> throwScores = new List<int>
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            ScoreParser parser = new ScoreParser();

            int[] actual = parser.CalcFrameScores(throwScores);
            int[] expected = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestCalcFramesScoreWithOnes()
        {
            List<int> throwScores = new List<int>
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            ScoreParser parser = new ScoreParser();

            int[] actual = parser.CalcFrameScores(throwScores);
            int[] expected = { 2, 4, 6, 8, 10, 12, 14, 16, 18, 20 };

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestCalcFramesScoreWithSpare()
        {
            List<int> throwScores = new List<int>
                { 1, 2, 3, 7, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            ScoreParser parser = new ScoreParser();

            int[] actual = parser.CalcFrameScores(throwScores);
            int[] expected = { 3, 14, 15, 15, 15, 15, 15, 15, 15, 15 };

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestCalcFramesScoreWithStrike()
        {
            List<int> throwScores = new List<int>
                { 8, 2, 5, 4, 9, 0, 10, 10, 5, 5, 5, 3, 6, 3, 9, 1, 9, 1, 10 };
            ScoreParser parser = new ScoreParser();

            int[] actual = parser.CalcFrameScores(throwScores);
            int[] expected = { 15, 24, 33, 58, 78, 93, 101, 110, 129, 149 };

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestCalcFramesScoreWithStrikeInTenthFrame()
        {
            List<int> throwScores = new List<int>
                { 8, 2, 5, 4, 9, 0, 10, 10, 5, 5, 5, 3, 6, 3, 9, 1, 10, 10, 10 };
            ScoreParser parser = new ScoreParser();

            int[] actual = parser.CalcFrameScores(throwScores);
            int[] expected = { 15, 24, 33, 58, 78, 93, 101, 110, 130, 160 };

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
