// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InspectCodeRunnerViewModel.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2014 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace InspectCodeRunner.Wpf.ViewModels
{
    using Catel.MVVM;

    public class InspectCodeRunnerViewModel : ViewModelBase
    {

        public string SolutionFile { get; set; }
        public string InspectRunnerLocation { get; set; }
        public string OutputResultDirectory { get; set; }
    }
}