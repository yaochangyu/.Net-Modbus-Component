// ***********************************************************************
// Assembly         : Tako.CRC
// Author           : 余小章
// Created          : 08-19-2014
//
// Last Modified By : 余小章
// Last Modified On : 08-19-2014
// ***********************************************************************
// <copyright file="CRC16Kermit.cs" company="">
//     Copyright (c) . 余小章 . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

/// <summary>
/// The CRC namespace.
/// </summary>
namespace Tako.CRC
{
    /// <summary>
    /// Class CRC16Kermit.
    /// </summary>
    internal class CRC16KermitProvider : CRCProviderBase
    {
        /// <summary>
        /// The _CRC table
        /// </summary>
        private uint[] _crcTable = new uint[256];

        /// <summary>
        /// The _polynomial
        /// </summary>
        private uint _polynomial = 0x8408;

        /// <summary>
        /// Gets the CRC table.
        /// </summary>
        /// <value>The CRC table.</value>
        protected override uint[] CRCTable
        {
            get { return _crcTable; }
        }

        /// <summary>
        /// Gets or sets the polynomial.
        /// </summary>
        /// <value>The polynomial.</value>
        protected override uint Polynomial
        {
            get { return _polynomial; }
            set { _polynomial = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CRC16KermitProvider"/> class.
        /// </summary>
        /// <param name="Polynomial">The polynomial.</param>
        public CRC16KermitProvider(uint Polynomial = 0x8408)
        {
            this.Polynomial = Polynomial;
            for (uint i = 0; i < this.CRCTable.Length; ++i)
            {
                uint value = 0;
                uint temp = i;
                for (uint j = 0; j < 8; ++j)
                {
                    if (((value ^ temp) & 0x0001) != 0)
                    {
                        value = (value >> 1) ^ this.Polynomial;
                    }
                    else
                    {
                        value >>= 1;
                    }
                    temp >>= 1;
                }
                this.CRCTable[i] = value;
            }
        }

        /// <summary>
        /// Gets the CRC.
        /// </summary>
        /// <param name="OriginalArray">The original array.</param>
        /// <returns>CRCStatus.</returns>
        public override CRCStatus GetCRC(byte[] OriginalArray)
        {
            CRCStatus status = base.GetCRC(OriginalArray);
            ushort crc = 0;

            for (uint i = 0; i < OriginalArray.Length; ++i)
            {
                byte index = (byte)(crc ^ OriginalArray[i]);
                crc = (ushort)((crc >> 8) ^ this.CRCTable[index]);
            }
            var crcArray = BitConverter.GetBytes(crc);

            Array.Reverse(crcArray);

            var crcTemp = Convert.ToUInt32(crcArray[1].ToString("X") + crcArray[0].ToString("X"), 16);
            base.GetCRCStatus(ref status, crcTemp, crcArray, OriginalArray);
            return status;
        }
    }
}