using System;

namespace TVCStudio.Trace
{
    public enum TraceSeverity
    {
        Info,
        Warning,
        Error
    }

    public sealed class TraceMessage
    {
        public TraceMessage(TraceSeverity severity, string messageText, DateTime timeStamp)
        {
            Severity = severity;
            MessageText = messageText;
            TimeStamp = timeStamp;
        }
        public TraceSeverity Severity { get; }
        public string MessageText { get; }
        public DateTime TimeStamp { get; }

        public override string ToString()
        {
            return $"{TimeStamp:yyyy-MM-dd hh:mm:ss}\t{Severity}\t{MessageText}\n";
        }
    }
}
