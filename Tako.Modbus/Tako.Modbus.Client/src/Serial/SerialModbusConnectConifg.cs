// ***********************************************************************
// Assembly         : Tako.Modbus.Client
// Author           : 余小章
// Created          : 08-19-2014
//
// Last Modified By : 余小章
// Last Modified On : 08-19-2014
// ***********************************************************************
// <copyright file="SerialModbusConnectConifg.cs" company="">
//     Copyright (c) . 余小章. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel;
using System.IO.Ports;

/// <summary>
/// The Client namespace.
/// </summary>
namespace Tako.Modbus.Client
{
    /// <summary>
    /// Class SerialModbusConnectConifg.
    /// </summary>
    [Serializable]
    public class SerialModbusConnectConifg : INotifyPropertyChanged
    {
        /// <summary>
        /// The _port name
        /// </summary>
        private string _portName = "COM1";

        /// <summary>
        /// The _baud rate
        /// </summary>
        private int _baudRate = 115200;

        /// <summary>
        /// The _parity
        /// </summary>
        private Parity _parity = Parity.None;

        /// <summary>
        /// The _data bits
        /// </summary>
        private int _dataBits = 8;

        /// <summary>
        /// The _stop bits
        /// </summary>
        private StopBits _stopBits = StopBits.One;

        /// <summary>
        /// The _receive timeout
        /// </summary>
        private int _receiveTimeout;

        /// <summary>
        /// The _send timeout
        /// </summary>
        private int _sendTimeout;

        /// <summary>
        /// The _retry time
        /// </summary>
        private int _retryTime;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="name">The name.</param>
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        /// <summary>
        /// Gets or sets the name of the port.
        /// </summary>
        /// <value>The name of the port.</value>
        public virtual string PortName
        {
            get { return _portName; }
            set
            {
                _portName = value;
                OnPropertyChanged("PortName");
            }
        }

        /// <summary>
        /// Gets or sets the baud rate.
        /// </summary>
        /// <value>The baud rate.</value>
        public virtual int BaudRate
        {
            get { return _baudRate; }
            set
            {
                _baudRate = value;
                OnPropertyChanged("BaudRate");
            }
        }

        /// <summary>
        /// Gets or sets the parity.
        /// </summary>
        /// <value>The parity.</value>
        public virtual Parity Parity
        {
            get { return _parity; }
            set
            {
                _parity = value;
                OnPropertyChanged("Parity");
            }
        }

        /// <summary>
        /// Gets or sets the data bits.
        /// </summary>
        /// <value>The data bits.</value>
        public virtual int DataBits
        {
            get { return _dataBits; }
            set
            {
                _dataBits = value;
                OnPropertyChanged("DataBits");
            }
        }

        /// <summary>
        /// Gets or sets the stop bits.
        /// </summary>
        /// <value>The stop bits.</value>
        public virtual StopBits StopBits
        {
            get { return _stopBits; }
            set
            {
                _stopBits = value;
                OnPropertyChanged("StopBits");
            }
        }
    }
}