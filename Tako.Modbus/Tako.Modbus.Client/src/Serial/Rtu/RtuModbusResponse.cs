// ***********************************************************************
// Assembly         : Tako.Modbus.Client
// Author           : 余小章
// Created          : 08-19-2014
//
// Last Modified By : 余小章
// Last Modified On : 08-19-2014
// ***********************************************************************
// <copyright file="RtuModbusResponse.cs" company="">
//     Copyright (c) . 余小章. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using Tako.Modbus.Core;

/// <summary>
/// The Client namespace.
/// </summary>
namespace Tako.Modbus.Client
{
    /// <summary>
    /// Class RtuModbusResponse.
    /// </summary>
    internal class RtuModbusResponse : ModbusResponseBase
    {
        /// <summary>
        /// The _function code position
        /// </summary>
        private byte _functionCodePosition = 1;

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
        /// Checks the data validate.
        /// </summary>
        /// <param name="ResponseArray">The response array.</param>
        /// <exception cref="ModbusException">CRC Validate Fail</exception>
        protected override void CheckDataValidate(byte[] ResponseArray)
        {
            var sourceCrcArray = new byte[2];
            Array.Copy(ResponseArray, ResponseArray.Length - 2, sourceCrcArray, 0, sourceCrcArray.Length);
            var sourceDataArray = new byte[ResponseArray.Length - 2];
            Array.Copy(ResponseArray, 0, sourceDataArray, 0, sourceDataArray.Length);
            var destinationCrcArray = Utility.CalculateCRC(sourceDataArray);
            if (!sourceCrcArray.SequenceEqual(destinationCrcArray))
            {
                throw new ModbusException("CRC Validate Fail");
            }
        }

        /// <summary>
        /// Gets the result array.
        /// </summary>
        /// <param name="ResponseArray">The response array.</param>
        /// <returns>System.Byte[].</returns>
        protected override byte[] GetResultArray(byte[] ResponseArray)
        {
            var counterPosition = this.FunctionCodePosition + 1;
            var position = ResponseArray[counterPosition];
            var resultArray = new byte[position];
            Array.Copy(ResponseArray, counterPosition + 1, resultArray, 0, resultArray.Length);
            return resultArray;
        }
    }
}