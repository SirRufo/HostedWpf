using HostedWpf.Models;
using HostedWpf.Services;
using HostedWpf.Services.Impl;
using HostedWpf.ViewModels;
using HostedWpf.Views;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System.Windows;

namespace HostedWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices( ( context, services ) =>
                {
                    ConfigureServices( context.Configuration, services );
                } )
                .Build();
        }

        private void ConfigureServices( IConfiguration configuration, IServiceCollection services )
        {
            services
                .Configure<AppSettings>( configuration.GetSection( nameof( AppSettings ) ) )
                .AddScoped<ISampleService, SampleService>()
                .AddTransient<MainViewModel>();
        }

        protected override async void OnStartup( StartupEventArgs e )
        {
            await _host.StartAsync();

            var view = new MainWindow();
            var vm = _host.Services.GetRequiredService<MainViewModel>();
            view.DataContext = vm;

            MainWindow = view;
            view.Show();

            await vm.InitializeAsync( null );

            base.OnStartup( e );
        }

        protected override async void OnExit( ExitEventArgs e )
        {
            using ( _host )
            {
                await _host.StopAsync();
            }

            base.OnExit( e );
        }
    }
}
