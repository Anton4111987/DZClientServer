// клиент 
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

const string ip = "127.0.0.1";
const int port = 8080;
IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
string message = " ";
while(message!="")
{
    Console.WriteLine("Введите текст сообщения: ");
    message = Console.ReadLine();
    var data = Encoding.UTF8.GetBytes(message);
    clientSocket.Connect(ep);
    clientSocket.Send(data); // отправка данных

    var buffer = new byte[256];
    // int size = 0;
    var answer = new StringBuilder();

    do
    {
        clientSocket.Receive(buffer);// для клиента
        answer.Append(Encoding.UTF8.GetString(buffer,0, 1));
        
    } while (clientSocket.Available > 0);

    Console.WriteLine(answer.ToString());


}
clientSocket.Shutdown(SocketShutdown.Both);
clientSocket.Close();
Console.ReadLine();