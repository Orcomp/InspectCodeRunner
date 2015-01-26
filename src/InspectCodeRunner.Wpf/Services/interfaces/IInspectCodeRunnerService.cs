namespace InspectCodeRunner.Wpf.Services
{
    using System.Threading.Tasks;

    public interface IInspectCodeRunnerService
    {
        Task Run(string fileName, string arguments);
        void CreateDirectoryIfNotExists(string path);
        Task TransformToHtmlReport(string inspectResultPath, string htmlReportPath);
    }
}