using System;
using System.Collections.Generic;

namespace TVCStudio.Trace
{
    internal static class TraceEngine
    {
        private static readonly List<ITraceClient> SubscribedClients = new List<ITraceClient>();

        public static void Subscribe(ITraceClient client)
        {
            if (!SubscribedClients.Contains(client))
            {
                SubscribedClients.Add(client);
            }
        }

        private static void Trace(TraceSeverity severity, string messageText)
        {
            TraceMessage message = new TraceMessage(severity, messageText, DateTime.Now);
            SubscribedClients.ForEach(client => client.TraceMessageRecieved(message));
        }

        public static void TraceError(string messageText)
        {
            Trace(TraceSeverity.Error, messageText);
        }

        public static void TraceInfo(string messageText)
        {
            Trace(TraceSeverity.Info, messageText);
        }

        public static void TraceWarning(string messageText)
        {
            Trace(TraceSeverity.Warning, messageText);
        }

        public static void UnSubscribe(ITraceClient client)
        {
            if (SubscribedClients.Contains(client))
            {
                SubscribedClients.Remove(client);
            }
        }
    }
}
