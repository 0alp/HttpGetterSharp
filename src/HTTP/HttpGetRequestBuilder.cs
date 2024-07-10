using HttpGetterSharp.HTTP.Abstractions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpGetterSharp.HTTP
{
    /// <summary>
    /// Provides functionality to build HTTP GET request messages.
    /// </summary>
    internal class HttpGetRequestBuilder : IHttpRequest
    {


        /// <summary>
        /// Builds an HTTP GET request message with the specified host.
        /// Includes default headers such as User-Agent, Accept, Accept-Language, Accept-Encoding,
        /// Connection, Upgrade-Insecure-Requests, and Priority.
        /// </summary>
        /// <param name="host">The host to include in the HTTP request message.</param>
        /// <returns>The HTTP GET request message as a StringBuilder.</returns>
        public StringBuilder BuildRequest(string host)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("GET / HTTP/1.1\r\n");
            stringBuilder.Append($"Host: {host}\r\n");

            stringBuilder.AppendLine($"User-Agent: zeroalp/1.0 ({Environment.OSVersion.VersionString}; {(Environment.Is64BitOperatingSystem ? "x64" : "x86")};\r\n");
            stringBuilder.AppendLine("Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8\r\n");
            stringBuilder.AppendLine($"Accept-Language: {CultureInfo.CurrentCulture.Name},en;q=0.8,en-US;q=0.5,en;q=0.3\r\n");
            stringBuilder.AppendLine("Accept-Encoding: gzip, deflate\r\n");
            stringBuilder.AppendLine("Connection: keep-alive\r\n");
            stringBuilder.AppendLine("Upgrade-Insecure-Requests: 1\r\n");
            stringBuilder.AppendLine("Priority: u=1\r\n");

            return stringBuilder;
        }


        /// <summary>
        /// Builds an HTTP GET request message with the specified host and optional headers.
        /// </summary>
        /// <param name="host">The host to include in the HTTP request message.</param>
        /// <param name="httpVersion">The HTTP version to include in the HTTP request message.</param>
        /// <param name="headers">Optional headers to include in the HTTP request message.</param>
        /// <returns>The HTTP GET request message as a StringBuilder.</returns>
        public StringBuilder BuildRequest(string host, string httpVersion, Dictionary<string, string>? headers = null)
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
