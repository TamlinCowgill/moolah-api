using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using moolah.transaction.api.Domain;
using moolah.transaction.api.Exceptions;
using moolah.transaction.api.Helpers;
using moolah.transaction.api.Services;

namespace moolah.transaction.api.Controllers
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
        public IActionResult CreateTransaction([FromBody] Transaction transaction)
        {
            return Created($"api/transactions/{transaction.TransactionId}", _transactionService.CreateTransaction(transaction));
        }

        [HttpPut("{transactionId}")]
        public IActionResult UpdateTransaction(string transactionId, [FromBody] Transaction transaction)
        {
            if (transaction == null) return BadRequest(nameof(transaction));
            if (transactionId != transaction.TransactionId) return BadRequest(nameof(transactionId));

            return Ok(_transactionService.UpdateTransaction(transaction));
        }

        [HttpPatch("{transactionId}")]
        public IActionResult PatchTransaction(string transactionId, [FromBody] JsonPatchDocument<Transaction> patchData)
        {
            var transaction = _transactionService.GetTransaction(transactionId);
            if (transaction == null) return NotFound();

            patchData.ApplyTo(transaction, ModelState);

            return Ok(_transactionService.UpdateTransaction(transaction));
        }
    }
}