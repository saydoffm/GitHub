using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
namespace TypeA
{
    class Program
    {
        static void Main(string[] args)
        {
            //System.Net.Sockets.UdpClient client = new System.Net.Sockets.UdpClient(9879);
            byte[] msg = new byte[1000];
            bool cont = true;
            
            //System.Net.IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9051);
            UdpClient udpClient = new UdpClient(9050);
            udpClient.Connect("Matan-NB", 9050);
            Byte[] sendBytes = Encoding.ASCII.GetBytes("Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there Is anybody there ");
            try
            {
                udpClient.Send(sendBytes, sendBytes.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            do
            {
                cont = true;
               try {
                    udpClient.Send(sendBytes, sendBytes.Length);
                    Console.WriteLine("send");
                }
            catch (Exception e)
                {

                    Console.WriteLine(e.ToString());
                }
                Thread.Sleep(1000);
            } while (cont);
        }

        private static byte[] IncrementData(byte[] msg)
        {
            for(int i = 0; i < msg.Length;++i)
            {
                msg[i] = ++msg[i] ;
            }
            return msg;
        }
    }
}
