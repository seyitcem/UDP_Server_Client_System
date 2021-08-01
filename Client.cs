using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace UDP_Server_Client_System
{
    class Client
    {
        static void Main(string[] args)
        {
            Thread UDP_listener = new Thread(UDPListener);
            UDP_listener.Start();
        }
        private static void UDPListener()
        {
            UdpClient listener = new UdpClient();
            listener.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 11000);
            try
            {
                listener.Client.Bind(ipEndPoint);
                Console.WriteLine("Connected to the UDP server.");
                while (true)
                {
                    byte[] bytes = listener.Receive(ref ipEndPoint);
                    string message = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                    Console.WriteLine($"Received broadcast from {ipEndPoint} : {message}\n");
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("UDP Connection Error.");
                Console.WriteLine(e.Message);
            }
            finally
            {
                listener.Close();
            }
        }
    }
}
