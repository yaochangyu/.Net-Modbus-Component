using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Net.Sockets;

namespace Tako.Modbus.Client.UnitTest
{
    [TestClass]
    public class RtuModbusClientUnitTest
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

        private static Mock<IModbusSocket> s_stubModbusSocket = null;
        private static RtuModbusClient s_client = null;

        public RtuModbusClientUnitTest()
        {
            if (s_stubModbusSocket == null)
            {
                s_stubModbusSocket = new Mock<IModbusSocket>();
                s_stubModbusSocket.Setup(
                 o => o.Connect<SerialModbusConnectConifg>(new SerialModbusConnectConifg()
                 {
                 })).Returns(true);
            }
            if (s_client == null)
            {
                s_client = new RtuModbusClient();
                s_client.ModbusSocket = s_stubModbusSocket.Object;
            }
        }

        private RtuModbusClient CreateTcpModbusClient(byte[] receiveArray)
        {
            s_stubModbusSocket.Setup(o => o.Receive()).Returns(receiveArray);
            return s_client;
        }

        [TestMethod()]
        [DeploymentItem("TestDoc\\RtuReadFunc1Test.csv")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestDoc\\RtuReadFunc1Test.csv", "RtuReadFunc1Test#csv", DataAccessMethod.Random)]
        public void ReadCoilsTest()
        {
            byte[] requestArray = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[0].ToString());
            byte[] receiveArray = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[1].ToString());
            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[2].ToString());
            var client = CreateTcpModbusClient(receiveArray);

            byte unit = 1;
            ushort startAddress = 0;
            ushort quantity = 100;

            byte[] actual;
            actual = client.ReadCoils(unit, startAddress, quantity);
            Assert.IsTrue(client.RequestArray.SequenceEqual(requestArray));
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod()]
        [DeploymentItem("TestDoc\\RtuReadFunc2Test.csv")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestDoc\\RtuReadFunc2Test.csv", "RtuReadFunc2Test#csv", DataAccessMethod.Sequential)]
        public void ReadDiscreteInputsTest()
        {
            byte[] requestArray = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[0].ToString());
            byte[] receiveArray = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[1].ToString());
            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[2].ToString());
            var client = CreateTcpModbusClient(receiveArray);

            byte unit = 1;
            ushort startAddress = 0;
            ushort quantity = 100;
            var actual = client.ReadDiscreteInputs(unit, startAddress, quantity);
            Assert.IsTrue(client.RequestArray.SequenceEqual(requestArray));
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod()]
        [DeploymentItem("TestDoc\\RtuReadFunc3Test.csv")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestDoc\\RtuReadFunc3Test.csv", "RtuReadFunc3Test#csv", DataAccessMethod.Sequential)]
        public void ReadHoldingRegistersTest()
        {
            byte[] requestArray = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[0].ToString());
            byte[] receiveArray = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[1].ToString());
            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[2].ToString());
            var client = CreateTcpModbusClient(receiveArray);

            byte unit = 1;
            ushort startAddress = 0;
            ushort quantity = 100;
            var actual = client.ReadHoldingRegisters(unit, startAddress, quantity);
            Assert.IsTrue(client.RequestArray.SequenceEqual(requestArray));
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod()]
        [DeploymentItem("TestDoc\\RtuReadFunc4Test.csv")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestDoc\\RtuReadFunc4Test.csv", "RtuReadFunc4Test#csv", DataAccessMethod.Sequential)]
        public void ReadInputRegistersTest()
        {
            byte[] requestArray = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[0].ToString());
            byte[] receiveArray = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[1].ToString());
            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[2].ToString());
            var client = CreateTcpModbusClient(receiveArray);

            byte unit = 1;
            ushort startAddress = 0;
            ushort quantity = 100;
            var actual = client.ReadInputRegisters(unit, startAddress, quantity);
            Assert.IsTrue(client.RequestArray.SequenceEqual(requestArray));
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod()]
        public void WriteSingleCoilTest()
        {
            byte[] requestArray = TestUtility.CreateModbusUtility().HexStringToBytes("01 05 00 02 FF 00 2D FA");
            byte[] receiveArray = TestUtility.CreateModbusUtility().HexStringToBytes("01 05 00 02 FF 00 2D FA");
            var client = CreateTcpModbusClient(receiveArray);
            var expected = true;
            byte unit = 1;
            ushort startAddress = 2;
            bool OutputValue = true;
            var actual = client.WriteSingleCoil(unit, startAddress, OutputValue);
            Assert.IsTrue(client.RequestArray.SequenceEqual(requestArray));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void WriteSingleRegisterTest()
        {
            byte[] requestArray = TestUtility.CreateModbusUtility().HexStringToBytes("01 06 00 02 09 28 2E 44");
            byte[] receiveArray = TestUtility.CreateModbusUtility().HexStringToBytes("01 06 00 02 09 28 2E 44");
            var client = CreateTcpModbusClient(receiveArray);
            var expected = true;
            byte unit = 1;
            ushort outputAddress = 2;
            short outputValue = 2344;

            var actual = client.WriteSingleRegister(unit, outputAddress, outputValue);
            Assert.IsTrue(client.RequestArray.SequenceEqual(requestArray));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void WriteMultipleCoilsTest()
        {
            byte[] requestArray = TestUtility.CreateModbusUtility().HexStringToBytes("01 0F 00 00 00 0A 02 3A 01 36 58");
            byte[] receiveArray = TestUtility.CreateModbusUtility().HexStringToBytes("01 0F 00 00 00 0A D5 CC");
            var client = CreateTcpModbusClient(receiveArray);
            var expected = true;
            byte unit = 1;
            ushort startAddress = 0;
            ushort quantity = 10;
            byte[] OutputValues = new byte[] { 58, 01 };

            var actual = client.WriteMultipleCoils(unit, startAddress, quantity, OutputValues);
            Assert.IsTrue(client.RequestArray.SequenceEqual(requestArray));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void WriteMultipleRegistersTest()
        {
            byte[] requestArray = TestUtility.CreateModbusUtility().HexStringToBytes("01 10 00 00 00 0A 14 04 D2 00 00 00 00 FF F3 00 0C FF AA 00 DF 00 00 00 00 FF F8 58 66");
            byte[] receiveArray = TestUtility.CreateModbusUtility().HexStringToBytes("01 10 00 00 00 0A 40 0E");
            var client = CreateTcpModbusClient(receiveArray);
            var expected = true;

            byte unit = 1;
            ushort startAddress = 0;
            ushort quantity = 10;
            short[] outputValues = new short[] { 1234, 0, 0, -13, 12, -86, 223, 0, 0, -8 };

            var actual = client.WriteMultipleRegisters(unit, startAddress, quantity, outputValues);
            Assert.IsTrue(client.RequestArray.SequenceEqual(requestArray));
            Assert.AreEqual(expected, actual);
        }
    }
}