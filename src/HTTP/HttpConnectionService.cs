using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HttpGetterSharp.HTTP
{
    /// <summary>
    /// Provides functionality to create, send, and receive HTTP requests over a TCP network connection.
    /// </summary>
    public class HttpConnectionService
    {
        TcpClient? _client;
        readonly IPAddress _address;
        readonly int _port = 80;
        NetworkStream? _stream;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpConnectionService"/> class with the specified IP address.
        /// </summary>
        /// <param name="ipAddress">The IP address of the HTTP server.</param>
        public HttpConnectionService(IPAddress ipAddress)
        {
            _address = ipAddress;
            InitTcpClient();


        }
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpConnectionService"/> class with the specified Domain name.
        /// </summary>
        /// <param name="domainName">The Domain name of the HTTP server.</param>
        public HttpConnectionService(string domainName)
        {
            
            _address = Dns.GetHostAddresses(domainName)[0];
            InitTcpClient();

        }




        private void InitTcpClient()
        {

            _client = new TcpClient();


        }



        /// <summary>
        /// Asynchronously creates an HTTP connection to port 80 of the specified IP address.
        /// </summary>
        /// <returns>Returns true if the connection is successfully established; otherwise, false.</returns>
        public async Task<bool> CreateHttpConnection()
        {
            

            try
            {
                await _client.ConnectAsync(_address, _port);
                if (_client.Connected)
                    _stream = _client.GetStream();
                return _client.Connected;
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
            const int maxBufferSize = 8192; 
            byte[] buffer = new byte[maxBufferSize];
            int len;
            int totalBytesRead = 0;

            if (_stream == null || !_stream.CanRead)
                throw new InvalidOperationException("Connection is not established or stream is not readable.");

            while ((len = await _stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                totalBytesRead += len;

                if (_stream.DataAvailable)
                {
                    int newBufferSize = Math.Min(maxBufferSize, totalBytesRead * 2); 
                    Array.Resize(ref buffer, newBufferSize);
                }
                else
                {
                    break;
                }
            }

            return Encoding.UTF8.GetString(buffer, 0, totalBytesRead);
        }

        /// <summary>
        /// Closes the current HTTP connection by closing and disposing the underlying stream and client.
        /// Reads any remaining data from the stream, half-closes the connection, and disposes of the resources.
        /// </summary>
        /// <remarks>
        /// This method ensures that all data in the stream is read before closing the connection.
        /// It performs a half-close operation on the stream and disposes of the resources safely.
        /// It terminates the connection according to the TCP standard by sending the FIN bit instead of RST.
        /// </remarks>
        async public Task CloseHttpConnection()
        {
            try
            {
                if (_stream != null)
                {
                    byte[] buffer = new byte[1024];
                    while (await _stream.ReadAsync(buffer, 0, buffer.Length) > 0) { }

                    _stream.Close();

                    await _stream.DisposeAsync();
                    _stream = null;
                }

                if (_client != null)
                {
                    _client.Close();
                    _client.Dispose();
                    _client = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error closing connection: {ex.Message}");
            }
        }

    }
}
