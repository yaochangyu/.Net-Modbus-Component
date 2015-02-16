using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Tako.Component.Extension;
using Tako.Modbus.Core;

namespace Tako.Modbus.Client.UnitTest
{
    /// <summary>
    ///This is a test class for HexModbusDataConvertTest and is intended
    ///to contain all HexModbusDataConvertTest unit Tests
    ///</summary>
    [TestClass()]
    public class AsciiModbusDataConvertTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes

        //
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //

        #endregion Additional test attributes

        public const string CLASS_NAME = "Tako.Modbus.Client.AsciiModbusDataConvert";

        /// <summary>
        ///A test for ToDecimal
        ///</summary>
        [TestMethod()]
        public void ToWordDecimalTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);
            byte[] ResultArray = TestUtility.CreateModbusUtility().HexStringToBytes("30 30 34 45 32 36 41 34 46 46 39 45 30 30 37 37 32 45 30 35 30 38 42 42 30 34 36 45 30 30 30 31 30 34 45 44 46 34 35 42");
            IEnumerable<long> expected = new long[] { 78, 9892, -98, 119, 11781, 2235, 1134, 1, 1261, -2981 };
            IEnumerable<long> actual;
            actual = target.ToDecimal(ResultArray, EnumModbusIntegralUnit.Word);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        /// <summary>
        ///A test for WordToHexadecimal
        ///</summary>
        [TestMethod()]
        public void ToWordHexadecimalTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);
            byte[] ResultArray = TestUtility.CreateModbusUtility().HexStringToBytes("30 30 34 45 32 36 41 34 46 46 39 45 30 30 37 37 32 45 30 35 30 38 42 42 30 34 36 45 30 30 30 31 30 34 45 44 46 34 35 42");
            IEnumerable<string> expected = new string[] { "004E", "26A4", "FF9E", "0077", "2E05", "08BB", "046E", "0001", "04ED", "F45B" };
            IEnumerable<string> actual;
            actual = target.ToHexadecimal(ResultArray, EnumModbusIntegralUnit.Word);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        /// <summary>
        ///A test for ToWordOctal
        ///</summary>
        [TestMethod()]
        public void ToWordOctalTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);


            byte[] ResultArray = TestUtility.CreateModbusUtility().HexStringToBytes("30 30 34 45 32 36 41 34 46 46 39 45 30 30 37 37 32 45 30 35 30 38 42 42 30 34 36 45 30 30 30 31 30 34 45 44 46 34 35 42");
            IEnumerable<long> expected = new long[] { 116, 23244, 177636, 167, 27005, 4273, 2156, 1, 2355, 172133 };
            IEnumerable<long> actual;
            actual = target.ToOctal(ResultArray, EnumModbusIntegralUnit.Word);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        /// <summary>
        ///A test for WordToBinary
        ///</summary>
        [TestMethod()]
        public void ToWordBinaryTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte[] ResultArray = TestUtility.CreateModbusUtility().HexStringToBytes("30 30 34 45 32 36 41 34 46 46 39 45 30 30 37 37 32 45 30 35 30 38 42 42 30 34 36 45 30 30 30 31 30 34 45 44 46 34 35 42");
            IEnumerable<string> expected = new string[]
            {
                "1001110".PadLeft(16, '0'),
                "10011010100100".PadLeft(16, '0'),
                "1111111110011110".PadLeft(16, '0'),
                "1110111".PadLeft(16,'0'),
                "10111000000101".PadLeft(16,'0'),
                "100010111011".PadLeft(16,'0'),
                "10001101110".PadLeft(16,'0'),
                "1".PadLeft(16,'0'),
                "10011101101".PadLeft(16,'0'),
                "1111010001011011".PadLeft(16,'0')
            };
            IEnumerable<string> actual;
            actual = target.ToBinary(ResultArray, EnumModbusIntegralUnit.Word);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        /// <summary>
        ///A test for ToByteDecimal
        ///</summary>
        [TestMethod()]
        public void ToByteDecimalTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte[] ResultArray = TestUtility.CreateModbusUtility().HexStringToBytes("45 36 44 46 33 37 ");
            IEnumerable<long> expected = new long[] { 230, 223, 55 };
            IEnumerable<long> actual;
            actual = target.ToDecimal(ResultArray, EnumModbusIntegralUnit.Byte);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        /// <summary>
        ///A test for ToByteOctal
        ///</summary>
        [TestMethod()]
        public void ToByteOctalTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte[] ResultArray = TestUtility.CreateModbusUtility().HexStringToBytes("45 36 44 46 33 37 ");
            IEnumerable<long> expected = new long[] { 346, 337, 67 };
            IEnumerable<long> actual;
            actual = target.ToOctal(ResultArray, EnumModbusIntegralUnit.Byte);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        /// <summary>
        ///A test for ToByteHexadecimal
        ///</summary>
        [TestMethod()]
        public void ToByteHexadecimalTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte[] ResultArray = TestUtility.CreateModbusUtility().HexStringToBytes("45 36 44 46 33 37 ");
            IEnumerable<string> expected = new string[]
            {
              "E6","DF","37"
            };
            IEnumerable<string> actual;
            actual = target.ToHexadecimal(ResultArray, EnumModbusIntegralUnit.Byte);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        /// <summary>
        ///A test for ToByteBinary
        ///</summary>
        [TestMethod()]
        public void ToByteBinaryTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte[] ResultArray = TestUtility.CreateModbusUtility().HexStringToBytes("45 36 44 46 33 37 ");
            IEnumerable<string> expected = new string[]
            {
                "11100110".PadLeft(8, '0'),
                "11011111".PadLeft(8, '0'),
                "110111".PadLeft(8, '0'),
            };
            IEnumerable<string> actual;
            actual = target.ToBinary(ResultArray, EnumModbusIntegralUnit.Byte);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        /// <summary>
        ///A test for ToByteHexadecimal
        ///</summary>
        [TestMethod()]
        public void ToDoubleTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte[] ResultArray = TestUtility.CreateModbusUtility().HexStringToBytes("33 37 41 36 45 39 42 41 46 43 44 36 34 30 30 30 38 44 45 41 42 41 36 45 33 46 33 35 43 30 32 30 30 30 30 30 30 30 30 30 30 30 30 30");
            IEnumerable<double> expected = new double[] { 2.12345678901234d, -8.12345678901234d };
            IEnumerable<double> actual;
            actual = target.ToDouble(ResultArray);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        /// <summary>
        ///A test for ToByteHexadecimal
        ///</summary>
        [TestMethod()]
        public void ToFloatTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte[] ResultArray = TestUtility.CreateModbusUtility().HexStringToBytes("44 36 38 30 33 44 46 43 30 36 31 30 42 46 39 45 38 37 39 34 34 31 34 35 45 39 37 39 43 32 46 36 35 31 45 43 43 34 39 41 30 30 30 30");
            IEnumerable<float> expected = new float[] { 0.123456f, -1.234560f, 12.3456f, -123.456f, -1234.56f };
            IEnumerable<float> actual;
            actual = target.ToFloat(ResultArray);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }
    }
}