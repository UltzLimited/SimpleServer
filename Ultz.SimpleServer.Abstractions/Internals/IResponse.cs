﻿// IResponse.cs - Ultz.SimpleServer.Abstractions
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

using System.IO;

#endregion

namespace Ultz.SimpleServer.Internals
{
    /// <summary>
    ///     Represents a response
    /// </summary>
    public interface IResponse
    {
        /// <summary>
        ///     Data associated with this response.
        /// </summary>
        Stream OutputStream { get; }

        /// <summary>
        ///     Formats this response, then sends it to the underlying connection and closes it. This method can also forcibly
        ///     terminate the underlying connection without sending the response.
        /// </summary>
        /// <param name="mode">what should be done with the response</param>
        void Close(CloseMode mode = CloseMode.Graceful);
    }
}