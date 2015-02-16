// ***********************************************************************
// Assembly         : Tako.CRC
// Author           : 余小章
// Created          : 08-19-2014
//
// Last Modified By : 余小章
// Last Modified On : 08-19-2014
// ***********************************************************************
// <copyright file="CRCStatus.cs" company="">
//     Copyright (c) . 余小章 . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel;

/// <summary>
/// The CRC namespace.
/// </summary>
namespace Tako.CRC
{
    /// <summary>
    /// Class CRCStatus.
    /// </summary>
    [Serializable]
    public class CRCStatus : INotifyPropertyChanged
    {
        /// <summary>
        /// The _full data array
        /// </summary>
        private byte[] _fullDataArray;

        /// <summary>
        /// The _CRC decimal
        /// </summary>
        private uint _crcDecimal;

        /// <summary>
        /// The _CRC hexadecimal
        /// </summary>
        private string _crcHexadecimal;

        /// <summary>
        /// The _CRC array
        /// </summary>
        private byte[] _crcArray;

        /// <summary>
        /// The _full data hexadecimal
        /// </summary>
        private string _fullDataHexadecimal;

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
        /// Gets or sets the CRC hexadecimal.
        /// </summary>
        /// <value>The CRC hexadecimal.</value>
        public string CrcHexadecimal
        {
            get { return _crcHexadecimal; }
            set
            {
                _crcHexadecimal = value;
                OnPropertyChanged("CrcHexadecimal");
            }
        }

        /// <summary>
        /// Gets or sets the CRC decimal.
        /// </summary>
        /// <value>The CRC decimal.</value>
        public uint CrcDecimal
        {
            get { return _crcDecimal; }
            set
            {
                _crcDecimal = value;
                OnPropertyChanged("CrcDecimal");
            }
        }

        /// <summary>
        /// Gets or sets the CRC array.
        /// </summary>
        /// <value>The CRC array.</value>
        public byte[] CrcArray
        {
            get { return _crcArray; }
            set
            {
                _crcArray = value;
                OnPropertyChanged("CrcArray");
            }
        }

        /// <summary>
        /// Gets the full data array.
        /// </summary>
        /// <value>The full data array.</value>
        public byte[] FullDataArray
        {
            get { return _fullDataArray; }
            internal set
            {
                _fullDataArray = value;
                OnPropertyChanged("FullDataArray");
            }
        }

        /// <summary>
        /// Gets or sets the full data hexadecimal.
        /// </summary>
        /// <value>The full data hexadecimal.</value>
        public string FullDataHexadecimal
        {
            get { return _fullDataHexadecimal; }
            set
            {
                _fullDataHexadecimal = value;
                OnPropertyChanged("Hexadecimal");
            }
        }
    }
}