﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpGetterSharp.HTTP.Abstractions
{
    /// <summary>
    /// Defines the methods required to build HTTP request messages.
    /// </summary>
    public interface IHttpRequest
    {


        /// <summary>
        /// Builds an HTTP request message for the specified host.
        /// </summary>
        /// <param name="host">The host for which the request is being built.</param>
        /// <returns>A StringBuilder representing the HTTP request message.</returns>
        StringBuilder BuildRequest(string host);








    }
}
