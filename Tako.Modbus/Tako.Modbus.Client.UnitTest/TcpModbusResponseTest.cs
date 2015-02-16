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
    ///This is a test class for TcpModbusResponseTest and is intended
    ///to contain all TcpModbusResponseTest unit Tests
    ///</summary>
    [TestClass()]
    public class TcpModbusResponseTest
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

        private const string CLASS_NAME = "Tako.Modbus.Client.TcpModbusResponse";

        [TestMethod()]
        [DeploymentItem("TestDoc\\TcpReadFunc1Test.csv")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestDoc\\TcpReadFunc1Test.csv",
            "TcpReadFunc1Test#csv", DataAccessMethod.Sequential)]
        public void ReadCoilsTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte[] RequestArray = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[0].ToString());
            byte[] ResponseArray = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[1].ToString());
            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[2].ToString());
            var actual = target.ReadCoils(RequestArray, ResponseArray) as IEnumerable<byte>;
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod()]
        [DeploymentItem("TestDoc\\TcpReadFunc2Test.csv")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestDoc\\TcpReadFunc2Test.csv",
            "TcpReadFunc2Test#csv", DataAccessMethod.Sequential)]
        public void ReadDiscreteInputsTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte[] RequestArray = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[0].ToString());
            byte[] ResponseArray = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[1].ToString());
            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[2].ToString());
            var actual = target.ReadDiscreteInputs(RequestArray, ResponseArray) as IEnumerable<byte>; ;
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod()]
        [DeploymentItem("TestDoc\\TcpReadFunc3Test.csv")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestDoc\\TcpReadFunc3Test.csv",
            "TcpReadFunc3Test#csv", DataAccessMethod.Sequential)]
        public void ReadHoldingRegistersTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte[] RequestArray = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[0].ToString());
            byte[] ResponseArray = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[1].ToString());
            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[2].ToString());

            var actual = target.ReadHoldingRegisters(RequestArray, ResponseArray) as IEnumerable<byte>; ;
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod()]
        [DeploymentItem("TestDoc\\TcpReadFunc4Test.csv")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestDoc\\TcpReadFunc4Test.csv",
            "TcpReadFunc4Test#csv", DataAccessMethod.Sequential)]
        public void ReadInputRegistersTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte[] RequestArray = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[0].ToString());
            byte[] ResponseArray = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[1].ToString());
            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[2].ToString());

            var actual = target.ReadInputRegisters(RequestArray, ResponseArray) as IEnumerable<byte>; ;
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod()]
        public void WriteSingleCoilTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte[] RequestArray = TestUtility.CreateModbusUtility().HexStringToBytes("04 52 00 00 00 06 01 05 00 00 FF 00");
            byte[] ResponseArray = TestUtility.CreateModbusUtility().HexStringToBytes("04 52 00 00 00 06 01 05 00 00 FF 00");
            byte[] expected = ResponseArray;
            byte[] actual;
            actual = target.WriteSingleCoil(RequestArray, ResponseArray);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod()]
        public void WriteSingleRegisterTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte[] RequestArray = TestUtility.CreateModbusUtility().HexStringToBytes("04 F9 00 00 00 06 01 06 00 01 01 00");
            byte[] ResponseArray = TestUtility.CreateModbusUtility().HexStringToBytes("04 F9 00 00 00 06 01 06 00 01 01 00");

            byte[] expected = ResponseArray;
            byte[] actual;
            actual = target.WriteSingleRegister(RequestArray, ResponseArray);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod()]
        public void WriteMultipleCoilsTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte[] RequestArray = TestUtility.CreateModbusUtility().HexStringToBytes("03 7F 00 00 00 09 01 0F 00 00 00 09 02 0E 00");
            byte[] ResponseArray = TestUtility.CreateModbusUtility().HexStringToBytes("03 7F 00 00 00 06 01 0F 00 00 00 09");

            byte[] expected = ResponseArray;
            byte[] actual;
            actual = target.WriteMultipleCoils(RequestArray, ResponseArray);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod()]
        public void WriteMultipleRegistersTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte[] RequestArray =
                TestUtility.CreateModbusUtility().HexStringToBytes(
                    "03 DA 00 00 00 1B 01 10 00 00 00 0A 14 00 4E 00 57 FF B2 FF A9 00 17 00 23 00 7B 00 0C 00 21 00 06");
            byte[] ResponseArray = TestUtility.CreateModbusUtility().HexStringToBytes("03 DA 00 00 00 06 01 10 00 00 00 0A");

            byte[] expected = ResponseArray;
            byte[] actual;
            actual = target.WriteMultipleRegisters(RequestArray, ResponseArray);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }
    }
}