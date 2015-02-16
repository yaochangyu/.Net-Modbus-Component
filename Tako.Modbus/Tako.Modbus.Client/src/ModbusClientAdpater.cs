// ***********************************************************************
// Assembly         : Tako.Modbus.Client
// Author           : 余小章
// Created          : 08-19-2014
//
// Last Modified By : 余小章
// Last Modified On : 08-19-2014
// ***********************************************************************
// <copyright file="ModbusClientAdpater.cs" company="">
//     Copyright (c) . 余小章. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using Tako.Modbus.Core;

/// <summary>
/// The Client namespace.
/// </summary>
namespace Tako.Modbus.Client
{
    /// <summary>
    /// Class ModbusClientAdpater.
    /// </summary>
    public class ModbusClientAdpater
    {
        /// <summary>
        /// Gets or sets the modbus client.
        /// </summary>
        /// <value>The modbus client.</value>
        public virtual ModbusClientBase ModbusClient
        {
            get;
            set;
        }

        /// <summary>
        /// Creates the modbus client.
        /// </summary>
        /// <param name="EnumModbusFrame">The enum modbus frame.</param>
        /// <returns>AbsModbusClient.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">EnumModbusFrame</exception>
        public virtual ModbusClientBase CreateModbusClient(EnumModbusFraming EnumModbusFrame)
        {
            switch (EnumModbusFrame)
            {
                case EnumModbusFraming.TCP:
                    this.ModbusClient = new TcpModbusClient();
                    break;

                case EnumModbusFraming.RTU:
                    this.ModbusClient = new RtuModbusClient();
                    break;

                case EnumModbusFraming.ASCII:
                    this.ModbusClient = new AsciiModbusClient();
                    break;

                default:
                    throw new ArgumentOutOfRangeException("EnumModbusFrame");
            }

            return this.ModbusClient;
        }
    }
}