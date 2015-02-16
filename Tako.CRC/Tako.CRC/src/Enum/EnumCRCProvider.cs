// ***********************************************************************
// Assembly         : Tako.CRC
// Author           : 余小章
// Created          : 08-19-2014
//
// Last Modified By : 余小章
// Last Modified On : 08-19-2014
// ***********************************************************************
// <copyright file="EnumCRCProvider.cs" company="">
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
    /// Enum EnumCRCProvider
    /// </summary>
    public enum EnumCRCProvider
    {
        /// <summary>
        /// The CRC16
        /// </summary>
        CRC16,

        /// <summary>
        /// The CRC16 ccit T_0X0000
        /// </summary>
        CRC16CCITT_0x0000,

        /// <summary>
        /// The CRC16 ccit T_0X FFFF
        /// </summary>
        CRC16CCITT_0xFFFF,

        /// <summary>
        /// The CRC16 ccit T_0X1 d0 f
        /// </summary>
        CRC16CCITT_0x1D0F,

        /// <summary>
        /// The CRC16 kermit
        /// </summary>
        CRC16Kermit,

        /// <summary>
        /// The CRC16 modbus
        /// </summary>
        CRC16Modbus,

        /// <summary>
        /// The CRC32
        /// </summary>
        CRC32,

        /// <summary>
        /// The CRC8
        /// </summary>
        CRC8,

        /// <summary>
        /// The CRC8 ccitt
        /// </summary>
        CRC8CCITT,

        /// <summary>
        /// The CRC8 dallasmaxim
        /// </summary>
        CRC8DALLASMAXIM,

        /// <summary>
        /// The CRC8 SAEJ1850
        /// </summary>
        CRC8SAEJ1850,

        /// <summary>
        /// The CRC8 wcdma
        /// </summary>
        CRC8WCDMA
    }
}