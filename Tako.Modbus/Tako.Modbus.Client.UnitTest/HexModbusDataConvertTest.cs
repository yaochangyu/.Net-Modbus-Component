using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class HexModbusDataConvertTest
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

        private const string CLASS_NAME = "Tako.Modbus.Client.HexModbusDataConvert";

        /// <summary>
        ///A test for ToDecimal
        ///</summary>
        [TestMethod()]
        public void ToWordDecimalTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte[] ResultArray = TestUtility.CreateModbusUtility().HexStringToBytes("00 4E 26 A4 FF 9E 00 77 2E 05 08 BB 04 6E 00 01 04 ED F4 5B");
            IEnumerable<long> expected = new long[] { 78, 9892, -98, 119, 11781, 2235, 1134, 1, 1261, -2981 };
            IEnumerable<long> actual;
            actual = target.ToDecimal(ResultArray, EnumModbusIntegralUnit.Word);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        /// <summary>
        ///A test for ToWordOctal
        ///</summary>
        [TestMethod()]
        public void ToWordOctalTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte[] ResultArray = TestUtility.CreateModbusUtility().HexStringToBytes("00 4E ,26 A4 ,FF 9E ,00 77 ,2E 05 ,08 BB, 04 6E ,00 01, 04 ED, F4 5B");
            IEnumerable<long> expected = new long[] { 116, 23244, 177636, 167, 27005, 4273, 2156, 1, 2355, 172133 };
            IEnumerable<long> actual;

            actual = target.ToOctal(ResultArray, EnumModbusIntegralUnit.Word);

            //actual = target.ToWordOctal(ResultArray);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        /// <summary>
        ///A test for WordToHexadecimal
        ///</summary>
        [TestMethod()]
        public void ToWordHexadecimalTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte[] ResultArray = TestUtility.CreateModbusUtility().HexStringToBytes("00 4E 26 A4 FF 9E 00 77 2E 05 08 BB 04 6E 00 01 04 ED F4 5B");
            IEnumerable<string> expected = new string[] { "004E", "26A4", "FF9E", "0077", "2E05", "08BB", "046E", "0001", "04ED", "F45B" };
            IEnumerable<string> actual;
            actual = target.ToHexadecimal(ResultArray, EnumModbusIntegralUnit.Word);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        /// <summary>
        ///A test for WordToBinary
        ///</summary>
        [TestMethod()]
        public void ToWordBinaryTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte[] ResultArray = TestUtility.CreateModbusUtility().HexStringToBytes("00 4E, 26 A4, FF 9E, 00 77 2E 05 08 BB 04 6E 00 01 04 ED F4 5B");
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

            byte[] ResultArray = TestUtility.CreateModbusUtility().HexStringToBytes("E6 DF 37");
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

            byte[] ResultArray = TestUtility.CreateModbusUtility().HexStringToBytes("E6 DF 37");
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

            byte[] ResultArray = TestUtility.CreateModbusUtility().HexStringToBytes("E6 DF 37");

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

            byte[] ResultArray = TestUtility.CreateModbusUtility().HexStringToBytes("E6 DF 37");
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
        ///A test for ToByteDecimal
        ///</summary>
        [TestMethod()]
        public void ToFloatTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte[] ResultArray = TestUtility.CreateModbusUtility().HexStringToBytes("C2 8F 3D F5 E6 B4 40 07 38 52 C2 B6 BD 7B C0 21 00 00 00 00 00 00");
            IEnumerable<float> expected = new float[] { 0.120000f, 2.123456f, -91.110001f, -2.527190f, 0.000000f };
            IEnumerable<float> actual;
            actual = target.ToFloat(ResultArray);

            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        /// <summary>
        ///A test for ToByteDecimal
        ///</summary>
        [TestMethod()]
        public void ToDoubleTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            //byte[] ResultArray = TestUtility.CreateModbusUtility().HexStringToBytes("37 A6 E9 BA FC D6 40 00 8D EA BA 6E 3F 35 C0 20 00 00 00 00 00 00");
            //IEnumerable<double> expected = new double[] { 2.12345678901234d, -8.12345678901234d };

            //D7 0A 70 A3 0A 3D 40 18
            byte[] ResultArray = TestUtility.CreateModbusUtility().HexStringToBytes("D7 0A 70 A3 0A 3D 40 18");
            IEnumerable<double> expected = new double[] { 6.01d };

            IEnumerable<double> actual;
            actual = target.ToDouble(ResultArray);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        //[TestMethod()]
        //public void FloatToHexStringTest()
        //{
        //    var expected = "3F8CCCCD";

        //    var source = 11.98;
        //    byte[] sourceArray = BitConverter.GetBytes(source);
        //    var number = BitConverter.ToInt64(sourceArray, 0);
        //    string actual = number.ToString("X");
        //    Assert.AreEqual(expected, actual);
        //}

        //[TestMethod()]
        //public void HexStringToFloatTest()
        //{
        //    var expected = -8.87d;

        //    string source = "3F8CCCCD";
        //    var number = long.Parse(source, System.Globalization.NumberStyles.AllowHexSpecifier);
        //    byte[] numberArray = BitConverter.GetBytes(number);
        //    float actual = BitConverter.ToSingle(numberArray, 0);
        //    Assert.AreEqual(expected, actual);
        //}
    }
}