using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace socket_client
{
    class Program
    {
        private static byte[] result = new byte[1024];

        static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse("192.168.25.9");
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                clientSocket.Connect(new IPEndPoint(ip, 1001));
                Console.WriteLine("Conexão bem sucedida");
            }
            catch
            {
                Console.WriteLine("Falha na conexão ao servidor. Pressione Enter para sair!");
                return;
            }

            int receiveLength = clientSocket.Receive(result);
            Console.WriteLine("Mensagem recebida: {0}", Encoding.ASCII.GetString(result, 0, receiveLength));
            for(int i=0; i<10; i++)
            {
                try
                {
                    Thread.Sleep(1000);
                    string sendMessage = "testando... " + DateTime.Now; //example
                    clientSocket.Send(Encoding.ASCII.GetBytes(sendMessage));
                    Console.WriteLine("Mensagem enviada：" + sendMessage);
                }
                catch
                {
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                    break;
                }

            }

            Console.WriteLine("Conexão encerrada, pressione enter para sair");
            Console.ReadLine();
        }
    }
}
