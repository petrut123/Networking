using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ServerApp
{
    class Server
    {
        public static string data;

        public static void StartListening()
        {
            byte[] bytes = new byte[1024];

            IPHostEntry ipHostEntry = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = IPAddress.Loopback;//ipHostEntry.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 1234);

            Socket ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            try
            {
                ServerSocket.Bind(ipEndPoint);
                ServerSocket.Listen(10);
                while (true)
                {
                    Console.WriteLine("Waiting for a connection");
                    Socket ClientSocket = ServerSocket.Accept();

                    data = null;

                    int bytesRec = ClientSocket.Receive(bytes);

                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);

                    Console.WriteLine("Text received: {0}", data);
                    data += " this is from the server";

                    byte[] msg = Encoding.ASCII.GetBytes(data);

                    ClientSocket.Send(msg);
                    ClientSocket.Shutdown(SocketShutdown.Both);
                    ClientSocket.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress Enter to continue...");
            Console.Read();
        }

        public static void ReceiveFrom()
        {
            IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 1234);

            Socket serverSocket = new Socket(IPAddress.Loopback.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
            
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint senderRemote = (EndPoint)sender;

            serverSocket.Bind(endPoint);

            byte[] msg = new byte[1024];
            Console.WriteLine("Waiting to receive datagrams from client...");
            int bytesRec = serverSocket.ReceiveFrom(msg, msg.Length, SocketFlags.None, ref senderRemote);
            //int bytesRec = serverSocket.Receive(msg);
            string data = Encoding.ASCII.GetString(msg, 0, bytesRec);
            Console.WriteLine(data);
            serverSocket.Close();
        }

        static void Main(string[] args)
        {
            //StartListening();
            while (true)
            {
                ReceiveFrom();
            }
            
        }
    }
}
