// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaskRunnerService.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2014 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace InspectCodeRunner.Wpf.Services
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;
    using System.Windows;
    using Catel;
    using Catel.Configuration;
    using Catel.IoC;
    using Catel.Logging;
    using Orchestra.Models;
    using Orchestra.Services;
    using View;
    using ViewModels;
    using Path = Catel.IO.Path;

    public class TaskRunnerService : ITaskRunnerService
    {
        #region Constants
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        #endregion

        #region Fields
        private readonly IServiceLocator _serviceLocator;
        private readonly IConfigurationService _configurationService;
        private InspectCodeRunnerViewModel _inspectCodeRunnerSettings;
        #endregion

        #region Constructors
        public TaskRunnerService(IServiceLocator serviceLocator, IConfigurationService configurationService)
        {
            Argument.IsNotNull(() => serviceLocator);
            Argument.IsNotNull(() => configurationService);

            _serviceLocator = serviceLocator;
            _configurationService = configurationService;
        }

        #endregion

        #region ITaskRunnerService Members
        public async Task<object> GetViewDataContext()
        {
            _inspectCodeRunnerSettings = new InspectCodeRunnerViewModel();
            LoadSettings();
            return _inspectCodeRunnerSettings;
        }

        public FrameworkElement GetView()
        {
            return new InspectCodeRunnerView();
        }

        public async Task Run(object dataContext)
        {
            ResetLog();
            RunInspectCode();
            SaveSettings();
        }

        private void SaveSettings()
        {
            _configurationService.SetValue("InspectCodeLocation", _inspectCodeRunnerSettings.InspectCodeLocation);
            _configurationService.SetValue("InspectCodeParameters", _inspectCodeRunnerSettings.InspectCodeParameters);
        }
        private void LoadSettings()
        {
            _inspectCodeRunnerSettings.InspectCodeLocation = _configurationService.GetValue<string>("InspectCodeLocation");
            _inspectCodeRunnerSettings.InspectCodeParameters =
                _configurationService.GetValue("InspectCodeParameters", @"");
            if (string.IsNullOrEmpty(_inspectCodeRunnerSettings.InspectCodeLocation))
            {
                _inspectCodeRunnerSettings.IsInspectCodeExpanded = true;
            }
        }


        private void RunInspectCode()
        {
            var arguments = string.Format("{0} /output={1} {2}",
                _inspectCodeRunnerSettings.SolutionFile,
                _inspectCodeRunnerSettings.OutputResultDirectory,
                _inspectCodeRunnerSettings.InspectCodeParameters);
            Directory.CreateDirectory(Path.GetParentDirectory(_inspectCodeRunnerSettings.OutputResultDirectory));
            StartProcess(_inspectCodeRunnerSettings.InspectCodeLocation, arguments);
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

            Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                logControlService.Clear();
            }));
        }


        public Size GetInitialWindowSize()
        {
            return new Size(600, 700);
        }

        public string Title
        {
            get { return "InspectCode runner"; }
        }

        public bool ShowCustomizeShortcutsButton
        {
            get { return false; }
        }

        public AboutInfo GetAboutInfo()
        {
            return new AboutInfo();
        }
        #endregion
    }
}