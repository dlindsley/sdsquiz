using System;
using Xunit;

namespace DeveloperSample.Algorithms
{
    public class AlgorithmTest
    {
        #region GetFactorial
        [Fact]
        public void CanGetFactorial()
        {
            Assert.Equal(24, Algorithms.GetFactorial(4));
        }
        [Fact]
        public void CanGetFactorialOfZero()
        {
            Assert.Equal(1, Algorithms.GetFactorial(0));
        }
        [Fact]
        public void CanGetFactorialOfOne()
        {
            Assert.Equal(1, Algorithms.GetFactorial(1));
        }
        [Fact]
        public void CanNotGetFactorialOfNegative()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Algorithms.GetFactorial(-1));
        }
        #endregion

        #region FormatSeparators
        [Fact]
        public void CanFormatSeparators()
        {
            Assert.Equal("a, b and c", Algorithms.FormatSeparators("a", "b", "c"));
        }
        [Fact]
        public void CanFormatSeparatorsWithNullParam()
        {
            Assert.Equal("", Algorithms.FormatSeparators(null));
        }
        [Fact]
        public void CanFormatSeparatorsWithZeroParams()
        {
            Assert.Equal("", Algorithms.FormatSeparators(new string[] { }));
        }
        [Fact]
        public void CanFormatSeparatorsWithOneParam()
        {
            Assert.Equal("foo", Algorithms.FormatSeparators(new string[] { "foo" }));
        }
        #endregion
    }
}