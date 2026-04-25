using System;
using NUnit.Framework;
using XXXL0C.ExBasics;

namespace XXXL0C.ExBasics.Tests
{
    public class SerializableTypeTests
    {
        [Test]
        public void Constructor_SetsType()
        {
            var st = new SerializableType(typeof(int));
            Assert.AreEqual(typeof(int), st.Type);
        }

        [Test]
        public void RoundTrip_PreservesType()
        {
            var st = new SerializableType(typeof(UnityEngine.Vector3));
            st.OnBeforeSerialize();
            st.OnAfterDeserialize();
            Assert.AreEqual(typeof(UnityEngine.Vector3), st.Type);
        }

        [Test]
        public void NullType_SurvivesRoundTrip()
        {
            var st = new SerializableType(null);
            st.OnBeforeSerialize();
            st.OnAfterDeserialize();
            Assert.IsNull(st.Type);
        }

        [Test]
        public void PropertySetter_UpdatesAssemblyQualifiedName()
        {
            var st = new SerializableType();
            st.Type = typeof(string);
            Assert.AreEqual(typeof(string), st.Type);
        }
    }
}
