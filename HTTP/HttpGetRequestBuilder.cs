using http_client_c_sharp.HTTP.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace http_client_c_sharp.HTTP
{
    internal class HttpGetRequestBuilder : IHttpRequest
    {


        public string BuildRequest(string host) 
        {
            return $"GET / HTTP/1.1\r\n"+
                 $"Host: {host}\r\n";
            
            
            
            

            
        }
        public string BuildRequest(string host, bool includeDefaultHeaders)
        {
            
            string requestMessage = $"GET / HTTP/1.1\r\n" +
                 $"Host: {host}\r\n";


            if (includeDefaultHeaders)
            {
                requestMessage = requestMessage +
                     "User-Agent: zeroalp/0.1 (Windows NT 10.0; Win64; x64; rv:127.0) Gecko/20100101 Firefox/127.0\r\n" +
                     "Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8\r\n" +
                     "Accept-Language: en-US,en;q=0.8,en-US;q=0.5,en;q=0.3\r\n" +
                     "Accept-Encoding: gzip, deflate\r\n" +
                     "Connection: keep-alive\r\n" +
                     "Upgrade-Insecure-Requests: 1\r\n" +
                     "Priority: u=1\r\n" +
                     "\r\n";


            }
            
            return requestMessage;
        }


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
