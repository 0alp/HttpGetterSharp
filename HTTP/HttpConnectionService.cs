using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace http_client_c_sharp.HTTP
{
    /// <summary>
    /// Provides functionality to create, send, and receive HTTP requests over a TCP network connection.
    /// </summary>
    internal class HttpConnectionService
    {
        readonly IPAddress _address;
        readonly int _port = 80;
        NetworkStream _stream;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpConnectionService"/> class with the specified IP address.
        /// </summary>
        /// <param name="ipAddress">The IP address of the HTTP server.</param>
        public HttpConnectionService(IPAddress ipAddress)
        {
            _address = ipAddress;
            

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpConnectionService"/> class with the specified Domain name.
        /// </summary>
        /// <param name="domainName">The Domain name of the HTTP server.</param>
        public HttpConnectionService(string domainName)
        {
            
            _address = Dns.GetHostAddresses(domainName)[0];

        }

        /// <summary>
        /// Asynchronously creates an HTTP connection to port 80 of the specified IP address.
        /// </summary>
        /// <returns>Returns true if the connection is successfully established; otherwise, false.</returns>
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
        /// <summary>
        /// Asynchronously sends an HTTP request message over the established connection.
        /// </summary>
        /// <param name="requestMessage">The HTTP request message to be sent.</param>
        /// <exception cref="InvalidOperationException">Thrown when the connection is not established or the stream is not writable.</exception>
        public async Task SendHttpRequest(string requestMessage)
        {
            if (_stream == null || !_stream.CanWrite)
                throw new InvalidOperationException("Connection is not established or stream is not writable.");

            byte[] buffer = Encoding.UTF8.GetBytes(requestMessage);

            await _stream.WriteAsync(buffer, 0, buffer.Length);
            await _stream.FlushAsync();





        }
        /// <summary>
        /// Asynchronously receives an HTTP response message from the established connection.
        /// </summary>
        /// <returns>Returns the received HTTP response message as a string.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the connection is not established or the stream is not readable.</exception>
        public async Task<string> ReceiveHttpResponse()
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


            return Encoding.UTF8.GetString(buffer);




        }


    }
}
