// ***********************************************************************
// Assembly         : Tako.Modbus.Client
// Author           : 余小章
// Created          : 08-19-2014
//
// Last Modified By : 余小章
// Last Modified On : 08-19-2014
// ***********************************************************************
// <copyright file="TcpModbusConnectConfig.cs" company="">
//     Copyright (c) . 余小章. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel;
using System.Net;

/// <summary>
/// The Client namespace.
/// </summary>
namespace Tako.Modbus.Client
{
    /// <summary>
    /// Class TcpModbusConnectConfig.
    /// </summary>
    [Serializable]
    public class TcpModbusConnectConfig : INotifyPropertyChanged
    {
        /// <summary>
        /// The _ip address
        /// </summary>
        private string _ipAddress = "127.0.0.1";

        /// <summary>
        /// The _port
        /// </summary>
        private int _port = 502;

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
        /// Gets or sets the ip address.
        /// </summary>
        /// <value>The ip address.</value>
        /// <exception cref="System.ArgumentNullException"></exception>
        public string IpAddress
        {
            get { return _ipAddress; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException();
                }
                IPAddress result;
                if (IPAddress.TryParse(value, out result))
                {
                    _ipAddress = value;
                }
                OnPropertyChanged("IpAddress");
            }
        }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>The port.</value>
        public int Port
        {
            get { return _port; }
            set
            {
                _port = value;
                OnPropertyChanged("Port");
            }
        }
    }
}