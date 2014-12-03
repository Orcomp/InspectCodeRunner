// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InspectCodeRunnerViewModel.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2014 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace InspectCodeRunner.Wpf.ViewModels
{
    using Catel.IO;
    using Catel.MVVM;

    public class InspectCodeRunnerViewModel : ViewModelBase
    {
        private string _solutionFile;
        private string _outputResultDirectory;
        private bool _isInspectCodeExpanded;

        public string SolutionFile
        {
            get { return _solutionFile; }
            set
            {
                if (_solutionFile != value)
                {
                    _solutionFile = value;
                    RaisePropertyChanged(() => SolutionFile);
                }
                OutputResultDirectory = GetOutputResultPath(_solutionFile);
            }
        }

        private string GetOutputResultPath(string solutionFilePath)
        {
            var projectFolder = Path.GetParentDirectory(Path.GetParentDirectory(solutionFilePath));
            return Path.Combine(projectFolder, @"output\InspectCode\InspectCodeResult.xml");
        }

        public bool IsInspectCodeExpanded
        {
            get { return _isInspectCodeExpanded; }
            set
            {
                if (value.Equals(_isInspectCodeExpanded))
                {
                    return;
                }
                _isInspectCodeExpanded = value;
                RaisePropertyChanged(() => IsInspectCodeExpanded);
            }
        }

        public string InspectCodeLocation { get; set; }

        public string InspectCodeParameters { get; set; }

        public string OutputResultDirectory
        {
            get { return _outputResultDirectory; }
            set
            {
                if (value == _outputResultDirectory)
                {
                    return;
                }
                _outputResultDirectory = value;
                RaisePropertyChanged(() => OutputResultDirectory);
            }
        }
    }
}