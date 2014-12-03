namespace InspectCodeRunner.Wpf.Services
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Windows;
    using Catel;
    using Catel.IoC;
    using Catel.Logging;
    using Orchestra.Services;

    public class InspectCodeRunnerService : IInspectCodeRunnerService
    {
        private readonly IServiceLocator _serviceLocator;

        public InspectCodeRunnerService()
        {
            _serviceLocator = ServiceLocator.Default;
        }

        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public void CreateDirectoryIfNotExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public void Run(string fileName, string arguments)
        {
            ResetLog();
            StartProcess(fileName, arguments);
        }

        private void StartProcess(string fileName, params string[] arguments)
        {
            Argument.IsNotNullOrWhitespace(() => fileName);
            Argument.IsNotNull(() => arguments);

            var argumentsString = string.Join(" ", arguments);
            Log.Info("Starting '{0} {1}'", fileName, argumentsString);

            var processStartInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = argumentsString,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
            };

            var workingDirectory = Path.GetDirectoryName(fileName);
            if (Directory.Exists(workingDirectory))
            {
                processStartInfo.WorkingDirectory = workingDirectory;
            }

            using (var process = new Process())
            {
                process.StartInfo = processStartInfo;
                process.EnableRaisingEvents = true;
                process.OutputDataReceived += (_, e) => Log.Info(e.Data);
                process.ErrorDataReceived += (_, e) => Log.Error(e.Data);
                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
            }
        }

        private void ResetLog()
        {
            var logControlService = _serviceLocator.ResolveType<ILogControlService>();

            Application.Current.Dispatcher.Invoke((Action) (() => { logControlService.Clear(); }));
        }
    }
}
