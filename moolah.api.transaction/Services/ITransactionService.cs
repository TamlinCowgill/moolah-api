using System.Collections.Generic;
using moolah.api.transaction.Domain;

namespace moolah.api.transaction.Services
{
    public interface ITransactionService
    {
        IEnumerable<Transaction> GetAll();
        Transaction GetTransaction(string transactionId);
        Transaction CreateTransaction(Transaction transaction);
        Transaction UpdateTransaction(Transaction transaction);
        IEnumerable<Transaction> GetTransactionForAccount(string accountId);
    }
}
