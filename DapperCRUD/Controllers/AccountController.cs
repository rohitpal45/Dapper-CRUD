using DapperCRUD.DAL;
using DapperCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DapperCRUD.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        DataAccess Db = new DataAccess();
        private int procId;
        // GET: Account
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(UserLogin M)
        {

            var list = Db.GetuserLogin(M).ToList();
            if (list.Count()>0)
            {
                Session.Add("userid", list[0].userid);
                Session.Add("UserName", list[0].UserName);
                Session.Add("RoleId", list[0].RoleId);
                FormsAuthentication.SetAuthCookie(list[0].userid.ToString(), false);
                if (list[0].RoleId== "1002")
                {
                    return RedirectToAction("UserInfo", "Home");
                }
                return RedirectToAction("EmployeeInfo", "Home");
            }
            else
            {
              M.password = "";
              TempData["Msg"] = "User Id Password Not Valid..?";
              return View(M);
            }
        }
        #region Logout
        public ActionResult Logout()
        {

            string[] mycookies = Request.Cookies.AllKeys;
            foreach (string cookie in mycookies)
            {
                Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
            }
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
            }
            if (Request.Cookies["PHPSESSID"] != null)
            {
                Response.Cookies["PHPSESSID"].Value = string.Empty;
                Response.Cookies["PHPSESSID"].Expires = DateTime.Now.AddMonths(-20);
            }
            if (Request.Cookies["AuthToken"] != null)
            {
                Response.Cookies["AuthToken"].Value = string.Empty;
                Response.Cookies["AuthToken"].Expires = DateTime.Now.AddMonths(-20);
            }
            return RedirectToAction("Index", "Account");
        }
        #endregion
    }
}