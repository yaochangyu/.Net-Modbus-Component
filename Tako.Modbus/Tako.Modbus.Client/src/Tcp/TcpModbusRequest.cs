// ***********************************************************************
// Assembly         : Tako.Modbus.Client
// Author           : 余小章
// Created          : 08-19-2014
//
// Last Modified By : 余小章
// Last Modified On : 08-27-2014
// ***********************************************************************
// <copyright file="TcpModbusRequest.cs" company="">
//     Copyright (c) . 余小章. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.IO;
using Tako.Modbus.Core;

/// <summary>
/// The Client namespace.
/// </summary>
namespace Tako.Modbus.Client
{
    /// <summary>
    /// Class TcpModbusRequest.
    /// </summary>
    internal class TcpModbusRequest : ModbusRequestBase
    {
        /// <summary>
        /// The modbu s_ protocol
        /// </summary>
        private readonly ushort MODBUS_PROTOCOL = 0;

        /// <summary>
        /// The modbu s_ defaul t_ length
        /// </summary>
        private readonly ushort MODBUS_DEFAULT_LENGTH = 6;

        /// <summary>
        /// Creates the request message.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="functionCode">The function code.</param>
        /// <param name="multiOutputLength">Length of the multi output.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>System.Byte[].</returns>
        protected override byte[] CreateRequestMessage(byte unit, EnumModbusFunctionCode functionCode, byte? multiOutputLength, params byte[] parameters)
        {
            ushort dataLength = 0;
            if (multiOutputLength == null)
            {
                dataLength = MODBUS_DEFAULT_LENGTH;
            }
            else
            {
                dataLength = (ushort)(MODBUS_DEFAULT_LENGTH + multiOutputLength + 1);
            }
            using (MemoryStream memory = new MemoryStream())
            {
                memory.WriteByte((byte)(this.TransactionId >> 8));
                memory.WriteByte((byte)this.TransactionId);
                memory.WriteByte((byte)(MODBUS_PROTOCOL >> 8));
                memory.WriteByte((byte)MODBUS_PROTOCOL);
                memory.WriteByte((byte)(dataLength >> 8));
                memory.WriteByte((byte)dataLength);
                memory.WriteByte((byte)unit);
                memory.WriteByte((byte)functionCode);
                memory.Write(parameters, 0, parameters.Length);

                return memory.ToArray();
            }
        }
    }
}