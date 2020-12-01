using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Moolah.Transaction.Api.Exceptions;
using Moolah.Transaction.Api.Helpers;
using Moolah.Transaction.Core.Services;

namespace Moolah.Transaction.Api.Controllers
{
    [TypeFilter(typeof(CustomExceptionFilter))]
    [Route("api/[controller]")]
    public class TransactionsController : Controller
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public IActionResult GetAllTransactions()
        {
            return Ok(_transactionService.GetAll());
        }

        [HttpGet("{transactionId}")]
        public IActionResult GetTransaction(string transactionId)
        {
            var transaction = _transactionService.GetTransaction(transactionId);
            if (transaction == null) throw new NotFoundException("transaction", "transactionid", transactionId);

            return Ok(transaction);
        }

        [HttpPost]
        public IActionResult CreateTransaction([FromBody] Core.Domain.Transaction transaction)
        {
            return Created($"api/transactions/{transaction.TransactionId}", _transactionService.CreateTransaction(transaction));
        }

        [HttpPut("{transactionId}")]
        public IActionResult UpdateTransaction(string transactionId, [FromBody] Core.Domain.Transaction transaction)
        {
            if (transaction == null) return BadRequest(nameof(transaction));
            if (transactionId != transaction.TransactionId) return BadRequest(nameof(transactionId));

            return Ok(_transactionService.UpdateTransaction(transaction));
        }

        [HttpPatch("{transactionId}")]
        public IActionResult PatchTransaction(string transactionId, [FromBody] JsonPatchDocument<Core.Domain.Transaction> patchData)
        {
            var transaction = _transactionService.GetTransaction(transactionId);
            if (transaction == null) return NotFound();

            patchData.ApplyTo(transaction, ModelState);

            return Ok(_transactionService.UpdateTransaction(transaction));
        }
    }
}