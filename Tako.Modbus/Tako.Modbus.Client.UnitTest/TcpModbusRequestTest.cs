using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Reflection;
using Tako.Component.Extension;
using Tako.Modbus.Core;

namespace Tako.Modbus.Client.UnitTest
{
    /// <summary>
    ///This is a test class for TcpModbusTransportTest and is intended
    ///to contain all TcpModbusTransportTest unit Tests
    ///</summary>
    [TestClass()]
    public class TcpModbusRequestTest
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

        private const string CLASS_NAME = "Tako.Modbus.Client.TcpModbusRequest";

        /// <summary>
        ///A test for ReadCoilsRequest
        ///</summary>
        [TestMethod()]
        //[ExpectedException(typeof(ModbusException))]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void ReadCoilsRequest_Exception_Test()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte unit = 0;
            ushort startAddress = 0;
            ushort quantity = 0;
            ushort Transaction = 0;
            byte[] expected = null;
            byte[] actual = null;
            target.TransactionId = Transaction;
            actual = target.ReadCoils(unit, startAddress, quantity);
        }

        [TestMethod()]
        public void ReadCoilsTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte unit = 1;
            ushort startAddress = 0;
            ushort quantity = 10;
            ushort Transaction = 0;
            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes("00 00 00 00 00 06 01 01 00 00 00 0A");
            target.TransactionId = Transaction;
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
            ushort Transaction = 0;
            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes("00 00 00 00 00 06 01 02 00 00 00 0A");
            byte[] actual;
            target.TransactionId = Transaction;
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
            ushort Transaction = 0;
            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes("00 00 00 00 00 06 01 03 00 00 00 0A");
            byte[] actual;
            target.TransactionId = Transaction;
            actual = target.ReadHoldingRegisters(unit, startAddress, quantity);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod()]
        public void ReadInputRegistersTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte unit = 1;
            ushort startAddress = 0;
            ushort quantity = 100;
            ushort Transaction = 934;
            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes("03 A6 00 00 00 06 01 04 00 00 00 64");
            byte[] actual;
            target.TransactionId = Transaction;
            actual = target.ReadInputRegisters(unit, startAddress, quantity);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod()]
        public void WriteSingleCoilTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte unit = 1;
            ushort OutputAddress = 0;
            bool OutputValue = true;
            ushort Transaction = 1106;
            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes("04 52 00 00 00 06 01 05 00 00 FF 00");
            byte[] actual;
            target.TransactionId = Transaction;
            actual = target.WriteSingleCoil(unit, OutputAddress, OutputValue);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod()]
        public void WriteSingleRegisterTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte unit = 1;
            ushort OutputAddress = 2;
            short OutputValue = 234;
            ushort Transaction = 18;
            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes("00 12 00 00 00 06 01 06 00 02 00 EA");
            byte[] actual;
            target.TransactionId = Transaction;
            actual = target.WriteSingleRegister(unit, OutputAddress, OutputValue);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod()]
        public void WriteMultipleCoilsTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte unit = 1;
            ushort startAddress = 0;
            ushort quantity = 9;
            byte[] OutputValues = new byte[] { 0x0E, 0x00 };
            ushort Transaction = 895;
            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes("03 7F 00 00 00 09 01 0F 00 00 00 09, 02 0E 00");
            byte[] actual;
            target.TransactionId = Transaction;
            actual = target.WriteMultipleCoils(unit, startAddress, quantity, OutputValues);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod()]
        public void WriteMultipleRegistersTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte unit = 1;
            ushort startAddress = 0;
            ushort quantity = 10;
            short[] OutputValues = new short[] { 78, 87, -78, -87, 23, 35, 123, 12, 33, 6 };
            ushort Transaction = 0x03DA;
            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes("03 DA 00 00 00 1B 01 10 00 00 00 0A 14 00 4E 00 57 FF B2 FF A9 00 17 00 23 00 7B 00 0C 00 21 00 06");
            byte[] actual;
            target.TransactionId = Transaction;
            actual = target.WriteMultipleRegisters(unit, startAddress, quantity, OutputValues);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }
    }
}