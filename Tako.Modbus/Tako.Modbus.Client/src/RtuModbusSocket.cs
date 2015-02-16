// ***********************************************************************
// Assembly         : Tako.Modbus.Client
// Author           : S01YAO
// Created          : 08-27-2014
//
// Last Modified By : S01YAO
// Last Modified On : 08-27-2014
// ***********************************************************************
// <copyright file="RtuModbusSocket.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using Tako.Modbus.Core;

/// <summary>
/// The Client namespace.
/// </summary>
namespace Tako.Modbus.Client
{
    /// <summary>
    /// Class RtuModbusSocket.
    /// </summary>
    public class RtuModbusSocket : IModbusSocket
    {
        /// <summary>
        /// The _disposed
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// The _serial port
        /// </summary>
        private SerialPort _serialPort = null;

        /// <summary>
        /// The _is connected
        /// </summary>
        private bool _isConnected;

        /// <summary>
        /// The _send timeout
        /// </summary>
        private int _sendTimeout = 1000;

        /// <summary>
        /// The _receive timeout
        /// </summary>
        private int _receiveTimeout = 1000;

        /// <summary>
        /// The _retry time interval
        /// </summary>
        private int _retryTimeInterval = 100;

        /// <summary>
        /// The _retry time
        /// </summary>
        private int _retryTime = 10;

        /// <summary>
        /// Gets or sets the send timeout.
        /// </summary>
        /// <value>The send timeout.</value>
        public int SendTimeout
        {
            get { return _sendTimeout; }
            set { _sendTimeout = value; }
        }

        /// <summary>
        /// Gets or sets the receive timeout.
        /// </summary>
        /// <value>The receive timeout.</value>
        public int ReceiveTimeout
        {
            get { return _receiveTimeout; }
            set { _receiveTimeout = value; }
        }

        /// <summary>
        /// Gets or sets the retry time interval.
        /// </summary>
        /// <value>The retry time interval.</value>
        public int RetryTimeInterval
        {
            get { return _retryTimeInterval; }
            set { _retryTimeInterval = value; }
        }

        /// <summary>
        /// Gets or sets the retry time.
        /// </summary>
        /// <value>The retry time.</value>
        public int RetryTime
        {
            get { return _retryTime; }
            set { _retryTime = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is connected.
        /// </summary>
        /// <value><c>true</c> if this instance is connected; otherwise, <c>false</c>.</value>
        public bool IsConnected
        {
            get
            {
                this._isConnected = this._serialPort.IsOpen;
                return _isConnected;
            }
            set { _isConnected = value; }
        }

        /// <summary>
        /// Connects the specified connect configuration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ConnectConfig">The connect configuration.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="System.ArgumentNullException">ConnectConfig</exception>
        /// <exception cref="System.NotSupportedException">ConnectConfig</exception>
        public bool Connect<T>(T ConnectConfig)
        {
            if (ConnectConfig == null)
            {
                throw new ArgumentNullException("ConnectConfig");
            }

            SerialModbusConnectConifg connectConfig = ConnectConfig as SerialModbusConnectConifg;
            if (connectConfig == null)
            {
                throw new NotSupportedException("ConnectConfig");
            }
            if (this._serialPort == null)
            {
                this._serialPort = new SerialPort(connectConfig.PortName, connectConfig.BaudRate, connectConfig.Parity, connectConfig.DataBits, connectConfig.StopBits);
            }

            if (this._serialPort.IsOpen)
            {
                this._serialPort.Close();
                Thread.Sleep(100);
            }

            this._serialPort.Open();
            this.IsConnected = this._serialPort.IsOpen;
            return this.IsConnected;
        }

        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Disconnect()
        {
            if (!this._serialPort.IsOpen)
            {
                return false;
            }
            this._serialPort.Close();
            this.IsConnected = this._serialPort.IsOpen;
            return !this.IsConnected;
        }

        /// <summary>
        /// Sends the specified request array.
        /// </summary>
        /// <param name="RequestArray">The request array.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="ModbusException">No Connect</exception>
        public int Send(byte[] RequestArray)
        {
            if (!this._serialPort.IsOpen)
            {
                throw new ModbusException("No Connect");
            }
            this._serialPort.Write(RequestArray, 0, RequestArray.Length);
            return RequestArray.Length;
        }

        /// <summary>
        /// Receives this instance.
        /// </summary>
        /// <returns>System.Byte[].</returns>
        /// <exception cref="ModbusException">
        /// No Connect
        /// or
        /// Receive Timeout
        /// </exception>
        public byte[] Receive()
        {
            if (!this._serialPort.IsOpen)
            {
                throw new ModbusException("No Connect");
            }
            var timeoutTemp = this._serialPort.ReadTimeout;
            this._serialPort.ReadTimeout = this.ReceiveTimeout;
            byte[] bufferArray = new byte[256];
            byte[] resultArray = null;
            var retryTime = 0;

            using (MemoryStream stream = new MemoryStream())
            {
                while (true)
                {
                    if (this._serialPort.BytesToRead > 0)
                    {
                        var receiveCount = this._serialPort.Read(bufferArray, 0, bufferArray.Length);
                        stream.Write(bufferArray, 0, receiveCount);
                        resultArray = stream.ToArray();
                        if (receiveCount <= 0)
                        {
                            break;
                        }

                        retryTime = 0;
                    }
                    retryTime++;
                    if (retryTime > this.RetryTime)
                    {
                        break;
                    }

                    //空轉
                    SpinWait.SpinUntil(() => retryTime > this.RetryTime, this.RetryTimeInterval);
                }
            }

            if (resultArray == null || resultArray.Length == 0)
            {
                throw new ModbusException("Receive Timeout");
            }
            this._serialPort.ReadTimeout = timeoutTemp;
            return resultArray;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="RtuModbusSocket"/> class.
        /// </summary>
        ~RtuModbusSocket()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this._disposed)
                return;

            this.IsConnected = false;

            if (disposing)
            {
                //clean management resource
                if (this._serialPort != null)
                {
                    this._serialPort.Dispose();
                    this._serialPort = null;
                }
            }

            //clean unmanagement resource

            //change flag
            this._disposed = true;
        }
    }
}