using System.Threading;
using System.Threading.Tasks;
using DotnetLambda21WithEf.Database;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace DotnetLambda21WithEf.Services
{
    internal class CustomerSearchService : ICustomerSearchService
    {
        [NotNull]
        private readonly AdventureWorksContext _context;

        public CustomerSearchService(
            [NotNull]AdventureWorksContext context)
        {
            _context = context;
        }

        public async Task<Customer> FindCustomerAsync(string name, CancellationToken cancellation)
        {

            var customer = await _context.Customers
                .FirstOrDefaultAsync(
                    x => x.FirstName.Contains(name) || x.LastName.Contains(name), 
                    cancellationToken: cancellation);

            return customer;
        }
    }
}