using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Tako.Modbus.Core;

namespace Tako.Modbus.Client
{
    public class TcpModbusSocket : IModbusSocket
    {
        private bool _disposed;
        private Socket _socket;
        private bool _isConnected;
        private int _sendTimeout = 1000;
        private int _receiveTimeout = 1000;
        private int _retryTimeInterval = 100;
        private int _retryTime = 10;

        public int SendTimeout
        {
            get { return _sendTimeout; }
            set { _sendTimeout = value; }
        }

        public int ReceiveTimeout
        {
            get { return _receiveTimeout; }
            set { _receiveTimeout = value; }
        }

        public int RetryTimeInterval
        {
            get { return _retryTimeInterval; }
            set { _retryTimeInterval = value; }
        }

        public int RetryTime
        {
            get { return _retryTime; }
            set { _retryTime = value; }
        }

        public bool IsConnected
        {
            get
            {
                this._isConnected = this._socket.Connected;
                return _isConnected;
            }
            set { _isConnected = value; }
        }

        public bool Connect<T>(T ConnectConfig)
        {
            if (ConnectConfig == null)
            {
                throw new ArgumentNullException("ConnectConfig");
            }

            TcpModbusConnectConfig connectConfig = ConnectConfig as TcpModbusConnectConfig;
            if (connectConfig == null)
            {
                throw new NotSupportedException("ConnectConfig");
            }
            if (this._socket == null)
            {
                this._socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }

            var ipEndPoint = new IPEndPoint(IPAddress.Parse(connectConfig.IpAddress), connectConfig.Port);

            this._socket.Connect((EndPoint)ipEndPoint);
            return this._socket.Connected;
        }

        public bool Disconnect()
        {
            if (!this._socket.Connected)
            {
                return false;
            }

            this._socket.Shutdown(SocketShutdown.Both);
            this._socket.Disconnect(false);
            this.IsConnected = this._socket.Connected;
            return !this.IsConnected;
        }

        public int Send(byte[] RequestArray)
        {
            return this._socket.Send(RequestArray);
        }

        public byte[] Receive()
        {
            var tempTimeOut = this._socket.ReceiveTimeout;
            this._socket.ReceiveTimeout = this.ReceiveTimeout;

            var bufferArray = new byte[256];
            var socketError = new SocketError();
            byte[] responseArray = null;

            var retrTime = 0;

            using (MemoryStream memory = new MemoryStream())
            {
                while (true)
                {
                    if (this._socket.Available > 0)
                    {
                        var receiveCount = this._socket.Receive(bufferArray, 0, bufferArray.Length, SocketFlags.None, out socketError);
                        if (receiveCount == 0 || socketError != SocketError.Success)
                        {
                            break;
                        }
                        memory.Write(bufferArray, 0, receiveCount);

                        retrTime = 0;
                    }

                    retrTime++;

                    if (retrTime > this.RetryTime)
                    {
                        break;
                    }

                    //空轉
                    SpinWait.SpinUntil(() => retrTime > this.RetryTime, this.RetryTimeInterval);
                }

                this._socket.ReceiveTimeout = tempTimeOut;
                responseArray = memory.ToArray();
            }

            if (responseArray == null || responseArray.Length == 0)
            {
                throw new ModbusException("Receive reponse timeout");
            }
            return responseArray;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="TcpModbusClient"/> class.
        /// </summary>
        ~TcpModbusSocket()
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

            if (disposing)
            {
                //clean management resource
                if (this._socket != null)
                {
                    this._socket.Shutdown(SocketShutdown.Both);
                    this._socket.Disconnect(false);
                    this._socket.Dispose();
                    this._socket = null;
                }
            }

            //clean unmanagement resource

            //change flag
            this._disposed = true;
        }
    }
}