using BusinessLayer;
using BusinessLayer.Interface;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityLayer;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAccount _instance;
        private static readonly Lazy<AccountBL> _Account = new Lazy<AccountBL>(() => new AccountBL());

        public HomeController()
        {
            _instance = _Account.Value;
        }

        public ActionResult Index()
        {

            return View();
        }

        public List<tblAccount> LoadContent(tblAccount filterModel)
        {
            List<tblAccount> lstResult = new List<tblAccount>();
            try
            {
                int PageNumber = 1;
                int Total = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(TempData[StringUtility.CurrentPage])))
                    PageNumber = Convert.ToInt32(TempData[StringUtility.CurrentPage]);
                else if (!string.IsNullOrEmpty(Convert.ToString(Request[StringUtility.Page])))
                    PageNumber = Convert.ToInt32(Request[StringUtility.Page]);

                lstResult = _instance.GetData(filterModel, PageNumber, out Total);
                //ViewData[StringUtility.VDPager] = new PaginationModel().CreatePager(PageNumber, Total);
                ViewData[StringUtility.CurrentPage] = PageNumber;
            }
            catch (Exception ex)
            {
                ViewData[StringUtility.ErrorMessage] = "General Error";
            }
            return lstResult;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}