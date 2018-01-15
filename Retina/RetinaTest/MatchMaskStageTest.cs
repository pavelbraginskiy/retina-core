﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace RetinaTest
{
    [TestClass]
    public class MatchMaskStageTest : RetinaTestBase
    {
        [TestMethod]
        public void TestBasicMatchMask()
        {
            AssertProgram(new TestSuite
            {
                Sources =
                {
                    "_`.",
                    "$=",
                },
                TestCases = { { "123\nabc\n<>", "123123123\nabcabcabc\n<><>" } }
            });
        }

        [TestMethod]
        public void TestCharacterParam()
        {
            AssertProgram(new TestSuite
            {
                Sources =
                {
                    "',_`.",
                    "$=$=",
                },
                TestCases = { { "123,abc,<>", "123,,abc,,<>" } }
            });
        }

        [TestMethod]
        public void TestStringParam()
        {
            AssertProgram(new TestSuite
            {
                Sources =
                {
                    "\", \"_`.",
                    "$=",
                },
                TestCases = { { "123, abc, <>", "123, , abc, , <>" } }
            });
        }

        [TestMethod]
        public void TestRegexParam()
        {
            AssertProgram(new TestSuite
            {
                Sources =
                {
                    @"/\w+/_`.",
                    "$=",
                },
                TestCases = { { "123, abc; XYZ", "123123123, abcabcabc; XYZXYZXYZ" } }
            });
        }

        [TestMethod]
        public void TestMatchLimit()
        {
            AssertProgram(new TestSuite
            {
                Sources =
                {
                    ",2,_`.",
                    "$&$&",
                },
                TestCases = { { "abc\ndef\nghi\njkl\nmno", "aabbcc\ndef\ngghhii\njkl\nmmnnoo" } }
            });
        }

        [TestMethod]
        public void TestExecutionOrder()
        {
            AssertProgram(new TestSuite
            {
                Sources =
                {
                    "._>G`",
                },
                TestCases = { { "123\nabc\n<>", "123abc<>" } }
            });

            AssertProgram(new TestSuite
            {
                Sources =
                {
                    ".^_r>G`",
                },
                TestCases = { { "123\nabc\n<>", "<>abc123" } }
            });
        }

        [TestMethod]
        public void TestRandomMatch()
        {
            AssertRandomProgram(new RandomTestSuite
            {
                Sources = {
                    @"@_`.+",
                    @"$&$&",
                },
                TestCases = { { "abc\ndef\nghi\njkl\nmno", new string[]
                {
                    "abcabc\ndef\nghi\njkl\nmno",
                    "abc\ndefdef\nghi\njkl\nmno",
                    "abc\ndef\nghighi\njkl\nmno",
                    "abc\ndef\nghi\njkljkl\nmno",
                    "abc\ndef\nghi\njkl\nmnomno",
                }
                } }
            });
        }
    }
}
