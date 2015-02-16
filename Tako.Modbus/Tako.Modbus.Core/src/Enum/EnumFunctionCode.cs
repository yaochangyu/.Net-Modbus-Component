// ***********************************************************************
// Assembly         : Tako.Modbus.Core
// Author           : 余小章
// Created          : 08-19-2014
//
// Last Modified By : 余小章
// Last Modified On : 08-19-2014
// ***********************************************************************
// <copyright file="EnumFunctionCode.cs" company="">
//     Copyright (c) . 余小章 . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

/// <summary>
/// The Modbus namespace.
/// </summary>

namespace Tako.Modbus.Core
{
    /// <summary>
    /// Enum EnumModbusFunctionCode
    /// </summary>
    [Serializable]
    public enum EnumModbusFunctionCode : byte
    {
        /// <summary>
        /// The read coils
        /// </summary>
        ReadCoils = 1,

        /// <summary>
        /// The read discrete inputs
        /// </summary>
        ReadDiscreteInputs = 2,

        /// <summary>
        /// The read holding registers
        /// </summary>
        ReadHoldingRegisters = 3,

        /// <summary>
        /// The read input registers
        /// </summary>
        ReadInputRegisters = 4,

        /// <summary>
        /// The write single coil
        /// </summary>
        WriteSingleCoil = 5,

        /// <summary>
        /// The write single register
        /// </summary>
        WriteSingleRegister = 6,

        /// <summary>
        /// The diagnostics
        /// </summary>
        Diagnostics = 8,

        /// <summary>
        /// The write multiple coils
        /// </summary>
        WriteMultipleCoils = 15,

        /// <summary>
        /// The write multiple registers
        /// </summary>
        WriteMultipleRegisters = 16,

        /// <summary>
        /// The read write multiple registers
        /// </summary>
        ReadWriteMultipleRegisters = 23,
    }
}