using DapperCRUD.DAL;
using DapperCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DapperCRUD.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        DataAccess Db = new DataAccess();
        private int procId;
        Securty Sc = new Securty();
        [HttpGet]
        public ActionResult EmployeeInfo()
        {
            Employee Model = new Employee();
            if (Request.QueryString["sid"] != null)   
            {
                var userid = Convert.ToInt32(Request.QueryString["sid"].ToString());
                Model = Db.GetEmpInfo(4, Model).ToList().Where(m=>m.userid== userid).FirstOrDefault();
                Model.UserPassword = Sc.DecodeFrom64(Model.UserPassword).ToString();
                ViewBag.button = "Update";
            }
            else
            {
                ViewBag.button = "Submit";
            }
            var list1 = Db.GetEmpInfo(4, Model).ToList(); 
            if (list1.Count > 0)
            {
                ViewBag.list = list1;
            }
            else
            {
                ViewBag.list = null;
            }
            return View(Model);
        }
        [HttpPost]
        public ActionResult EmployeeInfo(Employee Model, string command, HttpPostedFileBase UserProfile)
        {
            if (UserProfile != null)
            {
                string prepath = "~/Content/Uploads";
                string path = "";
                var uploadUrl = Server.MapPath(prepath);
                string extension = System.IO.Path.GetExtension(Request.Files["UserProfile"].FileName);
                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                {
                    if (UserProfile.ContentLength > 0)
                    {
                        UserProfile = Request.Files["UserProfile"];
                        string Name = DateTime.Now.Ticks + "_R" + extension.ToLower().ToString();
                        string pathtosave = prepath + "/" + Name;
                        path = uploadUrl + "/" + Name;
                        Model.UserProfile = Name;
                        UserProfile.SaveAs(path);
                    }
                    else
                    {
                        TempData["Message"] = "Please Upload Photo !";
                    }
                }
                else
                {
                    TempData["Message"] = "Please Upload Valid File !";
                }
            }

            if (command == "Submit")
            {
                procId = 1;

            }
            else if (command == "Update")
            {
                procId = 2;
            }
            var list = Db.GetEmpInfo(procId, Model).ToList();
            if (list.Count > 0)
            {
                if (list[0].Msg == "success")
                {
                    TempData["msg"] = "1";
                }
                else if (list[1].Msg == "Error")
                {
                    TempData["msg"] = "2";
                }
                else if (list[0].Msg == "update")
                {
                    TempData["msg"] = "3";
                }
                else
                {
                    TempData["msg"] = "4";
                }
                ViewBag.list = list;
            }
            else
            {
                ViewBag.list = null;
            }
            return RedirectToAction("EmployeeInfo", "Home");
        }

        public JsonResult Delete(int sid) 
        {
            Employee Model = new Employee();
            Model.userid = sid;
            var data = Db.GetEmpInfo(3, Model).ToList().FirstOrDefault();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Update(int Rid) 
        {
            Employee Model = new Employee();
            return RedirectToAction("EmployeeInfo", new { sid = Rid });
        }

        [HttpGet]
        public ActionResult UserInfo()
        {
            Employee Model = new Employee();
            var userid = Convert.ToInt32(Session["userid"].ToString());
            Model = Db.GetEmpInfo(4, Model).ToList().Where(m => m.userid == userid).FirstOrDefault();
            ViewBag.button = "Update";
            return View(Model);
        }

        public ActionResult UserUpdateProfile(int Rid)
        {
            Employee Model = new Employee();
            return RedirectToAction("UserInfo", new { sid = Rid });
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}