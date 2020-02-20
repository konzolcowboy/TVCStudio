namespace TVC.Computer
{
    public class BuildEventArgs
    {
        public BuildEventArgs(string message)
        {
            Message = message;
        }
        public string Message { get; }
    }
}
