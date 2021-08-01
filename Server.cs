using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace UDP_Server_Client_System
{
    class Server
    {
        static void Main(string[] args)
        {
            Thread UDP_sender = new Thread(UDPServerSender);
            UDP_sender.Start();
        }
        static public void UDPServerSender()
        {
            while (true)
            {
                string message = "The UDP Protocol sends this message.";
                UdpClient client = new UdpClient();
                try
                {
                    client.Client.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
                    IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Broadcast, 11000);
                    byte[] bytes = Encoding.ASCII.GetBytes(message);
                    client.Send(bytes, bytes.Length, ipEndPoint);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    client.Close();
                    Console.WriteLine("Sent Broadcast Message: " + message + "\n");
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
