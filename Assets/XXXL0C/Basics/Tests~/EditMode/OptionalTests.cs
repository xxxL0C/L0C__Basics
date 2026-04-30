using System;
using NUnit.Framework;
using XXXL0C.Basics;

namespace XXXL0C.Basics.Tests
{
    public class OptionalTests
    {
        [Test]
        public void Some_HasValue_True()
        {
            Assert.IsTrue(Optional<int>.Some(5).HasValue);
        }

        [Test]
        public void Some_Value_ReturnsValue()
        {
            Assert.AreEqual(99, Optional<int>.Some(99).Value);
        }

        [Test]
        public void None_HasValue_False()
        {
            Assert.IsFalse(Optional<int>.None().HasValue);
        }

        [Test]
        public void None_Value_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => { var _ = Optional<int>.None().Value; });
        }

        [Test]
        public void GetValueOrDefault_ReturnsValue_WhenSome()
        {
            Assert.AreEqual(7, Optional<int>.Some(7).GetValueOrDefault(0));
        }

        [Test]
        public void GetValueOrDefault_ReturnsFallback_WhenNone()
        {
            Assert.AreEqual(99, Optional<int>.None().GetValueOrDefault(99));
        }
    }
}
