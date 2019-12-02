using System;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetLambda30WithEf
{
    public partial class Function
    {
        private IServiceProvider ServiceProvider { get; }

        private static readonly Lazy<IServiceProvider> CommonServiceProvider = new Lazy<IServiceProvider>(CreateServiceProvider);

        private static IServiceProvider CreateServiceProvider()
        {
            var services = new ServiceCollection();

            var provider = services.BuildServiceProvider(validateScopes: true);
            return provider;
        }

        internal Function(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        [UsedImplicitly]
        public Function() : this(CommonServiceProvider.Value)
        {

        }
    }
}
