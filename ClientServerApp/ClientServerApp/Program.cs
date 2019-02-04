using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ClientServerApp
{
    class Client
    {
        public static void StartClient()
        {
            byte[] bytes = new byte[1024];

            try
            {
                IPHostEntry ipHostEntry = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = IPAddress.Loopback;
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 1234);

                Socket senderSocket = new Socket(ipAddress.AddressFamily, SocketType.Dgram, ProtocolType.Udp);

                try
                {
                    senderSocket.Connect(ipEndPoint);
                    Console.WriteLine("Socket Connected to {0}", senderSocket.RemoteEndPoint.ToString());

                    string sMessage = Console.ReadLine();

                    byte[] msg = Encoding.ASCII.GetBytes(sMessage);

                    int bytesSent = senderSocket.Send(msg);

                    //int bytesRec = senderSocket.Receive(bytes);
                    //Console.WriteLine("Enchoed test = {0}", Encoding.ASCII.GetString(bytes, 0, bytesRec));

                    senderSocket.Shutdown(SocketShutdown.Both);
                    senderSocket.Close();
                    //StartClient();
                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }
        }

        static void Main(string[] args)
        {
            while (true)
            {
                StartClient();
            }
            Console.WriteLine("Client shuting down");
            Console.ReadLine();
        }
    }
}
