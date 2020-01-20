using System.Threading;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using DotnetLambda21WithEf.Services;
using Microsoft.Extensions.DependencyInjection;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace DotnetLambda21WithEf
{
    public partial class Function
    {
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
