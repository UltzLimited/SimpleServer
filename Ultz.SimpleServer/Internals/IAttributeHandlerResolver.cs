﻿#region

using System;
using System.Collections.Generic;
using Ultz.SimpleServer.Handlers;

#endregion

namespace Ultz.SimpleServer.Internals
{
    public interface IAttributeHandlerResolver
    {
        IEnumerable<IHandler> GetHandlers(Type type);
    }
}