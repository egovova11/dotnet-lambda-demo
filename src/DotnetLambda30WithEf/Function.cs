using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using Amazon.Lambda.Serialization.Json;
using System;
using System.Threading.Tasks;
using Amazon.Extensions.NETCore.Setup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetLambda30WithEf
{
    public partial class Function
    {
        /// <summary>
        /// The main entry point for the custom runtime.
        /// </summary>
        /// <param name="args"></param>
        private static async Task Main(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddSystemsManager("", new AWSOptions(){})
            var function = new Function(CommonServiceProvider.Value);
            Func<string, ILambdaContext, string> func = function.FunctionHandler;


            using var handlerWrapper = HandlerWrapper.GetHandlerWrapper(func, new JsonSerializer());
            using var bootstrap = new LambdaBootstrap(handlerWrapper);
            await bootstrap.RunAsync();
        }

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string FunctionHandler(string input, ILambdaContext context)
        {
            using var scope = ServiceProvider.CreateScope();
            return input?.ToUpper();
        }
    }
}
