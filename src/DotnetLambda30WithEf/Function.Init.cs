using System;
using Amazon;
using Amazon.Extensions.Configuration.SystemsManager;
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
                var connectionString = configuration.GetValue<string>("DbConnectionString");
                options.UseSqlServer(connectionString);
            });

            var provider = services.BuildServiceProvider(validateScopes: true);
            return provider;
        }

        [NotNull]
        private static IConfiguration GetConfiguration()
        {
            var envConfiguration = new ConfigurationBuilder().AddEnvironmentVariables().Build();
            var environmentName = envConfiguration.GetValue<string>("ASPNETCORE_ENVIRONMENT");
            envConfiguration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true)
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .Build();

            var awsConfiguration = envConfiguration.GetSection("AWS");
            var ssmConfiguration = awsConfiguration.GetSection("SSM");
            //var prefix = envConfiguration.GetValue<string>("service-parameter-prefix");
            //var hostAndPort = envConfiguration.GetValue<string>("AWS_LAMBDA_RUNTIME_API");
            // "/malaga-serverless-net-demo/vars"

            //var prov = new SystemsManagerConfigurationProvider(new SystemsManagerConfigurationSource
            //{
            //    OnLoadException = x => Console.WriteLine(x),
            //    Prefix = "lambda-params",
            //});
            //prov.Load();

            var configurationBuilder = new ConfigurationBuilder()
                .AddSystemsManager(x =>
                {
                    //x.Prefix = "lambda-params";
                    x.Path = ssmConfiguration.GetValue<string>("Path");
                    x.AwsOptions = new AWSOptions
                    {
                        Region = RegionEndpoint.GetBySystemName(ssmConfiguration.GetValue<string>("Region")),
                        Credentials = new BasicAWSCredentials("foo", "foo")
                    };
                    x.AwsOptions.DefaultClientConfig.ServiceURL = ssmConfiguration.GetValue<string>("ServiceUrl"); // "http://localhost:4583";
                    x.AwsOptions.DefaultClientConfig.DisableLogging = false;
                    x.AwsOptions.DefaultClientConfig.UseHttp = true;
                });
            return configurationBuilder.AddConfiguration(envConfiguration).Build();
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
