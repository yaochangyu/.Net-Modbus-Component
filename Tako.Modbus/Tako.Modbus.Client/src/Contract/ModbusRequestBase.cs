// ***********************************************************************
// Assembly         : Tako.Modbus.Client
// Author           : 余小章
// Created          : 08-19-2014
//
// Last Modified By : 余小章
// Last Modified On : 08-27-2014
// ***********************************************************************
// <copyright file="ModbusRequestBase.cs" company="">
//     Copyright (c) . 余小章. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.IO;
using Tako.Modbus.Core;

/// <summary>
/// The Client namespace.
/// </summary>
namespace Tako.Modbus.Client
{
    using System;

    /// <summary>
    /// Class ModbusRequestBase.
    /// </summary>
    internal abstract class ModbusRequestBase
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
        /// Gets or sets the transaction identifier.
        /// </summary>
        /// <value>The transaction identifier.</value>
        public virtual ushort TransactionId { get; set; }

        /// <summary>
        /// Quantities the validate.
        /// </summary>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <param name="MinQuantity">The minimum quantity.</param>
        /// <param name="MaxQuantity">The maximum quantity.</param>
        protected virtual void QuantityValidate(ushort startAddress, ushort quantity, ushort MinQuantity, ushort MaxQuantity)
        {
            if (quantity < MinQuantity || quantity > MaxQuantity)
            {
                throw ModbusException.GetModbusException(0x03);
            }
        }

        /// <summary>
        /// Gets the multi output count.
        /// </summary>
        /// <param name="quantity">The quantity.</param>
        /// <returns>System.Byte.</returns>
        protected virtual byte GetMultiOutputCount(ushort quantity)
        {
            byte i = (byte)(quantity / 8);
            byte j = (byte)(quantity - (i * 8));

            byte counter = 0;
            if (j == 0)
            {
                counter = i;
            }
            else
            {
                counter = (byte)(i + 1);
            }

            return counter;
        }

        /// <summary>
        /// Gets the multi output count.
        /// </summary>
        /// <param name="OutputValues">The output values.</param>
        /// <returns>System.Byte[].</returns>
        protected virtual byte[] GetMultiOutputCount(short[] OutputValues)
        {
            byte counter = 0;
            byte[] outputArray = null;

            using (MemoryStream stream = new MemoryStream())
            {
                foreach (var output in OutputValues)
                {
                    var tempArray = BitConverter.GetBytes((short)output);
                    Array.Reverse(tempArray);
                    stream.Write(tempArray, 0, tempArray.Length);
                    counter += (byte)tempArray.Length;
                }

                outputArray = stream.ToArray();
            }
            return outputArray;
        }

        /// <summary>
        /// Creates the request message.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="FunctionCode">The function code.</param>
        /// <param name="MultiOutputLength">Length of the multi output.</param>
        /// <param name="Parameters">The parameters.</param>
        /// <returns>System.Byte[].</returns>
        protected abstract byte[] CreateRequestMessage(byte unit, EnumModbusFunctionCode FunctionCode, byte? MultiOutputLength, params byte[] Parameters);

        /// <summary>
        /// Reads the coils.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>System.Byte[].</returns>
        public virtual byte[] ReadCoils(byte unit, ushort startAddress, ushort quantity)
        {
            this.QuantityValidate(startAddress, quantity, 1, 2000);

            var parameters = new byte[]
            {
                (byte)(startAddress >> 8),
                (byte)(startAddress),
                (byte)(quantity >> 8),
                (byte)quantity
            };
            var requestArray = this.CreateRequestMessage(unit, EnumModbusFunctionCode.ReadCoils, null, parameters);
            return requestArray;
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
            this.QuantityValidate(startAddress, quantity, 1, 2000);

            var parameters = new byte[]
            {
                (byte)(startAddress >> 8),
                (byte)(startAddress),
                (byte)(quantity >> 8),
                (byte)quantity
            };
            var requestArray = this.CreateRequestMessage(unit, EnumModbusFunctionCode.ReadDiscreteInputs, null, parameters);
            return requestArray;
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
            this.QuantityValidate(startAddress, quantity, 1, 175);

            var parameters = new byte[]
            {
                (byte)(startAddress >> 8),
                (byte)(startAddress),
                (byte)(quantity >> 8),
                (byte)quantity
            };
            var requestArray = this.CreateRequestMessage(unit, EnumModbusFunctionCode.ReadHoldingRegisters, null, parameters);
            return requestArray;
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
            this.QuantityValidate(startAddress, quantity, 1, 175);

            var parameters = new byte[]
            {
                (byte)(startAddress >> 8),
                (byte)(startAddress),
                (byte)(quantity >> 8),
                (byte)quantity
            };
            var requestArray = this.CreateRequestMessage(unit, EnumModbusFunctionCode.ReadInputRegisters, null, parameters);
            return requestArray;
        }

