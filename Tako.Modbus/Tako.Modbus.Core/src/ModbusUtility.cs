// ***********************************************************************
// Assembly         : Tako.Modbus.Core
// Author           : 余小章
// Created          : 08-19-2014
//
// Last Modified By : 余小章
// Last Modified On : 08-19-2014
// ***********************************************************************
// <copyright file="ModbusUtility.cs" company="">
//     Copyright (c) . 余小章 . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using System.Text;
using Tako.CRC;

/// <summary>
/// The Modbus namespace.
/// </summary>
namespace Tako.Modbus.Core
{
    /// <summary>
    /// Class ModbusUtility.
    /// </summary>
    public class ModbusUtility
    {
        /// <summary>
        /// The asci i_ star t_ symbol
        /// </summary>
        public readonly static string ASCII_START_SYMBOL = ":";

        /// <summary>
        /// The asci i_ en d_ symbol
        /// </summary>
        public readonly static string ASCII_END_SYMBOL = "\r\n";

        /// <summary>
        /// The m_ CRC manager
        /// </summary>
        private CRCManager m_CrcManager = new CRCManager();

        /// <summary>
        /// The m_ CRC provider
        /// </summary>
        private CRCProviderBase m_CrcProvider;

        /// <summary>
        /// The s_ symbol
        /// </summary>
        private string[] m_Symbol = new string[] { " ", ",", "-" };

        /// <summary>
        /// Byteses to binary string.
        /// </summary>
        /// <param name="HexArray">The hexadecimal array.</param>
        /// <returns>System.String.</returns>
        public string BytesToBinaryString(byte[] HexArray)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var b in HexArray)
            {
                sb.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
            }
            return sb.ToString();
        }

        /// <summary>
        /// Hexadecimals the string to bytes.
        /// </summary>
        /// <param name="Hex">The hexadecimal.</param>
        /// <returns>System.Byte[].</returns>
        public byte[] HexStringToBytes(string Hex)
        {
            string filter = m_Symbol.Aggregate(Hex, (current, symbol) => current.Replace(symbol, ""));

            return Enumerable.Range(0, filter.Length)
                              .Where(x => x % 2 == 0)
                              .Select(x => Convert.ToByte(filter.Substring(x, 2), 16))
                              .ToArray();
        }

        /// <summary>
        /// Byteses to hexadecimal string.
        /// </summary>
        /// <param name="HexArray">The hexadecimal array.</param>
        /// <returns>System.String.</returns>
        public string BytesToHexString(byte[] HexArray)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var b in HexArray)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// Calculates the LRC.
        /// </summary>
        /// <param name="DataArray">The data array.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.ArgumentNullException">data</exception>
        public string CalculateLRC(byte[] DataArray)
        {
            if (DataArray == null)
                throw new ArgumentNullException("data");

            byte lrc = 0;
            foreach (byte b in DataArray)
            {
                lrc += b;
            }

            lrc = (byte)((lrc ^ 0xFF) + 1);

            var hex = lrc.ToString("X2");
            return hex;
        }

        /// <summary>
        /// Calculates the CRC.
        /// </summary>
        /// <param name="DataArray">The data array.</param>
        /// <returns>System.Byte[].</returns>
        public byte[] CalculateCRC(byte[] DataArray)
        {
            if (m_CrcProvider == null)
            {
                m_CrcProvider = m_CrcManager.CreateCRCProvider(EnumCRCProvider.CRC16Modbus);
            }
            var crcArray = m_CrcProvider.GetCRC(DataArray).CrcArray;
            Array.Reverse(crcArray);
            return crcArray;
        }
    }
}