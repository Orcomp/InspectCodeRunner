namespace InspectCodeRunner.Wpf.View
{
    using Catel;
    using Catel.IoC;
    using Orchestra.Services;
    using LogControlService = Services.LogControlService;

    /// <summary>
    /// Interaction logic for MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : Catel.Windows.Window
    {
        public MainWindowView()
        {
            InitializeComponent();
            var serviceLocator = this.GetServiceLocator();
            serviceLocator.RegisterInstance<ILogControlService>(new LogControlService(traceOutputControl));
        }
    }
}
