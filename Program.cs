using System.Net;
using System.Net.Sockets;

Console.WriteLine("Введите айпи или домен: ");
string host = Console.ReadLine() ?? "";

try
{
    IPAddress[] addresses = Dns.GetHostAddresses(host);
    string targetIP = addresses[0].ToString();

    Console.WriteLine($"Сканириую {host} ({targetIP})");

    ParallelOptions options = new ParallelOptions { MaxDegreeOfParallelism = 50 };

    Parallel.For(1, 1025, options, port =>
    {
        using TcpClient client = new TcpClient();
        try
        {
            if (client.ConnectAsync(targetIP, port).Wait(1000))
            {
                Console.WriteLine($"|!| Открыт порт: {port}");
            }
        }
        catch { }
    });
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

Console.WriteLine("Готово, нажмите Enter");
Console.ReadLine();