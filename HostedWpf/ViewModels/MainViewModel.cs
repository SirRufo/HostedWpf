using HostedWpf.Models;
using HostedWpf.Services;
using HostedWpf.ViewModels.Base;

using Microsoft.Extensions.Options;

using ReactiveUI;

using System.Threading.Tasks;
using System.Windows.Input;

namespace HostedWpf.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ISampleService _sampleService;
        private readonly AppSettings _settings;
        private string _someValue;

        public MainViewModel( ISampleService sampleService, IOptions<AppSettings> settings )
        {
            _sampleService = sampleService;
            _settings = settings.Value;

            RefreshCommand = ReactiveCommand.CreateFromTask( RefreshCommandExecute );
        }

        private Task RefreshCommandExecute()
        {
            return LoadDataAsync();
        }

        public string SomeValue { get => _someValue; private set => this.RaiseAndSetIfChanged( ref _someValue, value ); }
        public ICommand RefreshCommand { get; }

        private async Task LoadDataAsync()
        {
            IsBusy = true;
            try
            {
                SomeValue = null;
                SomeValue = await _sampleService.GetSomeDataAsync();
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected override async Task DoInitializeAsync( object parameter )
        {
            await LoadDataAsync();
            await base.DoInitializeAsync( parameter );
        }
    }
}
