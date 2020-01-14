using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using Amazon.Lambda.Serialization.Json;
using System;
using System.Threading;
using System.Threading.Tasks;
using DotnetLambda30WithEf.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetLambda30WithEf
{
    public partial class Function
    {
        /// <summary>
        /// The main entry point for the custom runtime.
        /// </summary>
        /// <param name="args"></param>
        internal static async Task Main(string[] args)
        {
            var function = new Function();
            Func<string, ILambdaContext, Task<string>> func = function.FunctionHandler;
            using var handlerWrapper = HandlerWrapper.GetHandlerWrapper(func, new JsonSerializer());
            using var bootstrap = new LambdaBootstrap(handlerWrapper);
            await bootstrap.RunAsync();
        }

        /// <summary>
        /// Function performs lookup of a customer by provided input string
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<string> FunctionHandler(string input, ILambdaContext context)
        {
            var serviceProvider = ServiceProvider.Value;
            using var scope = serviceProvider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<ICustomerSearchService>();
            var customer = await service.FindCustomerAsync(input, CancellationToken.None);
            return $"Customer search result: {customer?.CustomerID} {customer?.FirstName} {customer?.LastName}";
        }
    }
}
