﻿using SimpleServer.Internals;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace SimpleServer.Logging
{
    public class Log : ILog
    {
        public const string LogPrefix = "[%time%] [%source%%severity%]: ";
        internal Log(Type type) { source = type.Name; }
        internal Log(string prefix) { source = prefix; }
        #region Statics
        internal static List<TextWriter> Writers { get; set; }
        internal static void WriteLine(string s)
        {
            Info(s);
        }
        internal static void Info(string s)
        {
            Write("", "INFO",s);
        }
        internal static void Warn(string s)
        {
            Write("", "WARN",s);
        }
        internal static void Severe(string s)
        {
            Write("", "SEVERE",s);
        }
        internal static void Error(Exception ex)
        {
            Write("", "ERROR",ex.ToString());
        }
        internal static void Error(string s)
        {
            Write("", "ERROR", s);
        }
        internal static void Write(string source,string severity,string content)
        {
            foreach (TextWriter writer in Writers)
            {
                writer.WriteLine(LogPrefix.Replace("%time%",DateTime.Now.ToString("HH:mm:ss")).Replace("%source%",source+(string.IsNullOrEmpty(source) ? "" : "/")).Replace("%severity%",severity)+content);
            }
        }
        public static ILog GetLogger()
        {
            StackFrame frame = new StackFrame(1);
            var method = frame.GetMethod();
            var type = method.DeclaringType;
            var name = method.Name;
            return new Log(type);
        }
        public static void AddWriter(TextWriter writer)
        {
            Writers.Add(writer);
        }
        internal static ILog GetLogger(SimpleServerHost host)
        {
            return new Log(host.FQDN);
        }
        #endregion
        internal string source;
    }
}
