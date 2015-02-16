using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;
using Tako.Component.Extension;
using Tako.Modbus.Core;

namespace Tako.Modbus.Client.UnitTest
{
    [TestClass]
    public class TcpModbusClientUnitTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        private static Mock<IModbusSocket> s_stubModbusSocket = null;
        private static TcpModbusClient s_client = null;

        public TcpModbusClientUnitTest()
        {
            if (s_stubModbusSocket == null)
            {
                s_stubModbusSocket = new Mock<IModbusSocket>();
                s_stubModbusSocket.Setup(
                 o => o.Connect<TcpModbusConnectConfig>(new TcpModbusConnectConfig()
                 {
                     IpAddress = "192.168.1.1",
                     Port = 502
                 })).Returns(true);
            }
            if (s_client == null)
            {
                s_client = new TcpModbusClient();
                s_client.ModbusSocket = s_stubModbusSocket.Object;
            }
        }

        private TcpModbusClient CreateTcpModbusClient(byte[] receiveArray)
        {
            s_stubModbusSocket.Setup(o => o.Receive()).Returns(receiveArray);
            return s_client;
        }

        private static ushort s_ReadCoilsTransaction = 3143;
        private static ushort s_ReadDiscreteInputsTransaction = 3707;
        private static ushort s_ReadHoldingRegistersTransaction = 3968;
        private static ushort s_ReadInputRegistersTransaction = 4170;

