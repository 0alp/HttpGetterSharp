using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace http_client_c_sharp.HTTP
{
    internal class HttpConnectionService
    {
        readonly IPAddress _address;
        readonly int _port;
        NetworkStream _stream;
        public string ResponseMessage { get; set; }
        public HttpConnectionService(IPAddress ipAddress, int port = 80)
        {
            _address = ipAddress;
            _port = port;

        }

        public HttpConnectionService(string domainName, int port = 80)
        {
            
            _address = Dns.GetHostAddresses(domainName)[0];
            _port = port;

        }


        public async Task<bool> CreateHttpConnection()
        {
            TcpClient client;

            client = new TcpClient();
            try
            {
                await client.ConnectAsync(_address, _port);
                if (client.Connected)
                    _stream = client.GetStream();
                return client.Connected;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting: {ex.Message}");
                return false;
            }







        }

        public async Task SendHttpRequest(string requestMessage)
        {
            if (_stream == null || !_stream.CanWrite)
                throw new InvalidOperationException("Connection is not established or stream is not writable.");

            byte[] buffer = Encoding.UTF8.GetBytes(requestMessage);

            await _stream.WriteAsync(buffer, 0, buffer.Length);
            await _stream.FlushAsync();





        }
        public async Task<byte[]> ReceiveHttpResponse()
        {

            byte[] buffer = new byte[1024];
            int len;
            int totalBytesRead = 0;
            if (_stream == null || !_stream.CanRead)
                throw new InvalidOperationException("Connection is not established or stream is not readable.");

            while ((len = await _stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {

                await _stream.WriteAsync(buffer, 0, len);
                totalBytesRead += len;
                if (!_stream.DataAvailable)
                    break;
            }


            return buffer;




        }


    }
}
