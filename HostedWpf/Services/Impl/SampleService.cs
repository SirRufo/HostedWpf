using HostedWpf.Models;

using Microsoft.Extensions.Options;

using System;
using System.Threading.Tasks;

namespace HostedWpf.Services.Impl
{
    public class SampleService : ISampleService
    {
        private readonly AppSettings _settings;

        public SampleService( IOptions<AppSettings> settings )
        {
            _settings = settings.Value;
        }
        public string GetCurrentDate() => DateTime.Today.ToLongDateString();

        public async Task<string> GetSomeDataAsync()
        {
            await Task.Delay( _settings.IntValue ).ConfigureAwait( false );
            return _settings.StringValue;
        }
    }
}
