// ***********************************************************************
// Assembly         : Tako.Modbus.Core
// Author           : 余小章
// Created          : 08-19-2014
//
// Last Modified By : 余小章
// Last Modified On : 08-19-2014
// ***********************************************************************
// <copyright file="EnumModbusIntegralUnit.cs" company="">
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
    /// Enum EnumModbusIntegralUnit
    /// </summary>
    public enum EnumModbusIntegralUnit : byte
    {
        /// <summary>
        /// The byte
        /// </summary>
        Byte = 1, Word = 2, DWord = 4, QWord = 8
    }
}