using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InspectCodeRunner.Wpf.Models
{
    using System.Diagnostics;
    using System.IO;
    using Catel;
    using Catel.Data;
    using Catel.Logging;
    using Path = Catel.IO.Path;

    public class InspectCodeAppIntegration : ModelBase
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public InspectCodeAppIntegration()
        {
        }

        public bool CanExecute(string path)
        {
            return File.Exists(path);
        }

        public void Execute(string path)
        {
        }

        protected virtual void StartProcess(string fileName, params string[] arguments)
        {
            Argument.IsNotNullOrWhitespace(() => fileName);
            Argument.IsNotNull(() => arguments);

            var argumentsString = string.Join(" ", arguments);
            Log.Info("Starting '{0} {1}'", fileName, argumentsString);

            var processStartInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = argumentsString,
            };

            var workingDirectory = Path.GetDirectoryName(fileName);
            if (Directory.Exists(workingDirectory))
            {
                processStartInfo.WorkingDirectory = workingDirectory;
            }

            Process.Start(processStartInfo);
        }
    }
}
