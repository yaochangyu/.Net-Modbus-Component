// ***********************************************************************
// Assembly         : Tako.Modbus.Client
// Author           : 余小章
// Created          : 08-19-2014
//
// Last Modified By : 余小章
// Last Modified On : 08-19-2014
// ***********************************************************************
// <copyright file="ModbusResponseBase.cs" company="">
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
    /// <summary>
    /// Class ModbusResponseBase.
    /// </summary>
    internal abstract class ModbusResponseBase
    {
        /// <summary>
        /// The _utility
        /// </summary>
        private ModbusUtility _utility = new ModbusUtility();

        /// <summary>
        /// Gets or sets the function code position.
        /// </summary>
        /// <value>The function code position.</value>
        protected abstract byte FunctionCodePosition { get; set; }

        /// <summary>
        /// Gets or sets the utility.
        /// </summary>
        /// <value>The utility.</value>
        protected virtual ModbusUtility Utility
        {
            get { return _utility; }
            set { _utility = value; }
        }

        /// <summary>
        /// Exceptions the validate.
        /// </summary>
        /// <param name="ResponseArray">The response array.</param>
        /// <exception cref="ModbusException">No Response or Miss response data</exception>
        protected virtual void ExceptionValidate(byte[] ResponseArray)
        {
            if (ResponseArray == null | ResponseArray.Length <= FunctionCodePosition)
            {
                throw new ModbusException("No Response or Miss response data");
            }

            //exception
            var functionCode = ResponseArray[FunctionCodePosition];
            if (functionCode >= 80)
            {
                var exceptionCode = ResponseArray[FunctionCodePosition + 1];
                throw ModbusException.GetModbusException(exceptionCode);
            }
        }

        /// <summary>
        /// Functions the code validate.
        /// </summary>
        /// <param name="RequestArray">The request array.</param>
        /// <param name="ResponseArray">The response array.</param>
        /// <param name="FunctionCodeFlag">The function code flag.</param>
        protected virtual void FunctionCodeValidate(byte[] RequestArray, byte[] ResponseArray, EnumModbusFunctionCode FunctionCodeFlag)
        {
            //function code valid
            if (RequestArray[FunctionCodePosition] != ResponseArray[FunctionCodePosition])
            {
                throw ModbusException.GetModbusException(0x01);
            }
            if ((byte)FunctionCodeFlag != RequestArray[FunctionCodePosition])
            {
                throw ModbusException.GetModbusException(0x01);
            }
            if ((byte)FunctionCodeFlag != ResponseArray[FunctionCodePosition])
            {
                throw ModbusException.GetModbusException(0x01);
            }
        }

        /// <summary>
        /// Checks the data validate.
        /// </summary>
        /// <param name="ResponseArray">The response array.</param>
        protected abstract void CheckDataValidate(byte[] ResponseArray);

        /// <summary>
        /// Gets the result array.
        /// </summary>
        /// <param name="ResponseArray">The response array.</param>
        /// <returns>System.Byte[].</returns>
        protected abstract byte[] GetResultArray(byte[] ResponseArray);

        /// <summary>
        /// Reads the coils.
        /// </summary>
        /// <param name="RequestArray">The request array.</param>
        /// <param name="ResponseArray">The response array.</param>
        /// <returns>System.Byte[].</returns>
        public virtual byte[] ReadCoils(byte[] RequestArray, byte[] ResponseArray)
        {
            this.ExceptionValidate(ResponseArray);
            this.FunctionCodeValidate(RequestArray, ResponseArray, EnumModbusFunctionCode.ReadCoils);
            this.CheckDataValidate(ResponseArray);
            var resultArray = GetResultArray(ResponseArray);
            return resultArray;
        }

        /// <summary>
        /// Reads the discrete inputs.
        /// </summary>
        /// <param name="RequestArray">The request array.</param>
        /// <param name="ResponseArray">The response array.</param>
        /// <returns>System.Byte[].</returns>
        public virtual byte[] ReadDiscreteInputs(byte[] RequestArray, byte[] ResponseArray)
        {
            this.ExceptionValidate(ResponseArray);
            this.FunctionCodeValidate(RequestArray, ResponseArray, EnumModbusFunctionCode.ReadDiscreteInputs);
            this.CheckDataValidate(ResponseArray);
            var resultArray = GetResultArray(ResponseArray);
            return resultArray;
        }

        /// <summary>
        /// Reads the holding registers.
        /// </summary>
        /// <param name="RequestArray">The request array.</param>
        /// <param name="ResponseArray">The response array.</param>
        /// <returns>System.Byte[].</returns>
        public virtual byte[] ReadHoldingRegisters(byte[] RequestArray, byte[] ResponseArray)
        {
            this.ExceptionValidate(ResponseArray);
            this.FunctionCodeValidate(RequestArray, ResponseArray, EnumModbusFunctionCode.ReadHoldingRegisters);
            this.CheckDataValidate(ResponseArray);
            var resultArray = GetResultArray(ResponseArray);
            return resultArray;
        }

        /// <summary>
        /// Reads the input registers.
        /// </summary>
        /// <param name="RequestArray">The request array.</param>
        /// <param name="ResponseArray">The response array.</param>
        /// <returns>System.Byte[].</returns>
        public virtual byte[] ReadInputRegisters(byte[] RequestArray, byte[] ResponseArray)
        {
            this.ExceptionValidate(ResponseArray);
            this.FunctionCodeValidate(RequestArray, ResponseArray, EnumModbusFunctionCode.ReadInputRegisters);
            this.CheckDataValidate(ResponseArray);
            var resultArray = GetResultArray(ResponseArray);
            return resultArray;
        }

        /// <summary>
        /// Writes the single coil.
        /// </summary>
        /// <param name="RequestArray">The request array.</param>
        /// <param name="ResponseArray">The response array.</param>
        /// <returns>System.Byte[].</returns>
        public virtual byte[] WriteSingleCoil(byte[] RequestArray, byte[] ResponseArray)
        {
            this.ExceptionValidate(ResponseArray);
            this.FunctionCodeValidate(RequestArray, ResponseArray, EnumModbusFunctionCode.WriteSingleCoil);
            this.CheckDataValidate(ResponseArray);
            return ResponseArray;
        }

        /// <summary>
        /// Writes the single register.
        /// </summary>
        /// <param name="RequestArray">The request array.</param>
        /// <param name="ResponseArray">The response array.</param>
        /// <returns>System.Byte[].</returns>
        public virtual byte[] WriteSingleRegister(byte[] RequestArray, byte[] ResponseArray)
        {
            this.ExceptionValidate(ResponseArray);
            this.FunctionCodeValidate(RequestArray, ResponseArray, EnumModbusFunctionCode.WriteSingleRegister);
            this.CheckDataValidate(ResponseArray);
            return ResponseArray;
        }

        /// <summary>
        /// Writes the multiple coils.
        /// </summary>
        /// <param name="RequestArray">The request array.</param>
        /// <param name="ResponseArray">The response array.</param>
        /// <returns>System.Byte[].</returns>
        public virtual byte[] WriteMultipleCoils(byte[] RequestArray, byte[] ResponseArray)
        {
            this.ExceptionValidate(ResponseArray);
            this.FunctionCodeValidate(RequestArray, ResponseArray, EnumModbusFunctionCode.WriteMultipleCoils);
            this.CheckDataValidate(ResponseArray);
            return ResponseArray;
        }

        /// <summary>
        /// Writes the multiple registers.
        /// </summary>
        /// <param name="RequestArray">The request array.</param>
        /// <param name="ResponseArray">The response array.</param>
        /// <returns>System.Byte[].</returns>
        public virtual byte[] WriteMultipleRegisters(byte[] RequestArray, byte[] ResponseArray)
        {
            this.ExceptionValidate(ResponseArray);
            this.FunctionCodeValidate(RequestArray, ResponseArray, EnumModbusFunctionCode.WriteMultipleRegisters);
            this.CheckDataValidate(ResponseArray);
            return ResponseArray;
        }
    }
}