// ***********************************************************************
// Assembly         : Tako.Modbus.Client
// Author           : 余小章
// Created          : 08-19-2014
//
// Last Modified By : 余小章
// Last Modified On : 08-19-2014
// ***********************************************************************
// <copyright file="AsciiModbusDataConvert.cs" company="">
//     Copyright (c) . 余小章. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tako.Modbus.Core;

/// <summary>
/// The Client namespace.
/// </summary>
namespace Tako.Modbus.Client
{
    /// <summary>
    /// Class AsciiModbusDataConvert.
    /// </summary>
    internal class AsciiModbusDataConvert : ModbusDataConvertBase
    {
        /// <summary>
        /// Gets or sets the result array.
        /// </summary>
        /// <value>The result array.</value>
        public override byte[] ResultArray { get; internal set; }

        /// <summary>
        /// To the decimal.
        /// </summary>
        /// <param name="ResultArray">The result array.</param>
        /// <param name="unit">The unit.</param>
        /// <returns>IEnumerable&lt;System.Int64&gt;.</returns>
        /// <exception cref="System.ArgumentNullException">ResultArray</exception>
        /// <exception cref="System.FormatException">ResultArray</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">unit</exception>
        public override IEnumerable<long> ToDecimal(byte[] ResultArray, EnumModbusIntegralUnit unit)
        {
            var length = ((int)unit) * 2;
            if (ResultArray == null)
            {
                throw new ArgumentNullException("ResultArray");
            }
            if (ResultArray.Length <= 0 || ResultArray.Length < length)
            {
                throw new FormatException("ResultArray");
            }
            this.ResultArray = ResultArray;

            List<long> resultList = new List<long>();
            for (int i = 0; i < ResultArray.Length; i = i + length)
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    memory.Write(ResultArray, i, length);
                    var tempArray = memory.ToArray();

                    var hex = Encoding.ASCII.GetString(tempArray);
                    long result = 0;
                    switch (unit)
                    {
                        case EnumModbusIntegralUnit.Byte:
                            result = Convert.ToInt16(hex, 16);
                            break;

                        case EnumModbusIntegralUnit.Word:
                            result = Convert.ToInt16(hex, 16);
                            break;

                        case EnumModbusIntegralUnit.DWord:
                            result = Convert.ToInt32(hex, 16);
                            break;

                        case EnumModbusIntegralUnit.QWord:
                            result = Convert.ToInt64(hex, 16);
                            break;

                        default:
                            throw new ArgumentOutOfRangeException("unit");
                    }

                    resultList.Add(result);
                }
            }
            return resultList;
        }

        /// <summary>
        /// To the octal.
        /// </summary>
        /// <param name="ResultArray">The result array.</param>
        /// <param name="unit">The unit.</param>
        /// <returns>IEnumerable&lt;System.Int64&gt;.</returns>
        /// <exception cref="System.ArgumentNullException">ResultArray</exception>
        /// <exception cref="System.FormatException">ResultArray</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">unit</exception>
        public override IEnumerable<long> ToOctal(byte[] ResultArray, EnumModbusIntegralUnit unit)
        {
            var length = ((int)unit) * 2;
            if (ResultArray == null)
            {
                throw new ArgumentNullException("ResultArray");
            }
            if (ResultArray.Length <= 0 || ResultArray.Length < length)
            {
                throw new FormatException("ResultArray");
            }
            this.ResultArray = ResultArray;
            List<long> resultList = new List<long>();

            for (int i = 0; i < ResultArray.Length; i = i + length)
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    memory.Write(ResultArray, i, length);
                    var tempArray = memory.ToArray();
                    var hex = Encoding.ASCII.GetString(tempArray);

                    switch (unit)
                    {
                        case EnumModbusIntegralUnit.Byte:
                            var decByte = Convert.ToInt16(hex, 16);
                            var octByte = Convert.ToInt16(Convert.ToString(decByte, 8));
                            resultList.Add(octByte);
                            break;

                        case EnumModbusIntegralUnit.Word:
                            var decShort = Convert.ToInt16(hex, 16);
                            var octShort = Convert.ToInt32(Convert.ToString(decShort, 8));
                            resultList.Add(octShort);
                            break;

                        case EnumModbusIntegralUnit.DWord:
                            var decInt = Convert.ToInt32(hex, 16);
                            var octInt = Convert.ToInt64(Convert.ToString(decInt, 8));
                            resultList.Add(octInt);
                            break;

                        case EnumModbusIntegralUnit.QWord:
                            var decLong = Convert.ToInt64(hex, 16);
                            var octLong = Convert.ToInt64(Convert.ToString(decLong, 8));
                            resultList.Add(octLong);
                            break;

                        default:
                            throw new ArgumentOutOfRangeException("unit");
                    }
                }
            }

            return resultList;
        }

        /// <summary>
        /// To the hexadecimal.
        /// </summary>
        /// <param name="ResultArray">The result array.</param>
        /// <param name="unit">The unit.</param>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        /// <exception cref="System.ArgumentNullException">ResultArray</exception>
        /// <exception cref="System.FormatException">ResultArray</exception>
        public override IEnumerable<string> ToHexadecimal(byte[] ResultArray, EnumModbusIntegralUnit unit)
        {
            var length = ((int)unit) * 2;
            if (ResultArray == null)
            {
                throw new ArgumentNullException("ResultArray");
            }
            if (ResultArray.Length <= 0 || ResultArray.Length < length)
            {
                throw new FormatException("ResultArray");
            }
            this.ResultArray = ResultArray;
            List<string> resultList = new List<string>();
            for (int i = 0; i < ResultArray.Length; i = i + length)
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    memory.Write(ResultArray, i, length);
                    var tempArray = memory.ToArray();
                    var result = Encoding.ASCII.GetString(tempArray);
                    resultList.Add(result);
                }
            }
            return resultList;
        }

        /// <summary>
        /// To the binary.
        /// </summary>
        /// <param name="ResultArray">The result array.</param>
        /// <param name="unit">The unit.</param>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        /// <exception cref="System.ArgumentNullException">ResultArray</exception>
        /// <exception cref="System.FormatException">ResultArray</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">unit</exception>
        public override IEnumerable<string> ToBinary(byte[] ResultArray, EnumModbusIntegralUnit unit)
        {
            var length = ((int)unit) * 2;
            if (ResultArray == null)
            {
                throw new ArgumentNullException("ResultArray");
            }
            if (ResultArray.Length <= 0 || ResultArray.Length < length)
            {
                throw new FormatException("ResultArray");
            }
            this.ResultArray = ResultArray;
            List<string> resultList = new List<string>();
            for (int i = 0; i < ResultArray.Length; i = i + length)
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    memory.Write(ResultArray, i, length);
                    var tempArray = memory.ToArray();
                    var hex = Encoding.ASCII.GetString(tempArray);
                    string bin = "";
                    switch (unit)
                    {
                        case EnumModbusIntegralUnit.Byte:
                            var decByte = Convert.ToInt16(hex, 16);
                            bin = Convert.ToString(decByte, 2).PadLeft(8, '0');
                            break;

                        case EnumModbusIntegralUnit.Word:
                            var decShort = Convert.ToInt16(hex, 16);
                            bin = Convert.ToString(decShort, 2).PadLeft(16, '0');
                            break;

                        case EnumModbusIntegralUnit.DWord:
                            var decInt = Convert.ToInt32(hex, 16);
                            bin = Convert.ToString(decInt, 2).PadLeft(32, '0');
                            break;

                        case EnumModbusIntegralUnit.QWord:
                            var decLong = Convert.ToInt64(hex, 16);
                            bin = Convert.ToString(decLong, 2).PadLeft(64, '0');
                            break;

                        default:
                            throw new ArgumentOutOfRangeException("unit");
                    }

                    resultList.Add(bin);
                }
            }

            return resultList;
        }

        /// <summary>
        /// To the float.
        /// </summary>
        /// <param name="ResultArray">The result array.</param>
        /// <returns>IEnumerable&lt;System.Single&gt;.</returns>
        /// <exception cref="System.ArgumentNullException">ResultArray</exception>
        /// <exception cref="System.FormatException">ResultArray</exception>
        public override IEnumerable<float> ToFloat(byte[] ResultArray)
        {
            var length = 8;
            if (ResultArray == null)
            {
                throw new ArgumentNullException("ResultArray");
            }
            if (ResultArray.Length <= 0 || ResultArray.Length < length)
            {
                throw new FormatException("ResultArray");
            }

            this.ResultArray = ResultArray;
            int count = ResultArray.Length / length;

            List<float> resultList = new List<float>();
            List<string> hexList = new List<string>();

            for (int i = 0; i < count * length; i = i + length)
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    memory.Write(ResultArray, i, length);
                    var tempArray = memory.ToArray();
                    var hex = Encoding.ASCII.GetString(tempArray);
                    hexList.Add(hex);
                }
            }

            for (int i = 0; i < hexList.Count; i++)
            {
                var hexArray = this.ModbusUtility.HexStringToBytes(hexList[i]);
                using (MemoryStream memory = new MemoryStream())
                {
                    memory.Write(hexArray, 1, 1);
                    memory.Write(hexArray, 0, 1);
                    memory.Write(hexArray, 3, 1);
                    memory.Write(hexArray, 2, 1);

                    float result = BitConverter.ToSingle(memory.ToArray(), 0);
                    resultList.Add(result);
                }
            }
            return resultList;
        }

        /// <summary>
        /// To the double.
        /// </summary>
        /// <param name="ResultArray">The result array.</param>
        /// <returns>IEnumerable&lt;System.Double&gt;.</returns>
        /// <exception cref="System.ArgumentNullException">ResultArray</exception>
        /// <exception cref="System.FormatException">ResultArray</exception>
        public override IEnumerable<double> ToDouble(byte[] ResultArray)
        {
            var length = 16;
            if (ResultArray == null)
            {
                throw new ArgumentNullException("ResultArray");
            }
            if (ResultArray.Length <= 0 || ResultArray.Length < length)
            {
                throw new FormatException("ResultArray");
            }

            this.ResultArray = ResultArray;
            int count = ResultArray.Length / length;

            List<double> resultList = new List<double>();
            List<string> hexList = new List<string>();

            for (int i = 0; i < count * length; i = i + length)
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    memory.Write(ResultArray, i, length);
                    var tempArray = memory.ToArray();
                    var hex = Encoding.ASCII.GetString(tempArray);
                    hexList.Add(hex);
                }
            }

            for (int i = 0; i < hexList.Count; i++)
            {
                var hexArray = this.ModbusUtility.HexStringToBytes(hexList[i]);
                using (MemoryStream memory = new MemoryStream())
                {
                    memory.Write(hexArray, 1, 1);
                    memory.Write(hexArray, 0, 1);
                    memory.Write(hexArray, 3, 1);
                    memory.Write(hexArray, 2, 1);

                    memory.Write(hexArray, 5, 1);
                    memory.Write(hexArray, 4, 1);
                    memory.Write(hexArray, 7, 1);
                    memory.Write(hexArray, 6, 1);

                    double result = BitConverter.ToDouble(memory.ToArray(), 0);
                    resultList.Add(result);
                }
            }
            return resultList;
        }
    }
}