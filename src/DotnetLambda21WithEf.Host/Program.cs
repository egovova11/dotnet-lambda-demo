using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace DotnetLambda21WithEf.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(Configure)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void Configure([NotNull] IConfigurationBuilder configurationBuilder)
        {
            var envConfiguration = configurationBuilder
                .AddEnvironmentVariables()
                .Build();
            var environmentName = envConfiguration.GetValue<string>("ASPNETCORE_ENVIRONMENT");
            var fileConfiguration = configurationBuilder
                .AddJsonFile("appsettings.json", true)
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .Build();

            var ssmPath = fileConfiguration.GetValue<string>("AWS:SSM:Path");
            var ssmRegion = fileConfiguration.GetValue<string>("AWS:SSM:Region");
            var ssmServiceUrl = fileConfiguration.GetValue<string>("AWS:SSM:ServiceUrl");

            configurationBuilder.AddSystemsManager(source =>
            {
                source.Path = ssmPath;
                source.AwsOptions = new AWSOptions
                {
                    Region = RegionEndpoint.GetBySystemName(ssmRegion),
                    // doesn't matter what credentials are used - any are accepted by localstack
                    Credentials = new BasicAWSCredentials("foo", "foo")
                };
                source.AwsOptions.DefaultClientConfig.ServiceURL = ssmServiceUrl;
                source.AwsOptions.DefaultClientConfig.DisableLogging = false;
                source.AwsOptions.DefaultClientConfig.UseHttp = true;
            });
        }
    }
}
