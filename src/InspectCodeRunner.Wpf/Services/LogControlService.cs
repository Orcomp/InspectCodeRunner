namespace InspectCodeRunner.Wpf.Services
{
    using Catel;
    using Catel.Logging;
    using Catel.Windows.Threading;
    using Controls;
    using Orchestra.Services;

    public class LogControlService : ILogControlService
    {
        private readonly TraceOutputControl _traceOutputControl;

        #region Constructors
        public LogControlService(TraceOutputControl traceOutputControl)
        {
            Argument.IsNotNull(() => traceOutputControl);

            _traceOutputControl = traceOutputControl;
            _traceOutputControl.Dispatcher.BeginInvoke(() =>
            {
                _traceOutputControl.SelectedLevel = LogEvent.Info;
            });
        }
        #endregion

        public LogEvent SelectedLevel
        {
            get { return _traceOutputControl.SelectedLevel; }
            set { _traceOutputControl.SelectedLevel = value; }
        }

        public void Clear()
        {
            _traceOutputControl.Clear();
        }
    }
}
