// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2014 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace InspectCodeRunner.Wpf
{
    using System.Globalization;
    using System.Windows;
    using System.Windows.Markup;
    using Catel.IoC;
    using Services;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        }

        #region Methods
        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceLocator = ServiceLocator.Default;
            serviceLocator.RegisterType<IInspectCodeRunnerService, InspectCodeRunnerService>();

            base.OnStartup(e);
        }
        #endregion
    }
}