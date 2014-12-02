// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaskRunnerService.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2014 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace InspectCodeRunner.Wpf.Services
{
    using System.Threading.Tasks;
    using System.Windows;
    using Catel;
    using Catel.IoC;
    using Catel.Logging;
    using Orchestra.Models;
    using Orchestra.Services;
    using View;
    using ViewModels;

    public class TaskRunnerService : ITaskRunnerService
    {
        #region Constants
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        #endregion

        #region Fields
        private readonly IServiceLocator _serviceLocator;
        #endregion

        #region Constructors
        public TaskRunnerService(IServiceLocator serviceLocator)
        {
            Argument.IsNotNull(() => serviceLocator);

            _serviceLocator = serviceLocator;
        }
        #endregion

        #region ITaskRunnerService Members
        public async Task<object> GetViewDataContext()
        {
            return new InspectCodeRunnerViewModel();
        }

        public FrameworkElement GetView()
        {
            return new InspectCodeRunnerView();
        }

        public async Task Run(object dataContext)
        {
            Log.Info("Run...");
            Log.Info("Finished");
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