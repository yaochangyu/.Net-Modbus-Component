// ***********************************************************************
// Assembly         : Tako.Modbus.Client
// Author           : 余小章
// Created          : 08-19-2014
//
// Last Modified By : 余小章
// Last Modified On : 08-27-2014
// ***********************************************************************
// <copyright file="RtuModbusClient.cs" company="">
//     Copyright (c) . 余小章. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.IO.Ports;
using Tako.Modbus.Core;

/// <summary>
/// The Client namespace.
/// </summary>
namespace Tako.Modbus.Client
{
    using System;
    using System.IO;
    using System.Threading;

    /// <summary>
    /// Class RtuModbusClient.
    /// </summary>
    public class RtuModbusClient : ModbusClientBase
    {
        /// <summary>
        /// The _modbus request
        /// </summary>
        private ModbusRequestBase _modbusRequest = new RtuModbusRequest();

        /// <summary>
        /// The _modbus response
        /// </summary>
        private ModbusResponseBase _modbusResponse = new RtuModbusResponse();

        /// <summary>
        /// The _modbus data convert
        /// </summary>
        private ModbusDataConvertBase _modbusDataConvert = new HexModbusDataConvert();

        /// <summary>
        /// Initializes a new instance of the <see cref="RtuModbusClient"/> class.
        /// </summary>
        public RtuModbusClient()
        {
            if (this.ModbusSocket == null)
            {
                this.ModbusSocket = new RtuModbusSocket();
            }
        }

        /// <summary>
        /// Gets or sets the modbus serial port.
        /// </summary>
        /// <value>The modbus serial port.</value>
        public IModbusSocket ModbusSocket { get; set; }

        /// <summary>
        /// Connects the specified connect configuration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ConnectConfig">The connect configuration.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="System.ArgumentNullException">ConnectConfig</exception>
        /// <exception cref="System.NotSupportedException">ConnectConfig</exception>
        public override bool Connect<T>(T ConnectConfig)
        {
            return this.ModbusSocket.Connect<T>(ConnectConfig);
        }

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

        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool Disconnect()
        {
            return this.ModbusSocket.Disconnect();
        }

        /// <summary>
        /// Receives this instance.
        /// </summary>
        /// <returns>System.Byte[].</returns>
        /// <exception cref="ModbusException">No Connect
        /// or
        /// Receive Timeout</exception>
        public override byte[] Receive()
        {
            return this.ModbusSocket.Receive();
        }

        /// <summary>
        /// Sends the specified request array.
        /// </summary>
        /// <param name="RequestArray">The request array.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override int Send(byte[] RequestArray)
        {
            return this.ModbusSocket.Send(RequestArray);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public override void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="RtuModbusClient" /> class.
        /// </summary>
        ~RtuModbusClient()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.Disposed)
                return;

            if (disposing)
            {
                //clean management resource
                if (this.ModbusSocket != null)
                {
                    this.ModbusSocket.Dispose();
                    this.ModbusSocket = null;
                }
            }

            //clean unmanagement resource

            //change flag
            this.Disposed = true;
        }
    }
}