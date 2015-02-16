using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Tako.CRC.UnitTestProject
{
    /// <summary>
    ///This is a test class for CRCManagerTest and is intended
    ///to contain all CRCManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CRCManagerTest
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

        private CRCManager _manager = new CRCManager();

        [TestMethod]
        public void CRC16_ASCII_Test()
        {
            this._manager.DataFormat = EnumOriginalDataFormat.ASCII;
            CRCProviderBase provider = this._manager.CreateCRCProvider(EnumCRCProvider.CRC16);
            var source = "1234567890";
            var expectedCheckSum = "C57A";
            var expectedCheckSumValue = Convert.ToUInt32(expectedCheckSum, 16);
            var expectedCheckSumArray = provider.HexStringToBytes(expectedCheckSum);
            var expectedFullData = source + expectedCheckSum;

            CRCStatus actual = provider.GetCRC(source);
            Assert.AreEqual(expectedCheckSum, actual.CrcHexadecimal);
            Assert.AreEqual(expectedCheckSumValue, actual.CrcDecimal);
            Assert.AreEqual(expectedFullData, actual.FullDataHexadecimal);
            Assert.IsTrue(expectedCheckSumArray.SequenceEqual(actual.CrcArray));
        }

        [TestMethod]
        public void CRC16_HEX_Test()
        {
            this._manager.DataFormat = EnumOriginalDataFormat.HEX;
            CRCProviderBase provider = this._manager.CreateCRCProvider(EnumCRCProvider.CRC16);
            var source = "1234567890";
            var expectedCheckSum = "4F74";
            var expectedCheckSumValue = Convert.ToUInt32(expectedCheckSum, 16);
            var expectedCheckSumArray = provider.HexStringToBytes(expectedCheckSum);
            var expectedFullData = source + expectedCheckSum;

            CRCStatus actual = provider.GetCRC(source);
            Assert.AreEqual(expectedCheckSum, actual.CrcHexadecimal);
            Assert.AreEqual(expectedCheckSumValue, actual.CrcDecimal);
            Assert.AreEqual(expectedFullData, actual.FullDataHexadecimal);
            Assert.IsTrue(expectedCheckSumArray.SequenceEqual(actual.CrcArray));
        }

        [TestMethod]
        public void CRC32_ASCII_Test()
        {
            this._manager.DataFormat = EnumOriginalDataFormat.ASCII;
            CRCProviderBase provider = this._manager.CreateCRCProvider(EnumCRCProvider.CRC32);
            var source = "1234567890";
            var expectedCheckSum = "261DAEE5";
            var expectedCheckSumValue = Convert.ToUInt32(expectedCheckSum, 16);
            var expectedCheckSumArray = provider.HexStringToBytes(expectedCheckSum);
            var expectedFullData = source + expectedCheckSum;
            CRCStatus actual = provider.GetCRC(source);
            Assert.AreEqual(expectedCheckSum, actual.CrcHexadecimal);
            Assert.AreEqual(expectedCheckSumValue, actual.CrcDecimal);
            Assert.AreEqual(expectedFullData, actual.FullDataHexadecimal);
            Assert.IsTrue(expectedCheckSumArray.SequenceEqual(actual.CrcArray));
        }

        [TestMethod]
        public void CRC32_HEX_Test()
        {
            this._manager.DataFormat = EnumOriginalDataFormat.HEX;
            CRCProviderBase provider = this._manager.CreateCRCProvider(EnumCRCProvider.CRC32);
            var source = "1234567890";
            var expectedCheckSum = "DC936EB1";
            var expectedCheckSumValue = Convert.ToUInt32(expectedCheckSum, 16);
            var expectedCheckSumArray = provider.HexStringToBytes(expectedCheckSum);
            var expectedFullData = source + expectedCheckSum;
            CRCStatus actual = provider.GetCRC(source);
            Assert.AreEqual(expectedCheckSum, actual.CrcHexadecimal);
            Assert.AreEqual(expectedCheckSumValue, actual.CrcDecimal);
            Assert.AreEqual(expectedFullData, actual.FullDataHexadecimal);
            Assert.IsTrue(expectedCheckSumArray.SequenceEqual(actual.CrcArray));
        }

        [TestMethod]
        public void CRC16CCITT_0x0000_ASCII_Test()
        {
            this._manager.DataFormat = EnumOriginalDataFormat.ASCII;
            CRCProviderBase provider = this._manager.CreateCRCProvider(EnumCRCProvider.CRC16CCITT_0x0000);
            var source = "1234567890";
            var expectedCheckSum = "D321";
            var expectedCheckSumValue = Convert.ToUInt32(expectedCheckSum, 16);
            var expectedCheckSumArray = provider.HexStringToBytes(expectedCheckSum);
            var expectedFullData = source + expectedCheckSum;
            CRCStatus actual = provider.GetCRC(source);
            Assert.AreEqual(expectedCheckSum, actual.CrcHexadecimal);
            Assert.AreEqual(expectedCheckSumValue, actual.CrcDecimal);
            Assert.AreEqual(expectedFullData, actual.FullDataHexadecimal);
            Assert.IsTrue(expectedCheckSumArray.SequenceEqual(actual.CrcArray));
        }

        [TestMethod]
        public void CRC16CCITT_0x0000_HEX_Test()
        {
            this._manager.DataFormat = EnumOriginalDataFormat.HEX;
            CRCProviderBase provider = this._manager.CreateCRCProvider(EnumCRCProvider.CRC16CCITT_0x0000);
            var source = "1234567890";
            var expectedCheckSum = "48E6";
            var expectedCheckSumValue = Convert.ToUInt32(expectedCheckSum, 16);
            var expectedCheckSumArray = provider.HexStringToBytes(expectedCheckSum);
            var expectedFullData = source + expectedCheckSum;
            CRCStatus actual = provider.GetCRC(source);
            Assert.AreEqual(expectedCheckSum, actual.CrcHexadecimal);
            Assert.AreEqual(expectedCheckSumValue, actual.CrcDecimal);
            Assert.AreEqual(expectedFullData, actual.FullDataHexadecimal);
            Assert.IsTrue(expectedCheckSumArray.SequenceEqual(actual.CrcArray));
        }

        [TestMethod]
        public void CRC16CCITT_0x1D0F_ASCII_Test()
        {
            this._manager.DataFormat = EnumOriginalDataFormat.ASCII;
            CRCProviderBase provider = this._manager.CreateCRCProvider(EnumCRCProvider.CRC16CCITT_0x1D0F);
            var source = "1234567890";
            var expectedCheckSum = "57D8";
            var expectedCheckSumValue = Convert.ToUInt32(expectedCheckSum, 16);
            var expectedCheckSumArray = provider.HexStringToBytes(expectedCheckSum);
            var expectedFullData = source + expectedCheckSum;
            CRCStatus actual = provider.GetCRC(source);
            Assert.AreEqual(expectedCheckSum, actual.CrcHexadecimal);
            Assert.AreEqual(expectedCheckSumValue, actual.CrcDecimal);
            Assert.AreEqual(expectedFullData, actual.FullDataHexadecimal);
            Assert.IsTrue(expectedCheckSumArray.SequenceEqual(actual.CrcArray));
        }

        [TestMethod]
        public void CRC16CCITT_0x1D0F_HEX_Test()
        {
            this._manager.DataFormat = EnumOriginalDataFormat.HEX;
            CRCProviderBase provider = this._manager.CreateCRCProvider(EnumCRCProvider.CRC16CCITT_0x1D0F);
            var source = "1234567890";
            var expectedCheckSum = "B928";
            var expectedCheckSumValue = Convert.ToUInt32(expectedCheckSum, 16);
            var expectedCheckSumArray = provider.HexStringToBytes(expectedCheckSum);
            var expectedFullData = source + expectedCheckSum;
            CRCStatus actual = provider.GetCRC(source);
            Assert.AreEqual(expectedCheckSum, actual.CrcHexadecimal);
            Assert.AreEqual(expectedCheckSumValue, actual.CrcDecimal);
            Assert.AreEqual(expectedFullData, actual.FullDataHexadecimal);
            Assert.IsTrue(expectedCheckSumArray.SequenceEqual(actual.CrcArray));
        }

        [TestMethod]
        public void CRC16CCITT_0xFFFF_ASCII_Test()
        {
            this._manager.DataFormat = EnumOriginalDataFormat.ASCII;
            CRCProviderBase provider = this._manager.CreateCRCProvider(EnumCRCProvider.CRC16CCITT_0xFFFF);
            var source = "1234567890";
            var expectedCheckSum = "3218";
            var expectedCheckSumValue = Convert.ToUInt32(expectedCheckSum, 16);
            var expectedCheckSumArray = provider.HexStringToBytes(expectedCheckSum);
            var expectedFullData = source + expectedCheckSum;
            CRCStatus actual = provider.GetCRC(source);
            Assert.AreEqual(expectedCheckSum, actual.CrcHexadecimal);
            Assert.AreEqual(expectedCheckSumValue, actual.CrcDecimal);
            Assert.AreEqual(expectedFullData, actual.FullDataHexadecimal);
            Assert.IsTrue(expectedCheckSumArray.SequenceEqual(actual.CrcArray));
        }

        [TestMethod]
        public void CRC16CCITT_0xFFFF_HEX_Test()
        {
            this._manager.DataFormat = EnumOriginalDataFormat.HEX;
            CRCProviderBase provider = this._manager.CreateCRCProvider(EnumCRCProvider.CRC16CCITT_0xFFFF);
            var source = "1234567890";
            var expectedCheckSum = "59EA";
            var expectedCheckSumValue = Convert.ToUInt32(expectedCheckSum, 16);
            var expectedCheckSumArray = provider.HexStringToBytes(expectedCheckSum);
            var expectedFullData = source + expectedCheckSum;
            CRCStatus actual = provider.GetCRC(source);
            Assert.AreEqual(expectedCheckSum, actual.CrcHexadecimal);
            Assert.AreEqual(expectedCheckSumValue, actual.CrcDecimal);
            Assert.AreEqual(expectedFullData, actual.FullDataHexadecimal);
            Assert.IsTrue(expectedCheckSumArray.SequenceEqual(actual.CrcArray));
        }

        [TestMethod]
        public void CRC16Kermit_ASCII_Test()
        {
            this._manager.DataFormat = EnumOriginalDataFormat.ASCII;
            CRCProviderBase provider = this._manager.CreateCRCProvider(EnumCRCProvider.CRC16Kermit);
            var source = "1234567890";
            var expectedCheckSum = "6B28";
            var expectedCheckSumValue = Convert.ToUInt32(expectedCheckSum, 16);
            var expectedCheckSumArray = provider.HexStringToBytes(expectedCheckSum);
            var expectedFullData = source + expectedCheckSum;
            CRCStatus actual = provider.GetCRC(source);
            Assert.AreEqual(expectedCheckSum, actual.CrcHexadecimal);
            Assert.AreEqual(expectedCheckSumValue, actual.CrcDecimal);
            Assert.AreEqual(expectedFullData, actual.FullDataHexadecimal);
            Assert.IsTrue(expectedCheckSumArray.SequenceEqual(actual.CrcArray));
        }

        [TestMethod]
        public void CRC16Kermit_HEX_Test()
        {
            this._manager.DataFormat = EnumOriginalDataFormat.HEX;
            CRCProviderBase provider = this._manager.CreateCRCProvider(EnumCRCProvider.CRC16Kermit);
            var source = "1234567890";
            var expectedCheckSum = "6163";
            var expectedCheckSumValue = Convert.ToUInt32(expectedCheckSum, 16);
            var expectedCheckSumArray = provider.HexStringToBytes(expectedCheckSum);
            var expectedFullData = source + expectedCheckSum;
            CRCStatus actual = provider.GetCRC(source);
            Assert.AreEqual(expectedCheckSum, actual.CrcHexadecimal);
            Assert.AreEqual(expectedCheckSumValue, actual.CrcDecimal);
            Assert.AreEqual(expectedFullData, actual.FullDataHexadecimal);
            Assert.IsTrue(expectedCheckSumArray.SequenceEqual(actual.CrcArray));
        }

        [TestMethod]
        public void CRC8_ASCII_Test()
        {
            this._manager.DataFormat = EnumOriginalDataFormat.ASCII;
            CRCProviderBase provider = this._manager.CreateCRCProvider(EnumCRCProvider.CRC8);
            var source = "1234567890";
            var expectedCheckSum = "38";
            var expectedCheckSumValue = Convert.ToUInt32(expectedCheckSum, 16);
            var expectedCheckSumArray = provider.HexStringToBytes(expectedCheckSum);
            var expectedFullData = source + expectedCheckSum;
            CRCStatus actual = provider.GetCRC(source);
            Assert.AreEqual(expectedCheckSum, actual.CrcHexadecimal);
            Assert.AreEqual(expectedCheckSumValue, actual.CrcDecimal);
            Assert.AreEqual(expectedFullData, actual.FullDataHexadecimal);
            Assert.IsTrue(expectedCheckSumArray.SequenceEqual(actual.CrcArray));
        }

        [TestMethod]
        public void CRC16Modbus_ASCII_Test()
        {
            this._manager.DataFormat = EnumOriginalDataFormat.ASCII;
            CRCProviderBase provider = this._manager.CreateCRCProvider(EnumCRCProvider.CRC16Modbus);
            var source = "1234567890";
            var expectedCheckSum = "C20A";
            var expectedCheckSumValue = Convert.ToUInt32(expectedCheckSum, 16);
            var expectedCheckSumArray = provider.HexStringToBytes(expectedCheckSum);
            var expectedFullData = source + expectedCheckSum;
            CRCStatus actual = provider.GetCRC(source);
            Assert.AreEqual(expectedCheckSum, actual.CrcHexadecimal);
            Assert.AreEqual(expectedCheckSumValue, actual.CrcDecimal);
            Assert.AreEqual(expectedFullData, actual.FullDataHexadecimal);
            Assert.IsTrue(expectedCheckSumArray.SequenceEqual(actual.CrcArray));
        }

        [TestMethod]
        public void CRC16Modbus_HEX_Test()
        {
            this._manager.DataFormat = EnumOriginalDataFormat.HEX;
            CRCProviderBase provider = this._manager.CreateCRCProvider(EnumCRCProvider.CRC16Modbus);
            var source = "CC0900097600040000";
            var expectedCheckSum = "0C47";
            var expectedCheckSumValue = Convert.ToUInt32(expectedCheckSum, 16);
            var expectedCheckSumArray = provider.HexStringToBytes(expectedCheckSum);
            var expectedFullData = source + expectedCheckSum;
            CRCStatus actual = provider.GetCRC(source);
            Assert.AreEqual(expectedCheckSum, actual.CrcHexadecimal);
            Assert.AreEqual(expectedCheckSumValue, actual.CrcDecimal);
            Assert.AreEqual(expectedFullData, actual.FullDataHexadecimal);
            Assert.IsTrue(expectedCheckSumArray.SequenceEqual(actual.CrcArray));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CRC_SourceStringEmpty_Exception_Test()
        {
            this._manager.DataFormat = EnumOriginalDataFormat.HEX;
            CRCProviderBase provider = this._manager.CreateCRCProvider(EnumCRCProvider.CRC16Modbus);
            var source = "";

            CRCStatus actual = provider.GetCRC(source);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CRC_SourceArrayEmpty1_Exception_Test()
        {
            this._manager.DataFormat = EnumOriginalDataFormat.ASCII;
            CRCProviderBase provider = this._manager.CreateCRCProvider(EnumCRCProvider.CRC16Modbus);
            byte[] source = new byte[0];
            provider.GetCRC(source);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CRC_SourceArrayEmpty2_Exception_Test()
        {
            this._manager.DataFormat = EnumOriginalDataFormat.ASCII;
            CRCProviderBase provider = this._manager.CreateCRCProvider(EnumCRCProvider.CRC16Modbus);
            byte[] source = null;
            provider.GetCRC(source);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BytesToHexString1_Exception_Test()
        {
            this._manager.DataFormat = EnumOriginalDataFormat.ASCII;
            CRCProviderBase provider = this._manager.CreateCRCProvider(EnumCRCProvider.CRC16Modbus);
            byte[] source = new byte[0];
            var actual = provider.BytesToHexString(source);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BytesToHexString2_Exception_Test()
        {
            this._manager.DataFormat = EnumOriginalDataFormat.ASCII;
            CRCProviderBase provider = this._manager.CreateCRCProvider(EnumCRCProvider.CRC16Modbus);

            byte[] source = null;
            var actual = provider.BytesToHexString(source);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HexStringToBytes_Exception_Test()
        {
            this._manager.DataFormat = EnumOriginalDataFormat.ASCII;
            CRCProviderBase provider = this._manager.CreateCRCProvider(EnumCRCProvider.CRC16Modbus);

            var actual = provider.HexStringToBytes("");
        }
    }
}