using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Reflection;
using Tako.Component.Extension;
using Tako.Modbus.Core;

namespace Tako.Modbus.Client.UnitTest
{
    /// <summary>
    ///This is a test class for AsciiModbusResponseTest and is intended
    ///to contain all AsciiModbusResponseTest unit Tests
    ///</summary>
    [TestClass()]
    public class AsciiModbusResponseTest
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

        private const string CLASS_NAME = "Tako.Modbus.Client.AsciiModbusResponse";

        //[TestMethod()]
        //[ExpectedException(typeof(ModbusException))]
        //public void ReadCoils_Exception_Test()
        //{
        //    var target = TestUtility.GenerateInstance(CLASS_NAME);
        //    byte[] RequestArray = TestUtility.CreateModbusUtility().HexStringToBytes("3A 30 31 30 31 30 30 30 30 30 30 43 38 33 36 0D 0A");
        //    byte[] ResponseArray = TestUtility.CreateModbusUtility().HexStringToBytes("3A 30 31 38 31 30 32 37 43 0D 0A");
        //    byte[] expected = ResponseArray;
        //    byte[] actual;
        //    actual = target.ReadCoils(RequestArray, ResponseArray);
        //    Assert.IsTrue(expected.SequenceEqual(actual));
        //}

        [TestMethod()]
        [DeploymentItem("TestDoc\\AsciiReadFunc1Test.csv")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestDoc\\AsciiReadFunc1Test.csv", "AsciiReadFunc1Test#csv", DataAccessMethod.Sequential)]
        public void ReadCoilsTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte[] RequestArray = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[0].ToString());
            byte[] ResponseArray = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[1].ToString());
            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[2].ToString());

            byte[] actual;
            actual = target.ReadCoils(RequestArray, ResponseArray);

            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod()]
        [DeploymentItem("TestDoc\\AsciiReadFunc2Test.csv")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestDoc\\AsciiReadFunc2Test.csv", "AsciiReadFunc2Test#csv", DataAccessMethod.Sequential)]
        public void ReadDiscreteInputsTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte[] RequestArray = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[0].ToString());
            byte[] ResponseArray = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[1].ToString());
            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[2].ToString());
            byte[] actual;
            actual = target.ReadDiscreteInputs(RequestArray, ResponseArray);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        /// <summary>
        ///A test for ReadHoldingRegisters
        ///</summary>
        [TestMethod()]
        [DeploymentItem("TestDoc\\AsciiReadFunc3Test.csv")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestDoc\\AsciiReadFunc3Test.csv", "AsciiReadFunc3Test#csv", DataAccessMethod.Sequential)]
        public void ReadHoldingRegistersTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte[] RequestArray = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[0].ToString());
            byte[] ResponseArray = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[1].ToString());
            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[2].ToString());
            byte[] actual;
            actual = target.ReadHoldingRegisters(RequestArray, ResponseArray);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod()]
        [DeploymentItem("TestDoc\\AsciiReadFunc4Test.csv")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestDoc\\AsciiReadFunc4Test.csv", "AsciiReadFunc4Test#csv", DataAccessMethod.Sequential)]
        public void ReadInputRegistersTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte[] RequestArray = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[0].ToString());
            byte[] ResponseArray = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[1].ToString());
            byte[] expected = TestUtility.CreateModbusUtility().HexStringToBytes(TestContext.DataRow[2].ToString());
            byte[] actual;
            actual = target.ReadInputRegisters(RequestArray, ResponseArray);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod()]
        public void WriteSingleCoilTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte[] RequestArray = TestUtility.CreateModbusUtility().HexStringToBytes("3A 30 31 30 35 30 30 30 31 46 46 30 30 46 41 0D 0A");
            byte[] ResponseArray = TestUtility.CreateModbusUtility().HexStringToBytes("3A 30 31 30 35 30 30 30 31 46 46 30 30 46 41 0D 0A");
            byte[] expected = ResponseArray;
            byte[] actual;
            actual = target.WriteSingleCoil(RequestArray, ResponseArray);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod()]
        public void WriteSingleRegisterTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte[] RequestArray = TestUtility.CreateModbusUtility().HexStringToBytes("3A 30 31 30 36 30 30 30 35 30 32 33 33 42 46 0D 0A");
            byte[] ResponseArray = TestUtility.CreateModbusUtility().HexStringToBytes("3A 30 31 30 36 30 30 30 35 30 32 33 33 42 46 0D 0A");
            byte[] expected = ResponseArray;
            byte[] actual;
            actual = target.WriteSingleRegister(RequestArray, ResponseArray);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod()]
        public void WriteMultipleCoilsTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte[] RequestArray = TestUtility.CreateModbusUtility().HexStringToBytes("3A 30 31 30 46 30 30 30 30 30 30 30 41 30 32 32 37 30 30 42 44 0D 0A");
            byte[] ResponseArray = TestUtility.CreateModbusUtility().HexStringToBytes("3A 30 31 30 46 30 30 30 30 30 30 30 41 45 36 0D 0A");
            byte[] expected = ResponseArray;
            byte[] actual;
            actual = target.WriteMultipleCoils(RequestArray, ResponseArray);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod()]
        public void WriteMultipleRegistersTest()
        {
            var target = TestUtility.GenerateInstance(CLASS_NAME);

            byte[] RequestArray = TestUtility.CreateModbusUtility().HexStringToBytes("3A 30 31 31 30 30 30 30 30 30 30 30 41 31 34 30 30 30 30 30 30 30 43 30 30 30 30 46 46 42 45 30 30 37 33 30 30 30 30 30 30 30 30 30 30 30 30 30 30 30 30 30 30 30 30 39 35 0D 0A");
            byte[] ResponseArray = TestUtility.CreateModbusUtility().HexStringToBytes("3A 30 31 31 30 30 30 30 30 30 30 30 41 45 35 0D 0A");
            byte[] expected = ResponseArray;
            byte[] actual;
            actual = target.WriteMultipleRegisters(RequestArray, ResponseArray);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }
    }
}