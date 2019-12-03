using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderRead
{
    public class ServiceBusHosting : IHostedService
    {
        private IServiceProvider _provider;
        public ServiceBusHosting(IServiceProvider servicebus)
        {
            _provider = servicebus;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _provider.CreateScope()) {
                IServicebusReceiver rec = scope.ServiceProvider.GetRequiredService<IServicebusReceiver>();
                rec.ReceiveMessage();
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            var aa = 1;
            return Task.CompletedTask;
        }
    }
}
