using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tako.Modbus.Client
{
    public interface IModbusSocket : IDisposable
    {
        int SendTimeout { get; set; }

        int ReceiveTimeout { get; set; }

        int RetryTimeInterval { get; set; }

        int RetryTime { get; set; }

        bool IsConnected { get; set; }

        bool Connect<T>(T ConnectConfig);

        bool Disconnect();

        int Send(byte[] requestArray);

        byte[] Receive();
    }
}