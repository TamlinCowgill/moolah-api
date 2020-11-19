using System.Collections.Generic;
using moolah.transaction.api.Domain;

namespace moolah.transaction.api.Services
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
