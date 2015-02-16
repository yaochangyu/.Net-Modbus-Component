using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Reflection;
using Tako.Component.Extension;
using Tako.Modbus.Core;

namespace Tako.Modbus.Client.UnitTest
{
    /// <summary>
    ///This is a test class for AsciiModbusRequestTest and is intended
    ///to contain all AsciiModbusRequestTest unit Tests
    ///</summary>
    [TestClass()]
    public class AsciiModbusRequestTest
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

        private const string CLASS_NAME = "Tako.Modbus.Client.AsciiModbusRequest";

        [TestMethod()]
        public void ReadCoilsTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte unit = 1;
            ushort startAddress = 0;
            ushort quantity = 10;
            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes("3A 30 31 30 31 30 30 30 30 30 30 30 41 46 34 0D 0A");
            byte[] actual;
            actual = target.ReadCoils(unit, startAddress, quantity);

            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod()]
        public void ReadDiscreteInputsTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte unit = 1;
            ushort startAddress = 0;
            ushort quantity = 10;
            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes("3A 30 31 30 32 30 30 30 30 30 30 30 41 46 33 0D 0A");
            byte[] actual;
            actual = target.ReadDiscreteInputs(unit, startAddress, quantity);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod()]
        public void ReadHoldingRegistersTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte unit = 1;
            ushort startAddress = 0;
            ushort quantity = 10;
            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes("3A 30 31 30 33 30 30 30 30 30 30 30 41 46 32 0D 0A");
            byte[] actual;
            actual = target.ReadHoldingRegisters(unit, startAddress, quantity);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod()]
        public void ReadInputRegistersTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte unit = 1;
            ushort startAddress = 0;
            ushort quantity = 10;
            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes("3A 30 31 30 34 30 30 30 30 30 30 30 41 46 31 0D 0A");
            byte[] actual;
            actual = target.ReadInputRegisters(unit, startAddress, quantity);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod()]
        public void WriteSingleCoilTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte unit = 1;
            ushort OutputAddress = 1;
            bool OutputValue = true;
            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes("3A 30 31 30 35 30 30 30 31 46 46 30 30 46 41 0D 0A");
            byte[] actual;
            actual = target.WriteSingleCoil(unit, OutputAddress, OutputValue);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod()]
        public void WriteSingleRegisterTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte unit = 1;
            ushort OutputAddress = 2;
            short OutputValue = 123;
            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes("3A 30 31 30 36 30 30 30 32 30 30 37 42 37 43 0D 0A");
            byte[] actual;
            actual = target.WriteSingleRegister(unit, OutputAddress, OutputValue);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod()]
        public void WriteMultipleCoilsTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            ModbusUtility ModbusUtility = new ModbusUtility();
            byte unit = 1;
            ushort startAddress = 0;
            ushort quantity = 3;
            byte[] OutputValues = new byte[] { 3 };

            byte[] expected = ModbusUtility.HexStringToBytes("3A 30 31 30 46 30 30 30 30 30 30 30 33 30 31 30 33 45 39 0D 0A");

            byte[] actual;
            actual = target.WriteMultipleCoils(unit, startAddress, quantity, OutputValues);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod()]
        public void WriteMultipleRegistersTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte unit = 1;
            ushort startAddress = 0;
            ushort quantity = 24;
            short[] OutputValues = new short[] { 12, 0, 0, 987, -98, 1, 456, 0, 123, -8, 0, 0, 0, 234, 112, 0, 3458, -89, 0, 0, 0, 988, 0, 0 };
            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes("3A 30 31 31 30 30 30 30 30 30 30 31 38 33 30 30 30 30 43 30 30 30 30 30 30 30 30 30 33 44 42 46 46 39 45 30 30 30 31 30 31 43 38 30 30 30 30 30 30 37 42 46 46 46 38 30 30 30 30 30 30 30 30 30 30 30 30 30 30 45 41 30 30 37 30 30 30 30 30 30 44 38 32 46 46 41 37 30 30 30 30 30 30 30 30 30 30 30 30 30 33 44 43 30 30 30 30 30 30 30 30 37 36 0D 0A");
            byte[] actual;
            actual = target.WriteMultipleRegisters(unit, startAddress, quantity, OutputValues);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }
    }
}