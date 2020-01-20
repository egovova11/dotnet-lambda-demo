using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace DotnetLambda21WithEf.Tests
{
    public abstract class IntegrationFixtureBase
    {
        protected IConfiguration Configuration { get; set; }

        [OneTimeSetUp]
        public void FixtureSetup()
        {
            Configuration = GetConfiguration();
        }

        private static IConfigurationRoot GetConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var envConfiguration = configurationBuilder
                .AddEnvironmentVariables()
                .Build();

            var ssmPath = envConfiguration.GetValue<string>("AWS:SSM:Path");
            var ssmRegion = envConfiguration.GetValue<string>("AWS:SSM:Region");
            var ssmServiceUrl = envConfiguration.GetValue<string>("AWS:SSM:ServiceUrl");

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
            var finalConfiguration = configurationBuilder.Build();
            return finalConfiguration;
        }
    }
}
