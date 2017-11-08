using NonFraud.Service.Models;
using NonFraud.Web.Helpers;
using NonFraud.Web.Models;
using NonFraud.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace NonFraud.Web.Controllers
{
    /// <summary>
    /// Transaction controller of the application
    /// </summary>
    [Authorize]
    public class TransactionController : Controller
    {
        BaseService<TransactionModel> _baseService;
        EncryptionHelper _encrypHelper;

        public TransactionController()
        {
            _baseService = new BaseService<TransactionModel>();
            _encrypHelper = new EncryptionHelper();
        }

        [Authorize(Roles = "Superintendent,Administrator")]
        public ActionResult Index()
        {
            return View(GetTrx());
        }

        // GET: Transaction/Create
        [Authorize(Roles = "Assistant,Administrator")]
        public ActionResult Create()
        {
            return View(new TransactionModel { TrxTypes = GetTrxTypes(), TransactionDate = DateTime.Now });
        }

        // POST: Transaction/Create
        [HttpPost]
        [Authorize(Roles = "Assistant,Administrator")]
        public ActionResult Create(TransactionModel transaction)
        {
            try
            {
                ResponseModel responseModel = new ResponseModel();
                transaction.Token = _encrypHelper.GenerateToken(transaction.NameOrig + transaction.NameDest);
                responseModel = _baseService.Create(transaction, StaticValues.TrxServiceUrl);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Manager,Administrator")]
        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Superintendent,Administrator")]
        public ActionResult UpdateIsFraud(TransactionModel transaction)
        {
            ResponseModel responseModel = new ResponseModel();
            transaction.Token = _encrypHelper.GenerateToken(transaction.NameOrig + transaction.NameDest);
            responseModel = _baseService.Update(transaction, StaticValues.TrxServiceUrl);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Get transaction from service helper
        /// </summary>
        private List<TransactionModel> GetTrx()
        {
            string path = String.Format(StaticValues.TrxServiceUrl);
            string trx = _baseService.Get(path);
            var serializedTrx = new JavaScriptSerializer().Deserialize<List<TransactionModel>>(trx);

            return serializedTrx;
        }

        /// <summary>
        /// Get transaction types from service helper
        /// </summary>
        private List<SelectListItem> GetTrxTypes()
        {
            string path = String.Format(StaticValues.TrxTypeServiceUrl);
            string trx = _baseService.Get(path);
            var serializedTrx = new JavaScriptSerializer().Deserialize<List<SelectListItem>>(trx);

            return serializedTrx;
        }
    }
}
