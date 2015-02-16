// ***********************************************************************
// Assembly         : Tako.Modbus.Client
// Author           : 余小章
// Created          : 08-19-2014
//
// Last Modified By : 余小章
// Last Modified On : 08-27-2014
// ***********************************************************************
// <copyright file="TcpModbusClient.cs" company="">
//     Copyright (c) . 余小章. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Tako.Modbus.Core;

/// <summary>
/// The Client namespace.
/// </summary>
namespace Tako.Modbus.Client
{
    /// <summary>
    /// Class TcpModbusClient.
    /// </summary>
    public class TcpModbusClient : ModbusClientBase
    {
        /// <summary>
        /// The _modbus request
        /// </summary>
        private ModbusRequestBase _modbusRequest = new TcpModbusRequest();

        /// <summary>
        /// The _modbus response
        /// </summary>
        private ModbusResponseBase _modbusResponse = new TcpModbusResponse();

        /// <summary>
        /// The _modbus data convert
        /// </summary>
        private ModbusDataConvertBase _modbusDataConvert = new HexModbusDataConvert();

        /// <summary>
        /// Gets or sets the modbus socket.
        /// </summary>
        /// <value>The modbus socket.</value>
        public IModbusSocket ModbusSocket { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TcpModbusClient"/> class.
        /// </summary>
        public TcpModbusClient()
        {
            if (this.ModbusSocket == null)
            {
                this.ModbusSocket = new TcpModbusSocket();
            }
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
        /// Connects the specified connect configuration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ConnectConfig">The connect configuration.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool Connect<T>(T ConnectConfig)
        {
            return this.ModbusSocket.Connect<T>(ConnectConfig);
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
        /// Sends the specified request array.
        /// </summary>
        /// <param name="RequestArray">The request array.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override int Send(byte[] RequestArray)
        {
            return this.ModbusSocket.Send(RequestArray);
        }

        /// <summary>
        /// Receives this instance.
        /// </summary>
        /// <returns>System.Byte[].</returns>
        /// <exception cref="ModbusException">Receive reponse timeout</exception>
        public override byte[] Receive()
        {
            return this.ModbusSocket.Receive();
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
        /// Finalizes an instance of the <see cref="TcpModbusClient" /> class.
        /// </summary>
        ~TcpModbusClient()
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
                    this.ModbusSocket.Disconnect();
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