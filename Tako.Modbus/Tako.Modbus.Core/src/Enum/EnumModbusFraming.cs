// ***********************************************************************
// Assembly         : Tako.Modbus.Core
// Author           : 余小章
// Created          : 08-19-2014
//
// Last Modified By : 余小章
// Last Modified On : 08-19-2014
// ***********************************************************************
// <copyright file="EnumModbusFraming.cs" company="">
//     Copyright (c) . 余小章 . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

/// <summary>
/// The Modbus namespace.
/// </summary>
namespace Tako.Modbus.Core
{
    /// <summary>
    /// Enum EnumModbusFraming
    /// </summary>
    public enum EnumModbusFraming : byte
    {
        /// <summary>
        /// The TCP
        /// </summary>
        TCP = 0,

        /// <summary>
        /// The rtu
        /// </summary>
        RTU = 1,

        /// <summary>
        /// The ASCII
        /// </summary>
        ASCII = 2,
    }
}