using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace TypeB2
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] msg = new byte[1000];
            bool cont = true;

            System.Net.IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9051);
            UdpClient udpClient = new UdpClient(5090);
            udpClient.Connect("Matan-NB", 5090);
            //Byte[] sendBytes = Encoding.ASCII.GetBytes("Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there ");
            Byte[] sendBytesConnect = Encoding.ASCII.GetBytes("CONNECTED\r\n");
            Byte[] sendBytesDisconnect = Encoding.ASCII.GetBytes("DISCONNECTED\r\n");
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            try
            {
                Console.WriteLine(sendBytesConnect);
                udpClient.Send(sendBytesConnect, sendBytesConnect.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
           
            do
            {
               
                cont = true;
               
                try
                {
                    Console.WriteLine("byte[] recData = udpClient.Receive(ref RemoteIpEndPoint);");
                    byte[] recData = udpClient.Receive(ref RemoteIpEndPoint);
                    Console.WriteLine("Rec data");
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.ToString());
                }
                //Thread.Sleep(1000);
            } while (cont);
        }

    }
}

