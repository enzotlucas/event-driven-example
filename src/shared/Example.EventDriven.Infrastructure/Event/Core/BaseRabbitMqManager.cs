using EasyNetQ;
using EasyNetQ.DI;
using Microsoft.Extensions.Configuration;
using Polly;
using RabbitMQ.Client.Exceptions;

namespace Example.EventDriven.Infrastructure.Event.Configurations
{
    public class BaseRabbitMqManager
    {
        private readonly string _connectionString;

        protected IBus MessageBus { get; set; }
        protected bool IsConnected => MessageBus?.Advanced?.IsConnected ?? false;

        public BaseRabbitMqManager(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("RabbitMq");
        }

        protected void TryConnect()
        {
            if (IsConnected)
                return;

            var policy = Policy.Handle<EasyNetQException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(3, retryAttempt =>
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            policy.Execute(() =>
            {
                CreateBus();
            });
        }

        protected void CreateBus()
        {
            MessageBus = RabbitHutch.CreateBus(
                connectionString: _connectionString,
                registerServices: s =>
                {
                    s.Register<ITypeNameSerializer, EventBusTypeNameSerializer>();
                });
        }

        protected virtual void ExecuteDispose(bool disposing)
        {
            if (disposing)
            {
                MessageBus.Dispose();
            }
        }
    }
}
