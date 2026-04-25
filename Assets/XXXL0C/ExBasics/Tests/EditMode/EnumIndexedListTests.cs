using NUnit.Framework;
using XXXL0C.ExBasics;

namespace XXXL0C.ExBasics.Tests
{
    public class EnumIndexedListTests
    {
        private enum Fruit { Apple, Banana, Cherry }

        [Test]
        public void Indexer_ReadsAndWrites()
        {
            var list = new EnumIndexedList<Fruit, string>();
            list[Fruit.Banana] = "yellow";
            Assert.AreEqual("yellow", list[Fruit.Banana]);
        }

        [Test]
        public void AllSlots_DefaultToDefault()
        {
            var list = new EnumIndexedList<Fruit, string>();
            Assert.IsNull(list[Fruit.Apple]);
            Assert.IsNull(list[Fruit.Cherry]);
        }

        [Test]
        public void Count_MatchesEnumLength()
        {
            var list = new EnumIndexedList<Fruit, int>();
            Assert.AreEqual(3, list.Count);
        }

        [Test]
        public void MultipleSlots_Independent()
        {
            var list = new EnumIndexedList<Fruit, int>();
            list[Fruit.Apple]  = 1;
            list[Fruit.Banana] = 2;
            list[Fruit.Cherry] = 3;
            Assert.AreEqual(1, list[Fruit.Apple]);
            Assert.AreEqual(2, list[Fruit.Banana]);
            Assert.AreEqual(3, list[Fruit.Cherry]);
        }
    }
}
