// Сервер

using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

const string ip = "127.0.0.1";
const int port = 8080;

IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
while (true)
{
    try
    {
        var serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        serverSocket.Bind(serverEndPoint);
        serverSocket.Listen(5);

        Console.WriteLine("Сервер запущен. Ожидание подключений... ");

        while (true)
        {

            var listener = serverSocket.Accept();// для сервера
            var buffer = new byte[256];
            int size;
            var data = new StringBuilder();

            do
            {

                size = listener.Receive(buffer);
                data?.Append(Encoding.UTF8.GetString(buffer, 0, size));
                if (data == null)
                    Console.WriteLine("gecnf  строка пришла"); 

            } while (serverSocket.Available > 0);

            Console.WriteLine("Сервер получает: " + data);
            listener.Send(Encoding.UTF8.GetBytes($"Сервер получил ({data}) и отвечает: \nПривет клиент"));

                listener.Shutdown(SocketShutdown.Both);
            listener.Close();
            Console.WriteLine("Сервер закончил работу.");

        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}














