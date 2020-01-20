using System.Threading;
using System.Threading.Tasks;
using DotnetLambda21WithEf.Database;
using JetBrains.Annotations;

namespace DotnetLambda21WithEf.Services
{
    internal interface ICustomerSearchService
    {
        [NotNull, CanBeNull]
        Task<Customer> FindCustomerAsync(string name, CancellationToken cancellation);
    }
}
