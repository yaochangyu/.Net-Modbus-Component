// ***********************************************************************
// Assembly         : Tako.Modbus.Client
// Author           : 余小章
// Created          : 08-19-2014
//
// Last Modified By : 余小章
// Last Modified On : 08-19-2014
// ***********************************************************************
// <copyright file="AsciiModbusResponse.cs" company="">
//     Copyright (c) . 余小章. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.IO;
using System.Linq;
using System.Text;
using Tako.Modbus.Core;

/// <summary>
/// The Client namespace.
/// </summary>
namespace Tako.Modbus.Client
{
    /// <summary>
    /// Class AsciiModbusResponse.
    /// </summary>
    internal class AsciiModbusResponse : ModbusResponseBase
    {
        /// <summary>
        /// The _function code position
        /// </summary>
        private byte _functionCodePosition = 3;

        /// <summary>
        /// Gets or sets the function code position.
        /// </summary>
        /// <value>The function code position.</value>
        protected override byte FunctionCodePosition
        {
            get { return _functionCodePosition; }
            set { _functionCodePosition = value; }
        }

        /// <summary>
        /// Exceptions the validate.
        /// </summary>
        /// <param name="ResponseArray">The response array.</param>
        /// <exception cref="ModbusException">No Response or Data Fail</exception>
        protected override void ExceptionValidate(byte[] ResponseArray)
        {
            if (ResponseArray == null | ResponseArray.Length <= this.FunctionCodePosition)
            {
                throw new ModbusException("No Response or Data Fail");
            }

            var functionCodeArray = new byte[2];
            Array.Copy(ResponseArray, this.FunctionCodePosition, functionCodeArray, 0, functionCodeArray.Length);
            var functionCode = Utility.HexStringToBytes(Encoding.ASCII.GetString(functionCodeArray))[0];

            EnumModbusFunctionCode resultCode;
            if (!EnumModbusFunctionCode.TryParse(functionCode.ToString(), out resultCode))
            {
                throw ModbusException.GetModbusException(0x01);
            }

            if (functionCode >= 80)
            {
                var errorCodeArray = new byte[2];
                Array.Copy(ResponseArray, this.FunctionCodePosition + 2, errorCodeArray, 0, errorCodeArray.Length);

                var exceptionCode = byte.Parse(Encoding.ASCII.GetString(errorCodeArray));
                throw ModbusException.GetModbusException(exceptionCode);
            }
        }

        /// <summary>
        /// Functions the code validate.
        /// </summary>
        /// <param name="RequestArray">The request array.</param>
        /// <param name="ResponseArray">The response array.</param>
        /// <param name="FunctionCodeFlag">The function code flag.</param>
        protected override void FunctionCodeValidate(byte[] RequestArray, byte[] ResponseArray, EnumModbusFunctionCode FunctionCodeFlag)
        {
            var resFunctionCodeArray = new byte[2];
            Array.Copy(ResponseArray, FunctionCodePosition, resFunctionCodeArray, 0, resFunctionCodeArray.Length);
            byte resFunctionCode = Utility.HexStringToBytes(Encoding.ASCII.GetString(resFunctionCodeArray))[0];

            EnumModbusFunctionCode resFunctionCodeMode;
            if (!EnumModbusFunctionCode.TryParse(resFunctionCode.ToString(), out resFunctionCodeMode))
            {
                throw ModbusException.GetModbusException(0x01);
            }

            var reqFunctionCodeArray = new byte[2];
            Array.Copy(RequestArray, FunctionCodePosition, reqFunctionCodeArray, 0, reqFunctionCodeArray.Length);
            byte reqFunctionCode = Utility.HexStringToBytes(Encoding.ASCII.GetString(reqFunctionCodeArray))[0];

            EnumModbusFunctionCode reqFunctionCodeMode;
            if (!EnumModbusFunctionCode.TryParse(reqFunctionCode.ToString(), out reqFunctionCodeMode))
            {
                throw ModbusException.GetModbusException(0x01);
            }

            if ((byte)FunctionCodeFlag != resFunctionCode)
            {
                throw ModbusException.GetModbusException(0x01);
            }
            if ((byte)FunctionCodeFlag != reqFunctionCode)
            {
                throw ModbusException.GetModbusException(0x01);
            }
        }

        /// <summary>
        /// Checks the data validate.
        /// </summary>
        /// <param name="ResponseArray">The response array.</param>
        /// <exception cref="ModbusException">
        /// Start Symbol Format Fail
        /// or
        /// End Symbol Format Fail
        /// or
        /// LRC Validate Fail
        /// </exception>
        protected override void CheckDataValidate(byte[] ResponseArray)
        {
            //start symbol
            var startSymbolArrayFlag = Encoding.ASCII.GetBytes(ModbusUtility.ASCII_START_SYMBOL);
            if (ResponseArray[0] != startSymbolArrayFlag[0])
            {
                throw new ModbusException("Start Symbol Format Fail");
            }

            //end symbol
            var endSymbolArrayFlag = Encoding.ASCII.GetBytes(ModbusUtility.ASCII_END_SYMBOL);
            var endSymbolArray = new byte[2];
            Array.Copy(ResponseArray, ResponseArray.Length - 2, endSymbolArray, 0, endSymbolArray.Length);
            if (!endSymbolArrayFlag.SequenceEqual(endSymbolArray))
            {
                throw new ModbusException("End Symbol Format Fail");
            }

            // lrc validate
            var sourceLrcArray = new byte[2];
            Array.Copy(ResponseArray, ResponseArray.Length - 4, sourceLrcArray, 0, sourceLrcArray.Length);

            byte[] sourceDataArray = null;
            using (MemoryStream memory = new MemoryStream())
            {
                for (int i = 1; i < ResponseArray.Length - 4; i++)
                {
                    memory.WriteByte(ResponseArray[i]);
                }
                sourceDataArray = memory.ToArray();
            }
            var sourceDataHex = Encoding.ASCII.GetString(sourceDataArray);
            var hexArray = Utility.HexStringToBytes(sourceDataHex);
            var destinationLrcHex = Utility.CalculateLRC(hexArray);
            var destinationLrcArray = Encoding.ASCII.GetBytes(destinationLrcHex);
            if (!sourceLrcArray.SequenceEqual(destinationLrcArray))
            {
                throw new ModbusException("LRC Validate Fail");
            }
        }

        /// <summary>
        /// Gets the result array.
        /// </summary>
        /// <param name="ResponseArray">The response array.</param>
        /// <returns>System.Byte[].</returns>
        protected override byte[] GetResultArray(byte[] ResponseArray)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                memory.Write(ResponseArray, 7, ResponseArray.Length - 7 - 4);
                var resultArray = memory.ToArray();
                return resultArray;
            }
        }
    }
}