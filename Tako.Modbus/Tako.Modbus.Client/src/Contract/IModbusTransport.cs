// ***********************************************************************
// Assembly         : Tako.Modbus.Client
// Author           : 余小章
// Created          : 08-19-2014
//
// Last Modified By : 余小章
// Last Modified On : 08-27-2014
// ***********************************************************************
// <copyright file="IModbusTransport.cs" company="">
//     Copyright (c) . 余小章. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

/// <summary>
/// The Client namespace.
/// </summary>
namespace Tako.Modbus.Client
{
    /// <summary>
    /// Interface IModbusTransport
    /// </summary>
    public interface IModbusTransport
    {
        /// <summary>
        /// Reads the coils.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>System.Byte[].</returns>
        byte[] ReadCoils(byte unit, ushort startAddress, ushort quantity);

        /// <summary>
        /// Reads the coils to decimal.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>IEnumerable&lt;System.Int64&gt;.</returns>
        IEnumerable<long> ReadCoilsToDecimal(byte unit, ushort startAddress, ushort quantity);

        /// <summary>
        /// Reads the coils to binary.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        IEnumerable<string> ReadCoilsToBinary(byte unit, ushort startAddress, ushort quantity);

        /// <summary>
        /// Reads the coils to octal.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>IEnumerable&lt;System.Int64&gt;.</returns>
        IEnumerable<long> ReadCoilsToOctal(byte unit, ushort startAddress, ushort quantity);

        /// <summary>
        /// Reads the discrete inputs.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>System.Byte[].</returns>
        byte[] ReadDiscreteInputs(byte unit, ushort startAddress, ushort quantity);

        /// <summary>
        /// Reads the discrete inputs to decimal.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>IEnumerable&lt;System.Int64&gt;.</returns>
        IEnumerable<long> ReadDiscreteInputsToDecimal(byte unit, ushort startAddress, ushort quantity);

        /// <summary>
        /// Reads the discrete inputs to binary.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        IEnumerable<string> ReadDiscreteInputsToBinary(byte unit, ushort startAddress, ushort quantity);

        /// <summary>
        /// Reads the discrete inputs to octal.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>IEnumerable&lt;System.Int64&gt;.</returns>
        IEnumerable<long> ReadDiscreteInputsToOctal(byte unit, ushort startAddress, ushort quantity);

        /// <summary>
        /// Reads the holding registers.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>System.Byte[].</returns>
        byte[] ReadHoldingRegisters(byte unit, ushort startAddress, ushort quantity);

        /// <summary>
        /// Reads the holding registers to decimal.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>IEnumerable&lt;System.Int64&gt;.</returns>
        IEnumerable<long> ReadHoldingRegistersToDecimal(byte unit, ushort startAddress, ushort quantity);

        /// <summary>
        /// Reads the holding registers to binary.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        IEnumerable<string> ReadHoldingRegistersToBinary(byte unit, ushort startAddress, ushort quantity);

        /// <summary>
        /// Reads the holding registers to octal.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>IEnumerable&lt;System.Int64&gt;.</returns>
        IEnumerable<long> ReadHoldingRegistersToOctal(byte unit, ushort startAddress, ushort quantity);

        /// <summary>
        /// Reads the holding registers to float.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>IEnumerable&lt;System.Single&gt;.</returns>
        IEnumerable<float> ReadHoldingRegistersToFloat(byte unit, ushort startAddress, ushort quantity);

        /// <summary>
        /// Reads the holding registers to double.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>IEnumerable&lt;System.Double&gt;.</returns>
        IEnumerable<double> ReadHoldingRegistersToDouble(byte unit, ushort startAddress, ushort quantity);

        /// <summary>
        /// Reads the input registers.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>System.Byte[].</returns>
        byte[] ReadInputRegisters(byte unit, ushort startAddress, ushort quantity);

        /// <summary>
        /// Reads the input registers to decimal.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>IEnumerable&lt;System.Int64&gt;.</returns>
        IEnumerable<long> ReadInputRegistersToDecimal(byte unit, ushort startAddress, ushort quantity);

        /// <summary>
        /// Reads the input registers to binary.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        IEnumerable<string> ReadInputRegistersToBinary(byte unit, ushort startAddress, ushort quantity);

        /// <summary>
        /// Reads the input registers to octal.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>IEnumerable&lt;System.Int64&gt;.</returns>
        IEnumerable<long> ReadInputRegistersToOctal(byte unit, ushort startAddress, ushort quantity);

        /// <summary>
        /// Reads the input registers to float.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>IEnumerable&lt;System.Single&gt;.</returns>
        IEnumerable<float> ReadInputRegistersToFloat(byte unit, ushort startAddress, ushort quantity);

        /// <summary>
        /// Reads the input registers t double.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>IEnumerable&lt;System.Double&gt;.</returns>
        IEnumerable<double> ReadInputRegistersTDouble(byte unit, ushort startAddress, ushort quantity);

        /// <summary>
        /// Writes the single coil.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="outputAddress">The output address.</param>
        /// <param name="outputValue">if set to <c>true</c> [output value].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool WriteSingleCoil(byte unit, ushort outputAddress, bool outputValue);

        /// <summary>
        /// Writes the single register.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="outputAddress">The output address.</param>
        /// <param name="outputValue">The output value.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool WriteSingleRegister(byte unit, ushort outputAddress, short outputValue);

        /// <summary>
        /// Writes the multiple coils.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <param name="outputValues">The output values.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool WriteMultipleCoils(byte unit, ushort startAddress, ushort quantity, byte[] outputValues);

        /// <summary>
        /// Writes the multiple registers.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <param name="outputValues">The output values.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool WriteMultipleRegisters(byte unit, ushort startAddress, ushort quantity, short[] outputValues);
    }
}