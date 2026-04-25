using NUnit.Framework;
using XXXL0C.ExBasics;

namespace XXXL0C.ExBasics.Tests
{
    public class SerializableDictionaryTests
    {
        [Test]
        public void AddAndRetrieve()
        {
            var dict = new SerializableDictionary<string, int>();
            dict["key"] = 42;
            Assert.AreEqual(42, dict["key"]);
        }

        [Test]
        public void RoundTrip_PreservesEntries()
        {
            var dict = new SerializableDictionary<string, int>();
            dict["a"] = 1;
            dict["b"] = 2;
            dict.OnBeforeSerialize();
            dict.Clear();
            dict.OnAfterDeserialize();
            Assert.AreEqual(1, dict["a"]);
            Assert.AreEqual(2, dict["b"]);
        }

        [Test]
        public void DuplicateKeys_DoNotThrow()
        {
            var dict = new SerializableDictionary<string, int>();
            dict["dup"] = 1;
            dict.OnBeforeSerialize();
            Assert.DoesNotThrow(() => dict.OnAfterDeserialize());
        }

        [Test]
        public void Empty_RoundTrip()
        {
            var dict = new SerializableDictionary<string, int>();
            dict.OnBeforeSerialize();
            dict.OnAfterDeserialize();
            Assert.AreEqual(0, dict.Count);
        }
    }
}
