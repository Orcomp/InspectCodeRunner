namespace InspectCodeRunner.Wpf.ViewModels
{
    using Catel;
    using Catel.Configuration;
    using Catel.IO;
    using Catel.MVVM;
    using Services;

    public class MainWindowViewModel: ViewModelBase
    {
        private readonly IConfigurationService _configurationService;
        private readonly IInspectCodeRunnerService _inspectCodeRunnerService;

        public MainWindowViewModel(IConfigurationService configurationService, IInspectCodeRunnerService inspectCodeRunnerService)
        {
            Argument.IsNotNull(() => configurationService);
            Argument.IsNotNull(() => inspectCodeRunnerService);
            _configurationService = configurationService;
            _inspectCodeRunnerService = inspectCodeRunnerService;
            InspectCodeRunnerSettings = CreateInspectCodeRunnerSettings();
        }

        public InspectCodeRunnerViewModel InspectCodeRunnerSettings { get; set; }

        private InspectCodeRunnerViewModel CreateInspectCodeRunnerSettings()
        {
            var result = new InspectCodeRunnerViewModel();
            result.InspectCodeLocation = _configurationService.GetValue<string>("InspectCodeLocation");
            result.InspectCodeParameters = _configurationService.GetValue("InspectCodeParameters", @"");
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
                InspectCodeRunnerSettings.OutputResultDirectory,
                InspectCodeRunnerSettings.InspectCodeParameters);


            _inspectCodeRunnerService.CreateDirectoryIfNotExists(
                Path.GetParentDirectory(InspectCodeRunnerSettings.OutputResultDirectory));
            _inspectCodeRunnerService.Run(InspectCodeRunnerSettings.InspectCodeLocation, arguments);

            SaveSettings();
        }

        #endregion
    }
}
