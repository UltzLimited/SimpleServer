﻿// HttpOneResponse.cs - Ultz.SimpleServer.Minimal
// 
// Copyright (C) 2018 Ultz Limited
// 
// This file is part of SimpleServer.
// 
// SimpleServer is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// SimpleServer is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with SimpleServer. If not, see <http://www.gnu.org/licenses/>.

#region

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ultz.SimpleServer.Internals.Http;

#endregion

namespace Ultz.SimpleServer.Internals.Http1
{
    /// <inheritdoc />
    public class HttpOneResponse : HttpResponse
    {
        private readonly IConnection _connection;
        private readonly HttpRequest _request;

        /// <inheritdoc />
        public HttpOneResponse(HttpRequest request, IConnection conection)
        {
            _connection = conection;
            _request = request;
        }

        /// <inheritdoc />
        public override void Close(CloseMode mode = CloseMode.Graceful)
        {
            foreach (var cookie in Cookies)
                Headers.Add("set-cookie",cookie.ToString());
            if (mode == CloseMode.Force) _connection.Close();
            if (Headers.TryGetValue("server", out var val))
                Headers["server"] = "SimpleServer/1.0 " + val;
            else
                Headers["server"] = "SimpleServer/1.0";
            const string crlf = "\r\n";
            var response = "";
#pragma warning disable 618
#pragma warning disable 612
            response += _request.Protocol + " " + StatusCode + " " + ReasonPhrase + crlf;
#pragma warning restore 618
#pragma warning restore 612
            foreach (var header in Headers)
                response += header.Name + ": " + header.Value + crlf;
            response += crlf;
            // write headers
            var bytes = Encoding.UTF8.GetBytes(response);
            _connection.Stream.Write(bytes, 0, bytes.Length);
            // write response
            bytes = ((MemoryStream) OutputStream).ToArray();
            _connection.Stream.Write(bytes, 0, bytes.Length);
            // close the connection
            if (mode != CloseMode.KeepAlive)
                _connection.Close();
        }
    }
}