        [TestMethod()]
        [DeploymentItem("TestDoc\\TcpReadFunc1Test.csv")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestDoc\\TcpReadFunc1Test.csv",
            "TcpReadFunc1Test#csv", DataAccessMethod.Sequential)]
        public void ReadCoilsTest()
        {
            byte[] requestArray = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[0].ToString());
            byte[] receiveArray = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[1].ToString());
            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[2].ToString());
            var client = CreateTcpModbusClient(receiveArray);

            byte unit = 1;
            ushort startAddress = 0;
            ushort quantity = 100;

            client.TransactionId = s_ReadCoilsTransaction;

            var actual = client.ReadCoils(unit, startAddress, quantity);
            Assert.IsTrue(client.RequestArray.SequenceEqual(requestArray));
            Assert.IsTrue(expected.SequenceEqual(actual));
            s_ReadCoilsTransaction++;
        }

        [TestMethod()]
        [DeploymentItem("TestDoc\\TcpReadFunc2Test.csv")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestDoc\\TcpReadFunc2Test.csv",
            "TcpReadFunc2Test#csv", DataAccessMethod.Sequential)]
        public void ReadDiscreteInputsTest()
        {
            byte[] requestArray = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[0].ToString());
            byte[] receiveArray = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[1].ToString());
            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[2].ToString());

            var client = CreateTcpModbusClient(receiveArray);

            byte unit = 1;
            ushort startAddress = 0;
            ushort quantity = 100;
            client.TransactionId = s_ReadDiscreteInputsTransaction;

            var actual = client.ReadDiscreteInputs(unit, startAddress, quantity);
            Assert.IsTrue(client.RequestArray.SequenceEqual(requestArray));
            Assert.IsTrue(expected.SequenceEqual(actual));
            s_ReadDiscreteInputsTransaction++;
        }

        [TestMethod()]
        [DeploymentItem("TestDoc\\TcpReadFunc3Test.csv")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestDoc\\TcpReadFunc3Test.csv",
            "TcpReadFunc3Test#csv", DataAccessMethod.Sequential)]
        public void ReadHoldingRegistersTest()
        {
            byte[] requestArray = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[0].ToString());
            byte[] receiveArray = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[1].ToString());
            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[2].ToString());

            var client = CreateTcpModbusClient(receiveArray);

            byte unit = 1;
            ushort startAddress = 0;
            ushort quantity = 100;
            client.TransactionId = s_ReadHoldingRegistersTransaction;
            var actual = client.ReadHoldingRegisters(unit, startAddress, quantity);
            Assert.IsTrue(client.RequestArray.SequenceEqual(requestArray));
            Assert.IsTrue(expected.SequenceEqual(actual));
            s_ReadHoldingRegistersTransaction++;
        }

        [TestMethod()]
        [DeploymentItem("TestDoc\\TcpReadFunc4Test.csv")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestDoc\\TcpReadFunc4Test.csv",
            "TcpReadFunc4Test#csv", DataAccessMethod.Sequential)]
        public void ReadInputRegistersTest()
        {
            byte[] requestArray = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[0].ToString());
            byte[] receiveArray = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[1].ToString());
            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[2].ToString());

            var client = CreateTcpModbusClient(receiveArray);

            byte unit = 1;
            ushort startAddress = 0;
            ushort quantity = 100;
            client.TransactionId = s_ReadInputRegistersTransaction;
            var actual = client.ReadInputRegisters(unit, startAddress, quantity);
            Assert.IsTrue(client.RequestArray.SequenceEqual(requestArray));
            Assert.IsTrue(expected.SequenceEqual(actual));
            s_ReadInputRegistersTransaction++;
        }

        [TestMethod()]
        public void WriteSingleCoilTest()
        {
            byte[] requestArray = TestUtility.CreateModbusUtility().HexStringToBytes("04 52 00 00 00 06 01 05 00 00 FF 00");
            byte[] receiveArray = TestUtility.CreateModbusUtility().HexStringToBytes("04 52 00 00 00 06 01 05 00 00 FF 00");
            var client = CreateTcpModbusClient(receiveArray);

            var expected = true;
            byte unit = 1;
            ushort outputAddress = 0;
            bool outputValue = true;
            ushort transaction = 1106;
            client.TransactionId = transaction;
            var actual = client.WriteSingleCoil(unit, outputAddress, outputValue);
            Assert.IsTrue(client.RequestArray.SequenceEqual(requestArray));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void WriteSingleRegisterTest()
        {
            byte[] requestArray = TestUtility.CreateModbusUtility().HexStringToBytes("04 F9 00 00 00 06 01 06 00 02 00 EA");
            byte[] receiveArray = TestUtility.CreateModbusUtility().HexStringToBytes("04 F9 00 00 00 06 01 06 00 02 00 EA");
            var client = CreateTcpModbusClient(receiveArray);

            var expected = true;
            byte unit = 1;
            ushort outputAddress = 2;
            short outputValue = 234;
            ushort transaction = 1273;
            client.TransactionId = transaction;
            var response = client.WriteSingleRegister(unit, outputAddress, outputValue);
            Assert.IsTrue(client.RequestArray.SequenceEqual(requestArray));
            Assert.AreEqual(expected, response);
        }

        [TestMethod()]
        public void WriteMultipleCoilsTest()
        {
            byte[] requestArray = TestUtility.CreateModbusUtility().HexStringToBytes("03 7F 00 00 00 09 01 0F 00 00 00 09 02 0E 00");
            byte[] receiveArray = TestUtility.CreateModbusUtility().HexStringToBytes("03 7F 00 00 00 06 01 0F 00 00 00 09");

            var client = CreateTcpModbusClient(receiveArray);

            var expected = true;
            byte unit = 1;
            ushort startAddress = 0;
            ushort quantity = 9;
            byte[] outputValues = new byte[] { 0x0E, 0x00 };
            ushort transaction = 895;
            client.TransactionId = transaction;
            var response = client.WriteMultipleCoils(unit, startAddress, quantity, outputValues);
            Assert.IsTrue(client.RequestArray.SequenceEqual(requestArray));
            Assert.AreEqual(expected, response);
        }

        [TestMethod()]
        public void WriteMultipleRegistersTest()
        {
            byte[] requestArray = TestUtility.CreateModbusUtility().HexStringToBytes("03 DA 00 00 00 1B 01 10 00 00 00 0A 14 00 4E 00 57 FF B2 FF A9 00 17 00 23 00 7B 00 0C 00 21 00 06");
            byte[] receiveArray = TestUtility.CreateModbusUtility().HexStringToBytes("03 DA 00 00 00 06 01 10 00 00 00 0A");

            var client = CreateTcpModbusClient(receiveArray);

            var expected = true;
            byte unit = 1;
            ushort startAddress = 0;
            ushort quantity = 10;
            short[] outputValues = new short[] { 78, 87, -78, -87, 23, 35, 123, 12, 33, 6 };
            ushort transaction = 986;
            client.TransactionId = transaction;
            var response = client.WriteMultipleRegisters(unit, startAddress, quantity, outputValues);
            Assert.IsTrue(client.RequestArray.SequenceEqual(requestArray));
            Assert.AreEqual(expected, response);
        }

        [TestMethod()]
        [ExpectedException(typeof(ModbusException))]
        public void ReadCoilsRequest_Exception_Test()
        {
            var client = CreateTcpModbusClient(null);

            byte unit = 0;
            ushort startAddress = 0;
            ushort quantity = 0;
            ushort Transaction = 0;
            byte[] expected = null;
            byte[] actual = null;
            client.TransactionId = Transaction;
            actual = client.ReadCoils(unit, startAddress, quantity);
        }
    }
}