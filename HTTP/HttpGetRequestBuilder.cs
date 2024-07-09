using http_client_c_sharp.HTTP.Abstractions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace http_client_c_sharp.HTTP
{
    /// <summary>
    /// Provides functionality to build HTTP GET request messages.
    /// </summary>
    internal class HttpGetRequestBuilder : IHttpRequest
    {

        /// <summary>
        /// Builds a basic HTTP GET request message with the specified host.
        /// </summary>
        /// <param name="host">The host to include in the HTTP request message.</param>
        /// <returns>The HTTP GET request message as a string.</returns>
        public string BuildRequest(string host) 
        {
            return $"GET / HTTP/1.1\r\n"+
                 $"Host: {host}\r\n";
            
            
            
            

            
        }
        /// <summary>
        /// Builds an HTTP GET request message with the specified host and optionally includes default headers.
        /// </summary>
        /// <param name="host">The host to include in the HTTP request message.</param>
        /// <param name="includeDefaultHeaders">Indicates whether to include default headers in the HTTP request message.</param>
        /// <returns>The HTTP GET request message as a string.</returns>
        public string BuildRequest(string host, bool includeDefaultHeaders)
        {
            string requestMessage = $"GET / HTTP/1.1\r\n" +
                 $"Host: {host}\r\n";


            if (includeDefaultHeaders)
            {
                requestMessage = requestMessage +
                     $"User-Agent: zeroalp/0.1 ({Environment.OSVersion.VersionString}; {(Environment.Is64BitOperatingSystem ? "x64" : "x86")}; ) \r\n" +
                     "Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8\r\n" +
                     $"Accept-Language: {CultureInfo.CurrentCulture.Name},en;q=0.8,en-US;q=0.5,en;q=0.3\r\n" +
                     "Accept-Encoding: gzip, deflate\r\n" +
                     "Connection: keep-alive\r\n" +
                     "Upgrade-Insecure-Requests: 1\r\n" +
                     "Priority: u=1\r\n" +
                     "\r\n";


            }
            
            return requestMessage;
        }

        /// <summary>
        /// Builds an HTTP GET request message with the specified host, HTTP version, etc. optional headers.
        /// </summary>
        /// <param name="host">The host to include in the HTTP request message.</param>
        /// <param name="httpVersion">The HTTP version to include in the HTTP request message.</param>
        /// <param name="headers">Optional headers to include in the HTTP request message.</param>
        /// <returns>The HTTP GET request message as a <see cref="StringBuilder"/>.</returns>
        public StringBuilder BuildRequest(string host, string httpVersion, Dictionary<string, string> headers = null)
        {

            StringBuilder request = new StringBuilder();
            request.Append("GET / HTTP/1.1\r\n" +
                  $"Host: {host} \r\n");

            if (headers != null)
            {
                foreach(var header in headers)
                {
                    request.AppendLine($"{header.Key}: {header.Value}");

                }

            }

            return request;


        }


    }
}