        /// <summary>
        /// Writes the single coil.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="OutputAddress">The output address.</param>
        /// <param name="OutputValue">if set to <c>true</c> [output value].</param>
        /// <returns>System.Byte[].</returns>
        public virtual byte[] WriteSingleCoil(byte unit, ushort OutputAddress, bool OutputValue)
        {
            ushort outputValue = 0x0000;
            if (OutputValue)
            {
                outputValue = 0xFF00;
            }

            var parameters = new byte[]
            {
                (byte)(OutputAddress >> 8),
                (byte)(OutputAddress),
                (byte)(outputValue >> 8),
                (byte)(outputValue),
            };
            var requestArray = this.CreateRequestMessage(unit, EnumModbusFunctionCode.WriteSingleCoil, null, parameters);
            return requestArray;
        }

        /// <summary>
        /// Writes the single register.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="OutputAddress">The output address.</param>
        /// <param name="OutputValue">The output value.</param>
        /// <returns>System.Byte[].</returns>
        public virtual byte[] WriteSingleRegister(byte unit, ushort OutputAddress, short OutputValue)
        {
            var parameters = new byte[]
            {
                (byte)(OutputAddress >> 8),
                (byte)(OutputAddress),
                (byte)(OutputValue >> 8),
                (byte)(OutputValue),
            };
            var requestArray = this.CreateRequestMessage(unit, EnumModbusFunctionCode.WriteSingleRegister, null, parameters);
            return requestArray;
        }

        /// <summary>
        /// Writes the multiple coils.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <param name="OutputValues">The output values.</param>
        /// <returns>System.Byte[].</returns>
        public virtual byte[] WriteMultipleCoils(byte unit, ushort startAddress, ushort quantity, byte[] OutputValues)
        {
            this.QuantityValidate(startAddress, quantity, 1, 0x07B0);

            byte counter = this.GetMultiOutputCount(quantity);

            if (counter != OutputValues.Length)
            {
                ModbusException.GetModbusException(0x03);
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                memoryStream.WriteByte((byte)(startAddress >> 8));
                memoryStream.WriteByte((byte)(startAddress));
                memoryStream.WriteByte((byte)(quantity >> 8));
                memoryStream.WriteByte((byte)(quantity));
                memoryStream.WriteByte((byte)(counter));
                memoryStream.Write(OutputValues, 0, OutputValues.Length);
                var requestArray = this.CreateRequestMessage(unit, EnumModbusFunctionCode.WriteMultipleCoils, (byte)OutputValues.Length, memoryStream.ToArray());
                return requestArray;
            }
        }

        /// <summary>
        /// Writes the multiple registers.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="startAddress">The start address.</param>
        /// <param name="quantity">The quantity.</param>
        /// <param name="OutputValues">The output values.</param>
        /// <returns>System.Byte[].</returns>
        public virtual byte[] WriteMultipleRegisters(byte unit, ushort startAddress, ushort quantity, short[] OutputValues)
        {
            this.QuantityValidate(startAddress, quantity, 1, 0x007B);

            byte[] outputArray = this.GetMultiOutputCount(OutputValues);
            byte counter = (byte)outputArray.Length;

            if (quantity * 2 != outputArray.Length)
            {
                ModbusException.GetModbusException(0x02);
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                memoryStream.WriteByte((byte)(startAddress >> 8));
                memoryStream.WriteByte((byte)(startAddress));
                memoryStream.WriteByte((byte)(quantity >> 8));
                memoryStream.WriteByte((byte)(quantity));
                memoryStream.WriteByte((byte)(counter));
                memoryStream.Write(outputArray, 0, outputArray.Length);
                var requestArray = this.CreateRequestMessage(unit, EnumModbusFunctionCode.WriteMultipleRegisters, (byte)outputArray.Length, memoryStream.ToArray());
                return requestArray;
            }
        }
    }
}