using System;
using System.Net;
using System.Net.Sockets;
using ZplLabels.Common.Extensions;

namespace ZplLabels
{
    /// <summary>
    /// Summary description for NetworkPrinter.
    /// </summary>
    public class NetworkPrinter
    {
        private const int Port = 9100; //for ZEBRA PRINTERs

        private const int TimeOut = 30;

        public bool Print(IPAddress ipAddress, string scriptStr)
        {
            try
            {
                var buff = scriptStr.ToByteArray();

                using (var tcp = new TcpClient(ipAddress.ToString(), Port) {ReceiveTimeout = TimeOut})
                using (var stream = tcp.GetStream())
                {
                    stream.Write(buff, 0, buff.Length);
                }

                return true;
            }
            catch (Exception e)
            {
                throw new ApplicationException("Error Printing Label", e);
            }
        }
    }
}