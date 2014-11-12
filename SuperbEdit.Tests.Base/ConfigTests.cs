using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperbEdit.Base;
using System.Collections.Generic;
using System.Linq;

namespace SuperbEdit.Tests.Base
{
    [TestClass]
    public class ConfigTests
    {
        Config config;


        [TestInitialize]
        public void Initialize()
        {
            config = new Config();
        }


        [TestMethod]
        public void StringTest()
        {
            TestSimpleType<string>(config, "test_string", "test_string_value");
        }

        [TestMethod]
        public void DoubleTest()
        {
            TestSimpleType<double>(config, "test_double", 2.0);
        }

        [TestMethod]
        public void IntTest()
        {
            TestSimpleType<Int64>(config, "test_int", 1);
        }


        [TestMethod]
        public void ListOfDoubleTest()
        {
            TestList<double>(config, "test_array_double", new List<double>() {4.0, 8.0, 15.0, 16.0, 23.0, 42.0 });
        }


        [TestMethod]
        public void IntegerWithinObjectTest()
        {
            TestSimpleType<Int64>(config, "test_object.integer", 1);
        }

        [TestMethod]
        public void StringWithinObjectTest()
        {
            TestSimpleType<string>(config, "test_object.string", "test_object_string_value");
        }

        void TestList<T>(Config config, string path, List<T> expected)
        {
            List<object> actual = config.RetrieveConfigValue<List<object>>(path);


            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual<T>(expected[i], (T)actual[i]);
            }
        }

        void TestSimpleType<T>(Config config, string path, T expected)
        {
            T actual = config.RetrieveConfigValue<T>(path);

            Assert.IsNotNull(actual);
            Assert.AreEqual<T>(expected, actual);
        }
    }
}
