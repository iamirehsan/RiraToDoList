namespace RiraToDoList.Domain.LogService.@interface
{
    public interface ILoggerService
    {
        public void WriteError(string message, Exception ex);
        public void WriteWarning(string message);
        public void WriteInfo(string message);
        public void WriteDebug(string message);
    }
}
