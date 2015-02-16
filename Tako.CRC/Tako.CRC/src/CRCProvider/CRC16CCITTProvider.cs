// ***********************************************************************
// Assembly         : Tako.CRC
// Author           : 余小章
// Created          : 08-19-2014
//
// Last Modified By : 余小章
// Last Modified On : 08-19-2014
// ***********************************************************************
// <copyright file="CRC16CCITT.cs" company="">
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
    /// Class CRC16CCITT.
    /// </summary>
    internal class CRC16CCITTProvider : CRCProviderBase
    {
        /// <summary>
        /// The initail
        /// </summary>
        private const uint initail = 4129;

        /// <summary>
        /// The _CRC table
        /// </summary>
        private uint[] _crcTable = new uint[256];

        /// <summary>
        /// The _polynomial
        /// </summary>
        private uint _polynomial = 0;

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
        /// Initializes a new instance of the <see cref="CRC16CCITTProvider"/> class.
        /// </summary>
        /// <param name="Polynomial">The polynomial.</param>
        public CRC16CCITTProvider(uint Polynomial = 0)
        {
            this.Polynomial = Polynomial;

            for (uint i = 0; i < this.CRCTable.Length; ++i)
            {
                uint temp = 0;
                uint value = i << 8;
                for (uint j = 0; j < 8; ++j)
                {
                    if (((temp ^ value) & 0x8000) != 0)
                    {
                        temp = (temp << 1) ^ initail;
                    }
                    else
                    {
                        temp <<= 1;
                    }
                    value <<= 1;
                }

                this.CRCTable[i] = temp;
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
            ushort crc = (ushort)this.Polynomial;
            for (int i = 0; i < OriginalArray.Length; ++i)
            {
                crc = (ushort)((crc << 8) ^ _crcTable[((crc >> 8) ^ (0xff & OriginalArray[i]))]);
            }
            var crcArray = BitConverter.GetBytes(crc);
            base.GetCRCStatus(ref status, crc, crcArray, OriginalArray);
            return status;
        }
    }
}