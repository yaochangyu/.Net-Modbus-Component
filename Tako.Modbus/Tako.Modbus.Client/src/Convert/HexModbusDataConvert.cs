// ***********************************************************************
// Assembly         : Tako.Modbus.Client
// Author           : 余小章
// Created          : 08-19-2014
//
// Last Modified By : 余小章
// Last Modified On : 08-19-2014
// ***********************************************************************
// <copyright file="HexModbusDataConvert.cs" company="">
//     Copyright (c) . 余小章. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.IO;
using Tako.Modbus.Core;

/// <summary>
/// The Client namespace.
/// </summary>
namespace Tako.Modbus.Client
{
    /// <summary>
    /// Class HexModbusDataConvert.
    /// </summary>
    internal class HexModbusDataConvert : ModbusDataConvertBase
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
            var length = (int)unit;
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

                    long result = 0;

                    if (BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(tempArray);
                    }
                    switch (unit)
                    {
                        case EnumModbusIntegralUnit.Byte:
                            result = tempArray[0];
                            break;

                        case EnumModbusIntegralUnit.Word:
                            result = BitConverter.ToInt16(tempArray, 0);
                            break;

                        case EnumModbusIntegralUnit.DWord:
                            result = BitConverter.ToInt32(tempArray, 0);
                            break;

                        case EnumModbusIntegralUnit.QWord:
                            result = BitConverter.ToInt64(tempArray, 0);
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
            var length = (int)unit;
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
                    if (BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(tempArray);
                    }

                    switch (unit)
                    {
                        case EnumModbusIntegralUnit.Byte:
                            var dec = tempArray[0];
                            var oct = int.Parse(Convert.ToString(dec, 8));
                            resultList.Add(oct);
                            break;

                        case EnumModbusIntegralUnit.Word:
                            var decShort = BitConverter.ToInt16(tempArray, 0);
                            var octShort = int.Parse(Convert.ToString(decShort, 8));
                            resultList.Add(octShort);
                            break;

                        case EnumModbusIntegralUnit.DWord:
                            var decInt = BitConverter.ToInt32(tempArray, 0);
                            var octInt = long.Parse(Convert.ToString(decInt, 8));
                            resultList.Add(octInt);
                            break;

                        case EnumModbusIntegralUnit.QWord:
                            var decLong = BitConverter.ToInt32(tempArray, 0);
                            var octLong = long.Parse(Convert.ToString(decLong, 8));
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
            var length = (int)unit;
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
                    var result = BitConverter.ToString(tempArray).Replace("-", "");
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
            var length = (int)unit;
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
                    if (BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(tempArray);
                    }

                    var bin = "";
                    switch (unit)
                    {
                        case EnumModbusIntegralUnit.Byte:
                            bin = Convert.ToString(tempArray[0], 2).PadLeft(8, '0');
                            break;

                        case EnumModbusIntegralUnit.Word:
                            var decShort = BitConverter.ToInt16(tempArray, 0);
                            bin = Convert.ToString(decShort, 2).PadLeft(16, '0');
                            break;

                        case EnumModbusIntegralUnit.DWord:
                            var decInt = BitConverter.ToInt32(tempArray, 0);
                            bin = Convert.ToString(decInt, 2).PadLeft(32, '0');
                            break;

                        case EnumModbusIntegralUnit.QWord:
                            var decLong = BitConverter.ToInt64(tempArray, 0);
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
            var length = 4;
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

            for (int i = 0; i < count * length; i = i + length)
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    memory.Write(ResultArray, i + 1, 1);
                    memory.Write(ResultArray, i, 1);
                    memory.Write(ResultArray, i + 3, 1);
                    memory.Write(ResultArray, i + 2, 1);
                    var resultArray = memory.ToArray();
                    float result = BitConverter.ToSingle(resultArray, 0);
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
            List<double> resultList = new List<double>();

            for (int i = 0; i < count * length; i = i + length)
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    memory.Write(ResultArray, i + 1, 1);
                    memory.Write(ResultArray, i, 1);
                    memory.Write(ResultArray, i + 3, 1);
                    memory.Write(ResultArray, i + 2, 1);

                    memory.Write(ResultArray, i + 5, 1);
                    memory.Write(ResultArray, i + 4, 1);
                    memory.Write(ResultArray, i + 7, 1);
                    memory.Write(ResultArray, i + 6, 1);
                    var resultArray = memory.ToArray();
                    double result = BitConverter.ToDouble(resultArray, 0);
                    resultList.Add(result);
                }
            }

            return resultList;
        }
    }
}