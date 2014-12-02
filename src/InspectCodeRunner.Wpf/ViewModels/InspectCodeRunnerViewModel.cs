// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InspectCodeRunnerViewModel.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2014 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace InspectCodeRunner.Wpf.ViewModels
{
    using System.ComponentModel;
    using Catel.MVVM;

    public class InspectCodeRunnerViewModel : ViewModelBase
    {
        public InspectCodeRunnerViewModel()
        {
        }
        public string SolutionFile { get; set; }
        public string InspectCodeLocation { get; set; }

        public string InspectCodeParameters { get; set; }
        public string OutputResultDirectory { get; set; }
    }
}