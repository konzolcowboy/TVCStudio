namespace TVCStudio.Trace
{
    interface ITraceClient
    {
        void TraceMessageRecieved(TraceMessage newMessage);
    }
}
