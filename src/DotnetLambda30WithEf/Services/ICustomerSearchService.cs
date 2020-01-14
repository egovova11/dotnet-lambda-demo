using System.Threading;
using System.Threading.Tasks;
using DotnetLambda30WithEf.Database;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace DotnetLambda30WithEf.Services
{
    internal interface ICustomerSearchService
    {
        [NotNull, CanBeNull]
        Task<Customer> FindCustomerAsync(string name, CancellationToken cancellation);
    }
}
