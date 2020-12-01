using System.Collections.Generic;

namespace Moolah.Transaction.Core.Services
{
    public interface ITransactionService
    {
        IEnumerable<Domain.Transaction> GetAll();
        Domain.Transaction GetTransaction(string transactionId);
        Domain.Transaction CreateTransaction(Domain.Transaction transaction);
        Domain.Transaction UpdateTransaction(Domain.Transaction transaction);
        IEnumerable<Domain.Transaction> GetTransactionForAccount(string accountId);
    }
}
