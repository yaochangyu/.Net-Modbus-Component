using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO.Ports;
using Tako.Modbus.Core;

namespace Tako.Modbus.Client.UnitTest
{
    /// <summary>
    ///This is a test class for ModbusAdpaterTest and is intended
    ///to contain all ModbusAdpaterTest unit Tests
    ///</summary>
    [TestClass()]
    public class ModbusAdpaterTest
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

        //[TestMethod()]
        public void CreateModbusAsciiClientTest()
        {
            ModbusClientAdpater adpater = new ModbusClientAdpater();

            ModbusClientBase client = adpater.CreateModbusClient(EnumModbusFraming.ASCII);
            client.Connect(new SerialModbusConnectConifg() { PortName = "COM6", BaudRate = 115200, Parity = Parity.None, StopBits = StopBits.One });
            var result = client.ReadCoilsToDecimal(1, 0, 10);
        }

        //[TestMethod()]
        public void CreateModbusRtuClientTest()
        {
            ModbusClientAdpater adpater = new ModbusClientAdpater();

            ModbusClientBase client = adpater.CreateModbusClient(EnumModbusFraming.RTU);
            client.Connect(new SerialModbusConnectConifg() { PortName = "COM6", BaudRate = 115200, Parity = Parity.None, StopBits = StopBits.One });
            var result = client.ReadCoilsToDecimal(1, 0, 10);
        }

        //[TestMethod()]
        public void CreateModbusTcpClientTest()
        {
            ModbusClientAdpater adpater = new ModbusClientAdpater();
            ModbusClientBase client = adpater.CreateModbusClient(EnumModbusFraming.TCP);
            client.Connect(new TcpModbusConnectConfig() { IpAddress = "127.0.0.1", Port = 502 });
            var result = client.ReadCoilsToDecimal(1, 0, 10);
        }
    }
}