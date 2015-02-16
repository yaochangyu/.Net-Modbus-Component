using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tako.Component.Extension;
using Tako.Modbus.Core;

namespace Tako.Modbus.Client.UnitTest
{
    internal static class TestUtility
    {
        internal const string ASSEMBLY_NAME = "Tako.Modbus.Client, Version=1.0.0.3, Culture=neutral, PublicKeyToken=14a8cde7761e3395";
        internal static ModbusUtility s_modbusUtility = null;
        internal static Assembly s_assembly = null;
        internal static Dictionary<string, dynamic> s_pool = null;

        public static ModbusUtility CreateModbusUtility()
        {
            if (s_modbusUtility == null)
            {
                s_modbusUtility = new ModbusUtility();
            }
            return s_modbusUtility;
        }

        public static dynamic GenerateInstance(string className)
        {
            if (s_assembly == null)
            {
                s_assembly = Assembly.Load(TestUtility.ASSEMBLY_NAME);
            }
            if (s_pool == null)
            {
                s_pool = new Dictionary<string, dynamic>();
            }
            if (!s_pool.ContainsKey(className))
            {
                //取得組件類別
                var assemblyType = s_assembly.GetType(className);
                //建立執行個體
                dynamic instance = Activator.CreateInstance(assemblyType).AsTransparentDynamicObject();
                s_pool.Add(className, instance);
                return instance;
            }
            else
            {
                dynamic instance = s_pool[className];
                return instance;
            }
        }
    }
}