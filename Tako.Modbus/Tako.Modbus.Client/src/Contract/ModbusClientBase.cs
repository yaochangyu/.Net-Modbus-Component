// ***********************************************************************
// Assembly         : Tako.Modbus.Client
// Author           : 余小章
// Created          : 08-19-2014
//
// Last Modified By : 余小章
// Last Modified On : 08-27-2014
// ***********************************************************************
// <copyright file="ModbusClientBase.cs" company="">
//     Copyright (c) . 余小章. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Tako.Modbus.Core;

/// <summary>
/// The Client namespace.
/// </summary>
namespace Tako.Modbus.Client
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class AbsModbusClient.
    /// </summary>
    public abstract class ModbusClientBase : IModbusTransport, IDisposable
    {
        private ushort _transactionId;
        //fields

        /// <summary>
        /// Gets or sets the request array.
        /// </summary>
        /// <value>The request array.</value>
        public byte[] RequestArray { get; set; }

        /// <summary>
        /// Gets or sets the receive array.
        /// </summary>
        /// <value>The receive array.</value>
        public byte[] ReceiveArray { get; set; }

        /// <summary>
        /// Gets or sets the transaction identifier.
        /// </summary>
        /// <value>The transaction identifier.</value>
        public virtual ushort TransactionId
        {
            get { return _transactionId; }
            set
            {
                this.ModbusRequest.TransactionId = value;
                _transactionId = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ModbusClientBase" /> is disposed.
        /// </summary>
        /// <value><c>true</c> if disposed; otherwise, <c>false</c>.</value>
        protected virtual bool Disposed
        {
            get;
            set;
        }

        //abstract properties
        /// <summary>
        /// Gets or sets the modbus request.
        /// </summary>
        /// <value>The modbus request.</value>
        internal abstract ModbusRequestBase ModbusRequest { get; set; }

        /// <summary>
        /// Gets or sets the modbus response.
        /// </summary>
        /// <value>The modbus response.</value>
        internal abstract ModbusResponseBase ModbusResponse { get; set; }

        /// <summary>
        /// Gets or sets the modbus data convert.
        /// </summary>
        /// <value>The modbus data convert.</value>
        internal abstract ModbusDataConvertBase ModbusDataConvert { get; set; }

        //virtual method
        /// <summary>
        /// Sends the and receive.
        /// </summary>
        /// <param name="requestArray">The request array.</param>
        /// <returns>System.Byte[].</returns>
        /// <exception cref="System.ArgumentNullException">RequestArray</exception>
        public virtual byte[] SendAndReceive(byte[] requestArray)
        {
            if (requestArray == null)
            {
                throw new ArgumentNullException("RequestArray");
            }
            Send(requestArray);
            var receiveArray = Receive();
            this.RequestArray = requestArray;
            this.ReceiveArray = receiveArray;
            return receiveArray;
        }

        //abstract method
        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public abstract bool Disconnect();

        /// <summary>
        /// Receives this instance.
        /// </summary>
        /// <returns>System.Byte[].</returns>
        public abstract byte[] Receive();

        /// <summary>
        /// Sends the specified request array.
        /// </summary>
        /// <param name="RequestArray">The request array.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public abstract int Send(byte[] RequestArray);

        /// <summary>
        /// Connects the specified connect configuration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ConnectConfig">The connect configuration.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public abstract bool Connect<T>(T ConnectConfig);

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public abstract void Dispose();

        /// <summary>
        /// Reads the coils.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>System.Byte[].</returns>
        public virtual byte[] ReadCoils(byte unit, ushort startAddress, ushort quantity)
        {
            var requestArray = this.ModbusRequest.ReadCoils(unit, startAddress, quantity);
            var responseArray = SendAndReceive(requestArray);
            var result = this.ModbusResponse.ReadCoils(requestArray, responseArray);
            return result;
        }

        /// <summary>
        /// Reads the coils to decimal.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>IEnumerable&lt;System.Int64&gt;.</returns>
        public virtual IEnumerable<long> ReadCoilsToDecimal(byte unit, ushort startAddress, ushort quantity)
        {
            var resultArray = this.ReadCoils(unit, startAddress, quantity);
            return this.ModbusDataConvert.ToDecimal(resultArray, EnumModbusIntegralUnit.Byte);
        }

        /// <summary>
        /// Reads the coils to binary.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        public virtual IEnumerable<string> ReadCoilsToBinary(byte unit, ushort startAddress, ushort quantity)
        {
            var resultArray = this.ReadCoils(unit, startAddress, quantity);
            return this.ModbusDataConvert.ToBinary(resultArray, EnumModbusIntegralUnit.Byte);
        }

        /// <summary>
        /// Reads the coils to octal.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>IEnumerable&lt;System.Int64&gt;.</returns>
        public virtual IEnumerable<long> ReadCoilsToOctal(byte unit, ushort startAddress, ushort quantity)
        {
            var resultArray = this.ReadCoils(unit, startAddress, quantity);
            return this.ModbusDataConvert.ToOctal(resultArray, EnumModbusIntegralUnit.Byte);
        }

        /// <summary>
        /// Reads the discrete inputs.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>System.Byte[].</returns>
        public virtual byte[] ReadDiscreteInputs(byte unit, ushort startAddress, ushort quantity)
        {
            var requestArray = this.ModbusRequest.ReadDiscreteInputs(unit, startAddress, quantity);
            var responseArray = SendAndReceive(requestArray);
            var result = this.ModbusResponse.ReadDiscreteInputs(requestArray, responseArray);
            return result;
        }

        /// <summary>
        /// Reads the discrete inputs to decimal.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>IEnumerable&lt;System.Int64&gt;.</returns>
        public virtual IEnumerable<long> ReadDiscreteInputsToDecimal(byte unit, ushort startAddress, ushort quantity)
        {
            var resultArray = this.ReadDiscreteInputs(unit, startAddress, quantity);
            return this.ModbusDataConvert.ToDecimal(resultArray, EnumModbusIntegralUnit.Byte);
        }

        /// <summary>
        /// Reads the discrete inputs to binary.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        public virtual IEnumerable<string> ReadDiscreteInputsToBinary(byte unit, ushort startAddress, ushort quantity)
        {
            var resultArray = this.ReadDiscreteInputs(unit, startAddress, quantity);
            return this.ModbusDataConvert.ToBinary(resultArray, EnumModbusIntegralUnit.Byte);
        }

        /// <summary>
        /// Reads the discrete inputs to octal.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>IEnumerable&lt;System.Int64&gt;.</returns>
        public virtual IEnumerable<long> ReadDiscreteInputsToOctal(byte unit, ushort startAddress, ushort quantity)
        {
            var resultArray = this.ReadDiscreteInputs(unit, startAddress, quantity);
            return this.ModbusDataConvert.ToOctal(resultArray, EnumModbusIntegralUnit.Byte);
        }

        /// <summary>
        /// Reads the holding registers.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>System.Byte[].</returns>
        public virtual byte[] ReadHoldingRegisters(byte unit, ushort startAddress, ushort quantity)
        {
            var requestArray = this.ModbusRequest.ReadHoldingRegisters(unit, startAddress, quantity);
            var responseArray = SendAndReceive(requestArray);
            var result = this.ModbusResponse.ReadHoldingRegisters(requestArray, responseArray);
            return result;
        }

        /// <summary>
        /// Reads the holding registers to decimal.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>IEnumerable&lt;System.Int64&gt;.</returns>
        public virtual IEnumerable<long> ReadHoldingRegistersToDecimal(byte unit, ushort startAddress, ushort quantity)
        {
            var resultArray = this.ReadHoldingRegisters(unit, startAddress, quantity);
            return this.ModbusDataConvert.ToDecimal(resultArray, EnumModbusIntegralUnit.Word);
        }

        /// <summary>
        /// Reads the holding registers to binary.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        public virtual IEnumerable<string> ReadHoldingRegistersToBinary(byte unit, ushort startAddress, ushort quantity)
        {
            var resultArray = this.ReadHoldingRegisters(unit, startAddress, quantity);
            return this.ModbusDataConvert.ToBinary(resultArray, EnumModbusIntegralUnit.Word);
        }

        /// <summary>
        /// Reads the holding registers to octal.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>IEnumerable&lt;System.Int64&gt;.</returns>
        public virtual IEnumerable<long> ReadHoldingRegistersToOctal(byte unit, ushort startAddress, ushort quantity)
        {
            var resultArray = this.ReadHoldingRegisters(unit, startAddress, quantity);
            return this.ModbusDataConvert.ToOctal(resultArray, EnumModbusIntegralUnit.Word);
        }

        /// <summary>
        /// Reads the holding registers to float.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>IEnumerable&lt;System.Single&gt;.</returns>
        public virtual IEnumerable<float> ReadHoldingRegistersToFloat(byte unit, ushort startAddress, ushort quantity)
        {
            var resultArray = this.ReadHoldingRegisters(unit, startAddress, quantity);
            return this.ModbusDataConvert.ToFloat(resultArray);
        }

        /// <summary>
        /// Reads the holding registers to double.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>IEnumerable&lt;System.Double&gt;.</returns>
        public virtual IEnumerable<double> ReadHoldingRegistersToDouble(byte unit, ushort startAddress, ushort quantity)
        {
            var resultArray = this.ReadHoldingRegisters(unit, startAddress, quantity);
            return this.ModbusDataConvert.ToDouble(resultArray);
        }

        /// <summary>
        /// Reads the input registers.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>System.Byte[].</returns>
        public virtual byte[] ReadInputRegisters(byte unit, ushort startAddress, ushort quantity)
        {
            var requestArray = this.ModbusRequest.ReadInputRegisters(unit, startAddress, quantity);
            var responseArray = SendAndReceive(requestArray);
            var result = this.ModbusResponse.ReadInputRegisters(requestArray, responseArray);
            return result;
        }

        /// <summary>
        /// Reads the input registers to decimal.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>IEnumerable&lt;System.Int64&gt;.</returns>
        public virtual IEnumerable<long> ReadInputRegistersToDecimal(byte unit, ushort startAddress, ushort quantity)
        {
            var resultArray = this.ReadInputRegisters(unit, startAddress, quantity);
            return this.ModbusDataConvert.ToDecimal(resultArray, EnumModbusIntegralUnit.Word);
        }

        /// <summary>
        /// Reads the input registers to binary.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        public virtual IEnumerable<string> ReadInputRegistersToBinary(byte unit, ushort startAddress, ushort quantity)
        {
            var resultArray = this.ReadInputRegisters(unit, startAddress, quantity);
            return this.ModbusDataConvert.ToBinary(resultArray, EnumModbusIntegralUnit.Word);
        }

        /// <summary>
        /// Reads the input registers to octal.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>IEnumerable&lt;System.Int64&gt;.</returns>
        public virtual IEnumerable<long> ReadInputRegistersToOctal(byte unit, ushort startAddress, ushort quantity)
        {
            var resultArray = this.ReadInputRegisters(unit, startAddress, quantity);
            return this.ModbusDataConvert.ToOctal(resultArray, EnumModbusIntegralUnit.Word);
        }

        /// <summary>
        /// Reads the input registers to float.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>IEnumerable&lt;System.Single&gt;.</returns>
        public virtual IEnumerable<float> ReadInputRegistersToFloat(byte unit, ushort startAddress, ushort quantity)
        {
            var resultArray = this.ReadInputRegisters(unit, startAddress, quantity);
            return this.ModbusDataConvert.ToFloat(resultArray);
        }

        /// <summary>
        /// Reads the input registers t double.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>IEnumerable&lt;System.Double&gt;.</returns>
        public virtual IEnumerable<double> ReadInputRegistersTDouble(byte unit, ushort startAddress, ushort quantity)
        {
            var resultArray = this.ReadInputRegisters(unit, startAddress, quantity);
            return this.ModbusDataConvert.ToDouble(resultArray);
        }

        /// <summary>
        /// Writes the single coil.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="outputAddress">The output address.</param>
        /// <param name="outputValue">if set to <c>true</c> [output value].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public virtual bool WriteSingleCoil(byte unit, ushort outputAddress, bool outputValue)
        {
            var requestArray = this.ModbusRequest.WriteSingleCoil(unit, outputAddress, outputValue);
            var responseArray = SendAndReceive(requestArray);
            var result = this.ModbusResponse.WriteSingleCoil(requestArray, responseArray);
            return result != null;
        }

        /// <summary>
        /// Writes the single register.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="outputAddress">The output address.</param>
        /// <param name="outputValue">The output value.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public virtual bool WriteSingleRegister(byte unit, ushort outputAddress, short outputValue)
        {
            var requestArray = this.ModbusRequest.WriteSingleRegister(unit, outputAddress, outputValue);
            var responseArray = SendAndReceive(requestArray);
            var result = this.ModbusResponse.WriteSingleRegister(requestArray, responseArray);
            return result != null;
        }

        /// <summary>
        /// Writes the multiple coils.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <param name="OutputValues">The output values.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public virtual bool WriteMultipleCoils(byte unit, ushort startAddress, ushort quantity, byte[] OutputValues)
        {
            var requestArray = this.ModbusRequest.WriteMultipleCoils(unit, startAddress, quantity, OutputValues);
            var responseArray = SendAndReceive(requestArray);
            var result = this.ModbusResponse.WriteMultipleCoils(requestArray, responseArray);
            return result != null;
        }

        /// <summary>
        /// Writes the multiple registers.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <param name="OutputValues">The output values.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public virtual bool WriteMultipleRegisters(byte unit, ushort startAddress, ushort quantity, short[] OutputValues)
        {
            var requestArray = this.ModbusRequest.WriteMultipleRegisters(unit, startAddress, quantity, OutputValues);
            var responseArray = SendAndReceive(requestArray);

            var result = this.ModbusResponse.WriteMultipleRegisters(requestArray, responseArray);
            return result != null;
        }
    }
}