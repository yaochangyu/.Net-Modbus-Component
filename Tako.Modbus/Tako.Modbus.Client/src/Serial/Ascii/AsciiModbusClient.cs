// ***********************************************************************
// Assembly         : Tako.Modbus.Client
// Author           : 余小章
// Created          : 08-19-2014
//
// Last Modified By : 余小章
// Last Modified On : 08-19-2014
// ***********************************************************************
// <copyright file="AsciiModbusClient.cs" company="">
//     Copyright (c) . 余小章. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

/// <summary>
/// The Client namespace.
/// </summary>
namespace Tako.Modbus.Client
{
    /// <summary>
    /// Class AsciiModbusClient.
    /// </summary>
    public class AsciiModbusClient : RtuModbusClient
    {
        /// <summary>
        /// The _modbus request
        /// </summary>
        private ModbusRequestBase _modbusRequest = new AsciiModbusRequest();

        /// <summary>
        /// The _modbus response
        /// </summary>
        private ModbusResponseBase _modbusResponse = new AsciiModbusResponse();

        /// <summary>
        /// The _modbus data convert
        /// </summary>
        private ModbusDataConvertBase _modbusDataConvert = new AsciiModbusDataConvert();

        /// <summary>
        /// Gets or sets the modbus request.
        /// </summary>
        /// <value>The modbus request.</value>
        internal override ModbusRequestBase ModbusRequest
        {
            get { return _modbusRequest; }
            set { _modbusRequest = value; }
        }

        /// <summary>
        /// Gets or sets the modbus response.
        /// </summary>
        /// <value>The modbus response.</value>
        internal override ModbusResponseBase ModbusResponse
        {
            get { return _modbusResponse; }
            set { _modbusResponse = value; }
        }

        /// <summary>
        /// Gets or sets the modbus data convert.
        /// </summary>
        /// <value>The modbus data convert.</value>
        internal override ModbusDataConvertBase ModbusDataConvert
        {
            get { return _modbusDataConvert; }
            set { _modbusDataConvert = value; }
        }
    }
}