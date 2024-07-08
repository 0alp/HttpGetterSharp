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


        public StringBuilder BuildRequest(string host) 
        {

            StringBuilder request = new StringBuilder();    
            request.Append("GET / HTTP/1.1 \r\n" +
                  $"Host: {host} \r\n");

            

            return request;
        }

        public StringBuilder BuildRequest(string host, string httpVersion, Dictionary<string, string> headers = null)
        {

            StringBuilder request = new StringBuilder();
            request.Append("GET / HTTP/1.1 \r\n" +
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
