using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Reflection;
using Tako.Component.Extension;
using Tako.Modbus.Core;

namespace Tako.Modbus.Client.UnitTest
{
    /// <summary>
    ///This is a test class for RtuModbusRequestTest and is intended
    ///to contain all RtuModbusRequestTest unit Tests
    ///</summary>
    [TestClass()]
    public class RtuModbusRequestTest
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

        private const string CLASS_NAME = "Tako.Modbus.Client.RtuModbusRequest";

        [TestMethod()]
        public void ReadCoilsTest()
        {
            dynamic target = TestUtility.GenerateInstance(CLASS_NAME);
            byte unit = 1;
            ushort startAddress = 0;
            ushort quantity = 10;
            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes("01 01 00 00 00 0A BC 0D");
            byte[] actual;
            actual = target.ReadCoils(unit, startAddress, quantity);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod()]
        public void ReadDiscreteInputsTest()
        {
            dynamic target = TestUtility.GenerateInstance(CLASS_NAME);

            byte unit = 1;

            ushort startAddress = 0;
            ushort quantity = 10;
            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes("01 02 00 00 00 0A F8 0D");
            byte[] actual;
            actual = target.ReadDiscreteInputs(unit, startAddress, quantity);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod()]
        public void ReadHoldingRegistersTest()
        {
            dynamic target = TestUtility.GenerateInstance(CLASS_NAME);

            byte unit = 1;
            ushort startAddress = 0;
            ushort quantity = 10;
            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes("01 03 00 00 00 0A C5 CD");
            byte[] actual;
            actual = target.ReadHoldingRegisters(unit, startAddress, quantity);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod()]
        public void ReadInputRegistersTest()
        {
            dynamic target = TestUtility.GenerateInstance(CLASS_NAME);
            byte unit = 1;

            ushort startAddress = 0;
            ushort quantity = 10;

            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes("01 04 00 00 00 0A 70 0D");
            byte[] actual;
            actual = target.ReadInputRegisters(unit, startAddress, quantity);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod()]
        public void WriteSingleCoilTest()
        {
            dynamic target = TestUtility.GenerateInstance(CLASS_NAME);

            byte unit = 1;
            ushort startAddress = 2;
            bool OutputValue = true;

            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes("01 05 00 02 FF 00 2D FA");
            byte[] actual;
            actual = target.WriteSingleCoil(unit, startAddress, OutputValue);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod()]
        public void WriteSingleRegisterTest()
        {
            dynamic target = TestUtility.GenerateInstance(CLASS_NAME);

            byte unit = 1;
            ushort OutputAddress = 2;
            short OutputValue = 2344;
            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes("01 06 00 02 09 28 2E 44");

            byte[] actual;
            actual = target.WriteSingleRegister(unit, OutputAddress, OutputValue);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod()]
        public void WriteMultipleCoilsTest()
        {
            dynamic target = TestUtility.GenerateInstance(CLASS_NAME);

            byte unit = 1;
            ushort startAddress = 0;
            ushort quantity = 24;
            byte[] OutputValues = new byte[] { 147, 82, 5 };
            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes("01 0F 00 00 00 18 03 93 52 05 4D 3A");

            byte[] actual;
            actual = target.WriteMultipleCoils(unit, startAddress, quantity, OutputValues);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod()]
        public void WriteMultipleRegistersTest()
        {
            dynamic target = TestUtility.GenerateInstance(CLASS_NAME);

            byte unit = 1;
            ushort startAddress = 0;
            ushort quantity = 10;
            short[] OutputValues = new short[] { 1234, 0, 0, -13, 12, -86, 223, 0, 0, -8 };
            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes("01 10 00 00 00 0A 14 04 D2 00 00 00 00 FF F3 00 0C FF AA 00 DF 00 00 00 00 FF F8 58 66");
            byte[] actual;
            actual = target.WriteMultipleRegisters(unit, startAddress, quantity, OutputValues);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }
    }
}