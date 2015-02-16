// ***********************************************************************
// Assembly         : Tako.CRC
// Author           : 余小章
// Created          : 08-19-2014
//
// Last Modified By : 余小章
// Last Modified On : 08-19-2014
// ***********************************************************************
// <copyright file="AbsCRCProvider.cs" company="">
//     Copyright (c) . 余小章 . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using System.Text;

/// <summary>
/// The CRC namespace.
/// </summary>
namespace Tako.CRC
{
    /// <summary>
    /// Class AbsCRCProvider.
    /// </summary>
    public abstract class CRCProviderBase
    {
        /// <summary>
        /// The _symbol
        /// </summary>
        internal string[] _symbol = new string[] { " ", ",", "-", "|" };

        /// <summary>
        /// Gets the CRC table.
        /// </summary>
        /// <value>The CRC table.</value>
        protected abstract uint[] CRCTable { get; }

        /// <summary>
        /// Gets or sets the polynomial.
        /// </summary>
        /// <value>The polynomial.</value>
        protected abstract uint Polynomial { get; set; }

        /// <summary>
        /// Gets or sets the data format.
        /// </summary>
        /// <value>The data format.</value>
        internal virtual EnumOriginalDataFormat DataFormat { get; set; }

        /// <summary>
        /// Gets the CRC.
        /// </summary>
        /// <param name="OriginalData">The original data.</param>
        /// <returns>CRCStatus.</returns>
        /// <exception cref="System.ArgumentNullException">OriginalData</exception>
        public virtual CRCStatus GetCRC(string OriginalData)
        {
            if (string.IsNullOrEmpty(OriginalData))
            {
                throw new ArgumentNullException("OriginalData");
            }
            byte[] dataArray = null;

            switch (DataFormat)
            {
                case EnumOriginalDataFormat.ASCII:
                    string filter = _symbol.Aggregate(OriginalData, (current, symbol) => current.Replace(symbol, ""));
                    dataArray = Encoding.ASCII.GetBytes(filter);
                    break;

                case EnumOriginalDataFormat.HEX:
                    dataArray = HexStringToBytes(OriginalData);
                    break;
            }
            CRCStatus status = this.GetCRC(dataArray);
            return status;
        }

        /// <summary>
        /// Gets the CRC.
        /// </summary>
        /// <param name="OriginalArray">The original array.</param>
        /// <returns>CRCStatus.</returns>
        /// <exception cref="System.ArgumentNullException">OriginalArray</exception>
        public virtual CRCStatus GetCRC(byte[] OriginalArray)
        {
            if (OriginalArray == null || OriginalArray.Length <= 0)
            {
                throw new ArgumentNullException("OriginalArray");
            }

            return new CRCStatus();
        }

        /// <summary>
        /// Gets the CRC status.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="CRC">The CRC.</param>
        /// <param name="CrcArray">The CRC array.</param>
        /// <param name="OriginalArray">The original array.</param>
        protected void GetCRCStatus(ref CRCStatus status, uint CRC, byte[] CrcArray, byte[] OriginalArray)
        {
            //0xC57A
            //C5 is hi byte
            //7A is low byte

            status.CrcDecimal = CRC;
            var crcHex = CRC.ToString("X");

            if (crcHex.Length > 2 && crcHex.Length < 4)
            {
                status.CrcHexadecimal = crcHex.PadLeft(4, '0');
            }
            else if (crcHex.Length > 4 && crcHex.Length < 8)
            {
                status.CrcHexadecimal = crcHex.PadLeft(8, '0');
            }
            else
            {
                status.CrcHexadecimal = crcHex;
            }
            byte[] fullData = new byte[OriginalArray.Length + CrcArray.Length];
            Array.Copy(OriginalArray, fullData, OriginalArray.Length);
            var reverseCrcArray = new byte[CrcArray.Length];
            Array.Copy(CrcArray, reverseCrcArray, CrcArray.Length);

            Array.Reverse(reverseCrcArray);
            status.CrcArray = reverseCrcArray;

            Array.Copy(reverseCrcArray, reverseCrcArray.GetLowerBound(0), fullData, OriginalArray.GetUpperBound(0) + 1, reverseCrcArray.Length);

            status.FullDataArray = fullData;

            switch (DataFormat)
            {
                case EnumOriginalDataFormat.ASCII:
                    status.FullDataHexadecimal = Encoding.ASCII.GetString(OriginalArray) + status.CrcHexadecimal;
                    break;

                case EnumOriginalDataFormat.HEX:
                    status.FullDataHexadecimal = BytesToHexString(OriginalArray) + status.CrcHexadecimal;
                    break;
            }

            //return status;
        }

        /// <summary>
        /// Hexadecimals the string to bytes.
        /// </summary>
        /// <param name="Hex">The hexadecimal.</param>
        /// <returns>System.Byte[].</returns>
        /// <exception cref="System.ArgumentNullException">Hex</exception>
        public virtual byte[] HexStringToBytes(string Hex)
        {
            if (string.IsNullOrEmpty(Hex))
            {
                throw new ArgumentNullException("Hex");
            }
            string filter = _symbol.Aggregate(Hex, (current, symbol) => current.Replace(symbol, ""));

            return Enumerable.Range(0, filter.Length)
                              .Where(x => x % 2 == 0)
                              .Select(x => Convert.ToByte(filter.Substring(x, 2), 16))
                              .ToArray();

            //ulong number = ulong.Parse(filter, System.Globalization.NumberStyles.AllowHexSpecifier);
            //return BitConverter.GetBytes(number);
        }

        /// <summary>
        /// Byteses to hexadecimal string.
        /// </summary>
        /// <param name="HexArray">The hexadecimal array.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.ArgumentNullException">HexArray</exception>
        public virtual string BytesToHexString(byte[] HexArray)
        {
            if (HexArray == null || HexArray.Length <= 0)
            {
                throw new ArgumentNullException("HexArray");
            }

            var result = BitConverter.ToString(HexArray).Replace("-", "");
            return result;
        }
    }
}