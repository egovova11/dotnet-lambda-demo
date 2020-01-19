﻿using System;
using Amazon;
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
        private Lazy<IConfiguration> Configuration { get; }

        private static readonly Lazy<IConfiguration> DefaultConfiguration = new Lazy<IConfiguration>(GetDefaultConfiguration);

        [NotNull]
        internal static IServiceProvider CreateServiceProvider(IConfiguration configuration)
        {
            var services = new ServiceCollection();

            services.AddScoped<ICustomerSearchService, CustomerSearchService>();
            services.AddDbContext<AdventureWorksContext>(options =>
            {
                var connectionString = configuration.GetValue<string>("DbConnectionString");
                options.UseSqlServer(connectionString);
            });

            var provider = services.BuildServiceProvider(validateScopes: true);
            return provider;
        }

        private static IConfiguration GetDefaultConfiguration()
        {
            var envConfiguration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            var ssmPath = envConfiguration.GetValue<string>("AWS:SSM:Path");
            var ssmRegion = envConfiguration.GetValue<string>("AWS:SSM:Region");

            var combinedConfiguration = new ConfigurationBuilder()
                .AddConfiguration(envConfiguration)
                .AddSystemsManager(source =>
                {
                    source.Path = ssmPath;
                    source.AwsOptions = new AWSOptions
                    {
                        Region = RegionEndpoint.GetBySystemName(ssmRegion),
                    };
                    source.AwsOptions.DefaultClientConfig.DisableLogging = false;
                })
                .Build();
            return combinedConfiguration;
        }

        /// <summary>
        /// Initializes function with custom-configuration. For testing purposes.
        /// </summary>
        /// <param name="configuration"></param>
        internal Function(Lazy<IConfiguration> configuration)
        {
            Configuration = configuration;
            ServiceProvider = new Lazy<IServiceProvider>(() => CreateServiceProvider(configuration.Value));
        }

        [UsedImplicitly]
        public Function() : this(DefaultConfiguration)
        {

        }
    }
}
