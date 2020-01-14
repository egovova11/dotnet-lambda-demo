using System;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using DotnetLambda30WithEf.Database;
using DotnetLambda30WithEf.Services;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetLambda30WithEf
{
    public partial class Function
    {
        [NotNull, ItemNotNull]
        private Lazy<IServiceProvider> ServiceProvider { get; }



        [NotNull, ItemNotNull]
        private static readonly Lazy<IServiceProvider> CommonServiceProvider = new Lazy<IServiceProvider>(CreateServiceProvider);

        [NotNull]
        internal static IServiceProvider CreateServiceProvider()
        {
            var configuration = GetConfiguration();

            var services = new ServiceCollection();

            services.AddScoped<ICustomerSearchService, CustomerSearchService>();
            services.AddDbContext<AdventureWorksContext>(options =>
                {
                    options.UseSqlServer("Server=host.docker.internal,8085;Initial Catalog=AdventureWorksLT2017;User Id=sa;Password=PaSSw0rd;");
                });

            var provider = services.BuildServiceProvider(validateScopes: true);
            return provider;
        }

        [NotNull]
        private static IConfiguration GetConfiguration()
        {
            var envConfiguration = new ConfigurationBuilder().AddEnvironmentVariables().Build();
            var prefix = envConfiguration.GetValue<string>("service-parameter-prefix");
            //var hostAndPort = envConfiguration.GetValue<string>("AWS_LAMBDA_RUNTIME_API");
            // "/malaga-serverless-net-demo/vars"

            //var configurationBuilder = new ConfigurationBuilder()
            //    .AddSystemsManager(prefix, new AWSOptions
            //    {
            //        Credentials = new BasicAWSCredentials("123", "456")
            //    });

            //return configurationBuilder.AddConfiguration(envConfiguration).Build();
            return envConfiguration;
        }

        /// <summary>
        /// This interface is for testing purposes
        /// </summary>
        /// <param name="serviceProvider"></param>
        internal Function(Lazy<IServiceProvider> serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        [UsedImplicitly]
        public Function() : this(CommonServiceProvider)
        {

        }
    }
}
