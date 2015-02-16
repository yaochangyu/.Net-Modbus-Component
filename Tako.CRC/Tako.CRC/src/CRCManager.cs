// ***********************************************************************
// Assembly         : Tako.CRC
// Author           : 余小章
// Created          : 08-19-2014
//
// Last Modified By : 余小章
// Last Modified On : 08-19-2014
// ***********************************************************************
// <copyright file="CRCManager.cs" company="">
//     Copyright (c) . 余小章 . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

/// <summary>
/// The CRC namespace.
/// </summary>
namespace Tako.CRC
{
    /// <summary>
    /// Class CRCManager.
    /// </summary>
    public class CRCManager
    {
        /// <summary>
        /// The _data format
        /// </summary>
        private EnumOriginalDataFormat _dataFormat = EnumOriginalDataFormat.HEX;

        //property
        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        /// <value>The provider.</value>
        private CRCProviderBase Provider { get; set; }

        /// <summary>
        /// Gets or sets the data format.
        /// </summary>
        /// <value>The data format.</value>
        public EnumOriginalDataFormat DataFormat
        {
            get { return _dataFormat; }
            set { _dataFormat = value; }
        }

        /// <summary>
        /// Creates the CRC provider.
        /// </summary>
        /// <param name="Provider">The provider.</param>
        /// <returns>AbsCRCProvider.</returns>
        public CRCProviderBase CreateCRCProvider(EnumCRCProvider Provider)
        {
            this.Provider = null;
            switch (Provider)
            {
                case EnumCRCProvider.CRC16:
                    this.Provider = new CRC16Provider();
                    break;

                case EnumCRCProvider.CRC32:
                    this.Provider = new CRC32Provider();
                    break;

                case EnumCRCProvider.CRC8:
                    this.Provider = new CRC8Provider();
                    break;

                case EnumCRCProvider.CRC8CCITT:
                    this.Provider = new CRC8Provider(0x07);
                    break;

                case EnumCRCProvider.CRC8DALLASMAXIM:
                    this.Provider = new CRC8Provider(0x31);
                    break;

                case EnumCRCProvider.CRC8SAEJ1850:
                    this.Provider = new CRC8Provider(0x1D);
                    break;

                case EnumCRCProvider.CRC8WCDMA:
                    this.Provider = new CRC8Provider(0x9b);
                    break;

                case EnumCRCProvider.CRC16Modbus:
                    this.Provider = new CRC16ModbusProvider();
                    break;

                case EnumCRCProvider.CRC16CCITT_0x0000:
                    this.Provider = new CRC16CCITTProvider(0x0000);
                    break;

                case EnumCRCProvider.CRC16CCITT_0xFFFF:
                    this.Provider = new CRC16CCITTProvider(0xFFFF);
                    break;

                case EnumCRCProvider.CRC16CCITT_0x1D0F:
                    this.Provider = new CRC16CCITTProvider(0x1D0F);
                    break;

                case EnumCRCProvider.CRC16Kermit:
                    this.Provider = new CRC16KermitProvider();
                    break;
            }
            this.Provider.DataFormat = this.DataFormat;

            return this.Provider;
        }
    }
}