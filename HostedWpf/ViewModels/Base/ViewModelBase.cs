using ReactiveUI;

using System.Threading.Tasks;

namespace HostedWpf.ViewModels.Base
{
    public abstract class ViewModelBase : ReactiveObject
    {
        private bool _isInitializing;
        private bool _isBusy;

        public bool IsBusy { get => _isBusy; protected set => this.RaiseAndSetIfChanged( ref _isBusy, value ); }
        public bool IsInitializing { get => _isInitializing; private set => this.RaiseAndSetIfChanged( ref _isInitializing, value ); }
        public async Task InitializeAsync( object parameter )
        {
            IsInitializing = true;
            try
            {
                await DoInitializeAsync( parameter );
            }
            finally
            {
                IsInitializing = false;
            }
        }

        virtual protected Task DoInitializeAsync( object parameter )
        {
            return Task.CompletedTask;
        }
    }
}
