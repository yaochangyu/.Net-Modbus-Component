// ***********************************************************************
// Assembly         : Tako.Modbus.Client
// Author           : 余小章
// Created          : 08-19-2014
//
// Last Modified By : 余小章
// Last Modified On : 08-19-2014
// ***********************************************************************
// <copyright file="AsciiModbusRequest.cs" company="">
//     Copyright (c) . 余小章. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.IO;
using System.Text;
using Tako.Modbus.Core;

/// <summary>
/// The Client namespace.
/// </summary>
namespace Tako.Modbus.Client
{
    /// <summary>
    /// Class AsciiModbusRequest.
    /// </summary>
    internal class AsciiModbusRequest : ModbusRequestBase
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
                memory.WriteByte(unit);
                memory.WriteByte((byte)FunctionCode);
                memory.Write(Parameters, 0, Parameters.Length);

                var aduArray = toAsciiData(memory.ToArray());
                return aduArray;
            }
        }

        /// <summary>
        /// To the ASCII data.
        /// </summary>
        /// <param name="OriginalArray">The original array.</param>
        /// <returns>System.Byte[].</returns>
        private byte[] toAsciiData(byte[] OriginalArray)
        {
            var lrc = ModbusUtility.CalculateLRC(OriginalArray);
            var pdu = ModbusUtility.BytesToHexString(OriginalArray);
            var adu = Encoding.ASCII.GetBytes(ModbusUtility.ASCII_START_SYMBOL + pdu + lrc + ModbusUtility.ASCII_END_SYMBOL);
            return adu;
        }
    }
}