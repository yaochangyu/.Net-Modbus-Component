// ***********************************************************************
// Assembly         : Tako.CRC
// Author           : 余小章
// Created          : 08-19-2014
//
// Last Modified By : 余小章
// Last Modified On : 08-19-2014
// ***********************************************************************
// <copyright file="CRC16.cs" company="">
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
    /// Class CRC16.
    /// </summary>
    internal class CRC16Provider : CRCProviderBase
    {
        //fields

        /// <summary>
        /// The _polynomial
        /// </summary>
        private uint _polynomial = 0xA001;

        /// <summary>
        /// The _CRC table
        /// </summary>
        private uint[] _crcTable = new uint[256];

        //property

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
        /// Initializes a new instance of the <see cref="CRC16Provider"/> class.
        /// </summary>
        /// <param name="Polynomial">The polynomial.</param>
        public CRC16Provider(uint Polynomial = 0xA001)
        {
            this.Polynomial = Polynomial;

            uint value;
            uint temp;
            for (uint i = 0; i < this.CRCTable.Length; ++i)
            {
                value = 0;
                temp = i;
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
                crc = (ushort)((crc >> 8) ^ this._crcTable[index]);
            }
            var crcArray = BitConverter.GetBytes(crc);

            base.GetCRCStatus(ref status, crc, crcArray, OriginalArray);
            return status;
        }
    }
}