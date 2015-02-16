// ***********************************************************************
// Assembly         : Tako.Modbus.Client
// Author           : 余小章
// Created          : 08-19-2014
//
// Last Modified By : 余小章
// Last Modified On : 08-19-2014
// ***********************************************************************
// <copyright file="TcpModbusResponse.cs" company="">
//     Copyright (c) . 余小章. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

/// <summary>
/// The Client namespace.
/// </summary>
namespace Tako.Modbus.Client
{
    /// <summary>
    /// Class TcpModbusResponse.
    /// </summary>
    internal class TcpModbusResponse : ModbusResponseBase
    {
        /// <summary>
        /// The _function code position
        /// </summary>
        private byte _functionCodePosition = 7;

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
        protected override void CheckDataValidate(byte[] ResponseArray)
        {
        }

        /// <summary>
        /// Gets the result array.
        /// </summary>
        /// <param name="ResponseArray">The response array.</param>
        /// <returns>System.Byte[].</returns>
        protected override byte[] GetResultArray(byte[] ResponseArray)
        {
            //get result data
            var counterPosition = this.FunctionCodePosition + 1;
            var counter = ResponseArray[counterPosition];
            var resultArray = new byte[counter];
            Array.Copy(ResponseArray, counterPosition + 1, resultArray, 0, counter);
            return resultArray;
        }
    }
}