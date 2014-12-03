namespace InspectCodeRunner.Wpf.ViewModels
{
    using Catel;
    using Catel.Configuration;
    using Catel.IO;
    using Catel.MVVM;
    using Catel.Services;
    using Services;

    public class MainWindowViewModel: ViewModelBase
    {
        private readonly IConfigurationService _configurationService;
        private readonly IInspectCodeRunnerService _inspectCodeRunnerService;
        private readonly IProcessService _processService;

        public MainWindowViewModel(IConfigurationService configurationService, IInspectCodeRunnerService inspectCodeRunnerService, IProcessService processService)
        {
            Argument.IsNotNull(() => configurationService);
            Argument.IsNotNull(() => inspectCodeRunnerService);
            Argument.IsNotNull(() => processService);
            _configurationService = configurationService;
            _inspectCodeRunnerService = inspectCodeRunnerService;
            _processService = processService;
            InspectCodeRunnerSettings = CreateInspectCodeRunnerSettings();
        }

        public InspectCodeRunnerViewModel InspectCodeRunnerSettings { get; set; }

        private InspectCodeRunnerViewModel CreateInspectCodeRunnerSettings()
        {
            var result = new InspectCodeRunnerViewModel();
            result.InspectCodeLocation = _configurationService.GetValue<string>("InspectCodeLocation");
            result.InspectCodeParameters = _configurationService.GetValue("InspectCodeParameters", @"");
            result.InspectCodeResultViewer = _configurationService.GetValue("InspectCodeResultViewer", @"");
            if (string.IsNullOrEmpty(result.InspectCodeLocation))
            {
                result.IsInspectCodeExpanded = true;
            }

            return result;
        }

        private void SaveSettings()
        {
            _configurationService.SetValue("InspectCodeLocation", InspectCodeRunnerSettings.InspectCodeLocation);
            _configurationService.SetValue("InspectCodeParameters", InspectCodeRunnerSettings.InspectCodeParameters);
            _configurationService.SetValue("InspectCodeResultViewer", InspectCodeRunnerSettings.InspectCodeResultViewer);
        }

        #region Run command

        private Command _runCommand;

        public Command Run
        {
            get { return _runCommand ?? (_runCommand = new Command(ExecuteRun)); }
        }

        private async void ExecuteRun()
        {

            var arguments = string.Format("{0} /output={1} {2}",
                InspectCodeRunnerSettings.SolutionFile,
                InspectCodeRunnerSettings.OutputResultPath,
                InspectCodeRunnerSettings.InspectCodeParameters);


            _inspectCodeRunnerService.CreateDirectoryIfNotExists(
                Path.GetParentDirectory(InspectCodeRunnerSettings.OutputResultPath));
            _inspectCodeRunnerService.Run(InspectCodeRunnerSettings.InspectCodeLocation, arguments);

            SaveSettings();
        }

        #endregion

        #region RunViewer command

        private Command _runViewerCommand;

        /// <summary>
        /// Gets the RunViewer command.
        /// </summary>
        public Command RunViewer
        {
            get { return _runViewerCommand ?? (_runViewerCommand = new Command(ExecuteRunViewer)); }
        }

        /// <summary>
        /// Method to invoke when the RunViewer command is executed.
        /// </summary>
        private void ExecuteRunViewer()
        {
            _processService.StartProcess(InspectCodeRunnerSettings.InspectCodeResultViewer, InspectCodeRunnerSettings.OutputResultPath);
        }

        #endregion
    }
}
