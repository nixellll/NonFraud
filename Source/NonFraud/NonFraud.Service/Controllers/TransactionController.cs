using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NonFraud.Data.Repositories;
using NonFraud.Data.Entities;
using NonFraud.Service.Mappers;
using NonFraud.Service.Helpers;

namespace NonFraud.Service.Controllers
{
    /// <summary>
    /// Transaction controller of application
    /// </summary>
    public class TransactionController : ApiController
    {
        BaseRepo<Transaction> _baseTrxRepo;
        BaseRepo<Customer> _baseCustRepo;
        BaseRepo<CustomerBalance> _baseCustBalRepo;
        BaseRepo<TransactionType> _baseTrxTypeRepo;
        TransactionRepo _trxRepo;
        CustomerRepo _custRepo;
        TransactionMapper _trxMapper;
        EncryptionHelper _encrypHelper;

        public TransactionController()
        {
            _baseTrxRepo = new BaseRepo<Transaction>();
            _baseCustRepo = new BaseRepo<Customer>();
            _baseCustBalRepo = new BaseRepo<CustomerBalance>();
            _baseTrxTypeRepo = new BaseRepo<TransactionType>();
            _trxRepo = new TransactionRepo();
            _custRepo = new CustomerRepo();
            _trxMapper = new TransactionMapper();
            _encrypHelper = new EncryptionHelper();
        }

        /// <summary>
        /// Get the transaction list
        /// </summary>
        public List<NonFraud.Data.Models.TransactionModel> Get()
        {
            return _trxRepo.GetTransactions();
        }

        /// <summary>
        /// Insert a new transaction
        /// </summary>
        /// <param name="trx">Transaction model</param>
        [HttpPost]
        public HttpResponseMessage Post(NonFraud.Service.Models.TransactionModel trx)
        {
            var response = new HttpResponseMessage();

            if (trx.Token == null || !_encrypHelper.ValidateToken(trx.Token, trx.NameOrig + trx.NameDest))
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                response.ReasonPhrase = "Invalid Request. Please try again";
                return response;
            }

            _baseCustRepo.Insert(_trxMapper.Map(trx.NameOrig));
            _baseCustRepo.Insert(_trxMapper.Map(trx.NameDest));
            int custOrigId = _custRepo.GetCustByName(trx.NameOrig).CustomerID;
            int custDestId = _custRepo.GetCustByName(trx.NameDest).CustomerID;
            _baseCustBalRepo.Insert(_trxMapper.Map(custOrigId, trx.OldBalanceOrig, trx.NewBalanceOrig));
            _baseCustBalRepo.Insert(_trxMapper.Map(custDestId, trx.OldBalanceDest, trx.NewBalanceDest));
            _baseTrxRepo.Insert(_trxMapper.Map(trx, custOrigId, custDestId));
            response.StatusCode = HttpStatusCode.OK;

            return response;
        }

        /// <summary>
        /// Update isFraud field in a transaction
        /// </summary>
        /// <param name="trx">Transaction model</param>
        [HttpPut]
        public HttpResponseMessage Put(NonFraud.Service.Models.TransactionModel trx)
        {
            var response = new HttpResponseMessage();

            if (trx.Token == null || !_encrypHelper.ValidateToken(trx.Token, trx.NameOrig + trx.NameDest))
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                response.ReasonPhrase = "Invalid Request. Please try again";
                return response;               
            }
            _trxRepo.UpdateIsFraudTrx(trx.TransactionId, trx.IsFraud);

            return response;
        }
    }
}
