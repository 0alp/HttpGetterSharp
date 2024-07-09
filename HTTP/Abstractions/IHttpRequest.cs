using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace http_client_c_sharp.HTTP.Abstractions
{
    /// <summary>
    /// Defines the methods required to build HTTP request messages.
    /// </summary>
    internal interface IHttpRequest
    {


        /// <summary>
        /// Builds an HTTP request message for the specified host.
        /// </summary>
        /// <param name="host">The host for which the request is being built.</param>
        /// <returns>A string representing the HTTP request message.</returns>
        string BuildRequest(string host);








    }
}
