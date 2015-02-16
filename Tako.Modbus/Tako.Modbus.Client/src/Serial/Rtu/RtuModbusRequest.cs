// ***********************************************************************
// Assembly         : Tako.Modbus.Client
// Author           : 余小章
// Created          : 08-19-2014
//
// Last Modified By : 余小章
// Last Modified On : 08-19-2014
// ***********************************************************************
// <copyright file="RtuModbusRequest.cs" company="">
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
    /// Class RtuModbusRequest.
    /// </summary>
    internal class RtuModbusRequest : ModbusRequestBase
    {
        /// <summary>
        /// Creates the request message.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="FunctionCode">The function code.</param>
        /// <param name="MultiOutputLength">Length of the multi output.</param>
        /// <param name="Parameters">The parameters.</param>
        /// <returns>System.Byte[].</returns>
        protected override byte[] CreateRequestMessage(byte unit, EnumModbusFunctionCode FunctionCode, byte? MultiOutputLength, params byte[] Parameters)
        {
            ushort dataLength = 0;
            if (MultiOutputLength == null)
            {
                dataLength = 0;
            }
            else
            {
                dataLength = (ushort)(MultiOutputLength + 1);
            }
            using (MemoryStream memory = new MemoryStream())
            {
                memory.WriteByte((byte)unit);
                memory.WriteByte((byte)FunctionCode);
                memory.Write(Parameters, 0, Parameters.Length);

                var crcArray = ModbusUtility.CalculateCRC(memory.ToArray());
                memory.Write(crcArray, 0, crcArray.Length);
                return memory.ToArray();
            }
        }
    }
}