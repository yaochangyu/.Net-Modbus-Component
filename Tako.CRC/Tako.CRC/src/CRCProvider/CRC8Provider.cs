// ***********************************************************************
// Assembly         : Tako.CRC
// Author           : 余小章
// Created          : 08-19-2014
//
// Last Modified By : 余小章
// Last Modified On : 08-19-2014
// ***********************************************************************
// <copyright file="CRC8.cs" company="">
//     Copyright (c) . 余小章 . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

/// <summary>
/// The CRC namespace.
/// </summary>
namespace Tako.CRC
{
    /// <summary>
    /// Class CRC8.
    /// </summary>
    internal class CRC8Provider : CRCProviderBase
    {
        /// <summary>
        /// The _CRC table
        /// </summary>
        private uint[] _crcTable = new uint[256];

        /// <summary>
        /// The _polynomial
        /// </summary>
        private uint _polynomial = 0xd5;

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
        /// Initializes a new instance of the <see cref="CRC8Provider"/> class.
        /// </summary>
        /// <param name="Polynomial">The polynomial.</param>
        public CRC8Provider(uint Polynomial = 0xd5)
        {
            this.Polynomial = Polynomial;

            for (int i = 0; i < 256; ++i)
            {
                int curr = i;

                for (int j = 0; j < 8; ++j)
                {
                    if ((curr & 0x80) != 0)
                    {
                        curr = (curr << 1) ^ (byte)this.Polynomial;
                    }
                    else
                    {
                        curr <<= 1;
                    }
                }

                this.CRCTable[i] = (byte)curr;
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
            byte crc = 0;

            foreach (byte b in OriginalArray)
            {
                crc = (byte)this.CRCTable[crc ^ b];
            }

            byte[] crcArray = new byte[1] { crc };

            base.GetCRCStatus(ref status, crc, crcArray, OriginalArray);
            return status;
        }
    }
}