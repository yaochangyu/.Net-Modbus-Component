// ***********************************************************************
// Assembly         : Tako.CRC
// Author           : 余小章
// Created          : 08-19-2014
//
// Last Modified By : 余小章
// Last Modified On : 08-19-2014
// ***********************************************************************
// <copyright file="CRC32.cs" company="">
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
    /// Class CRC32.
    /// </summary>
    internal class CRC32Provider : CRCProviderBase
    {
        /// <summary>
        /// The _CRC table
        /// </summary>
        private uint[] _crcTable = new uint[256];

        /// <summary>
        /// The _polynomial
        /// </summary>
        private uint _polynomial = 0xedb88320;

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
        /// Initializes a new instance of the <see cref="CRC32Provider"/> class.
        /// </summary>
        /// <param name="Polynomial">The polynomial.</param>
        public CRC32Provider(uint Polynomial = 0xedb88320)
        {
            this.Polynomial = Polynomial;
            uint temp = 0;
            for (uint i = 0; i < this.CRCTable.Length; ++i)
            {
                temp = i;
                for (uint j = 8; j > 0; --j)
                {
                    if ((temp & 1) == 1)
                    {
                        temp = (temp >> 1) ^ this.Polynomial;
                    }
                    else
                    {
                        temp >>= 1;
                    }
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
            uint crc = 0xffffffff;
            for (int i = 0; i < OriginalArray.Length; ++i)
            {
                byte index = (byte)(((crc) & 0xff) ^ OriginalArray[i]);
                crc = (uint)((crc >> 8) ^ _crcTable[index]);
            }
            var crcTemp = ~crc;
            var crcArray = BitConverter.GetBytes(crcTemp);

            base.GetCRCStatus(ref status, crcTemp, crcArray, OriginalArray);
            return status;
        }
    }
}