namespace InspectCodeRunner.Wpf.Services
{
    public interface IInspectCodeRunnerService
    {
        void Run(string fileName, string arguments);
        void CreateDirectoryIfNotExists(string path);
    }
}