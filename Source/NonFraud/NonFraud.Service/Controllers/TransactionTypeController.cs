using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NonFraud.Data.Repositories;
using NonFraud.Data.Entities;
using NonFraud.Service.Mappers;
using System.Web.Mvc;

namespace NonFraud.Service.Controllers
{
    /// <summary>
    /// Transaction Type controller of application
    /// </summary>
    public class TransactionTypeController : ApiController
    {
        BaseRepo<TransactionType> _baseRepo;  
        TransactionTypeMapper _trxMapper;

        public TransactionTypeController()
        {
            _baseRepo = new BaseRepo<TransactionType>();
            _trxMapper = new TransactionTypeMapper();
        }

        /// <summary>
        /// Returns the Transaction Type List
        /// </summary>
        public List<SelectListItem> Get()
        {
            return _trxMapper.Map(_baseRepo.GetAll().ToList());
        }
    }
}
