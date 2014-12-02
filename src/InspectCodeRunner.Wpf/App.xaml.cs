// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2014 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace InspectCodeRunner.Wpf
{
    using System.Windows;
    using Catel.IoC;
    using Orchestra.Services;
    using Orchestra.Views;
    using Services;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Methods
        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceLocator = ServiceLocator.Default;

            serviceLocator.RegisterType<ITaskRunnerService, TaskRunnerService>();

            var shellService = serviceLocator.ResolveType<IShellService>();
            shellService.Create<ShellWindow>();
            base.OnStartup(e);
        }
        #endregion
    }
}