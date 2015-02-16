// ***********************************************************************
// Assembly         : Tako.Modbus.Client
// Author           : 余小章
// Created          : 08-19-2014
//
// Last Modified By : 余小章
// Last Modified On : 08-19-2014
// ***********************************************************************
// <copyright file="ModbusDataConvertBase.cs" company="">
//     Copyright (c) . 余小章. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using Tako.Modbus.Core;

/// <summary>
/// The Client namespace.
/// </summary>
namespace Tako.Modbus.Client
{
    /// <summary>
    /// Class ModbusDataConvertBase.
    /// </summary>
    internal abstract class ModbusDataConvertBase
    {
        /// <summary>
        /// The _modbus utility
        /// </summary>
        private ModbusUtility _modbusUtility = new ModbusUtility();

        /// <summary>
        /// Gets or sets the modbus utility.
        /// </summary>
        /// <value>The modbus utility.</value>
        protected ModbusUtility ModbusUtility
        {
            get { return _modbusUtility; }
            set { _modbusUtility = value; }
        }

        /// <summary>
        /// Gets or sets the result array.
        /// </summary>
        /// <value>The result array.</value>
        public abstract byte[] ResultArray { get; internal set; }

        /// <summary>
        /// To the decimal.
        /// </summary>
        /// <param name="ResultArray">The result array.</param>
        /// <param name="unit">The unit.</param>
        /// <returns>IEnumerable&lt;System.Int64&gt;.</returns>
        public abstract IEnumerable<long> ToDecimal(byte[] ResultArray, EnumModbusIntegralUnit unit);

        /// <summary>
        /// To the octal.
        /// </summary>
        /// <param name="ResultArray">The result array.</param>
        /// <param name="unit">The unit.</param>
        /// <returns>IEnumerable&lt;System.Int64&gt;.</returns>
        public abstract IEnumerable<long> ToOctal(byte[] ResultArray, EnumModbusIntegralUnit unit);

        /// <summary>
        /// To the hexadecimal.
        /// </summary>
        /// <param name="ResultArray">The result array.</param>
        /// <param name="unit">The unit.</param>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        public abstract IEnumerable<string> ToHexadecimal(byte[] ResultArray, EnumModbusIntegralUnit unit);

        /// <summary>
        /// To the binary.
        /// </summary>
        /// <param name="ResultArray">The result array.</param>
        /// <param name="unit">The unit.</param>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        public abstract IEnumerable<string> ToBinary(byte[] ResultArray, EnumModbusIntegralUnit unit);

        /// <summary>
        /// To the float.
        /// </summary>
        /// <param name="ResultArray">The result array.</param>
        /// <returns>IEnumerable&lt;System.Single&gt;.</returns>
        public abstract IEnumerable<float> ToFloat(byte[] ResultArray);

        /// <summary>
        /// To the double.
        /// </summary>
        /// <param name="ResultArray">The result array.</param>
        /// <returns>IEnumerable&lt;System.Double&gt;.</returns>
        public abstract IEnumerable<double> ToDouble(byte[] ResultArray);
    }
}