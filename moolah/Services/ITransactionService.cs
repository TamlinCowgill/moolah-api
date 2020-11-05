using System.Collections.Generic;
using Moolah.Api.Domain;

namespace Moolah.Api.Services
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
