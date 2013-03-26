using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SkhuKB.Models;
using SkhuKB.DAO;

namespace SkhuKB.Controllers
{
    public class LoginController : BaseController
    {
        public ActionResult LoginView()
        {
            return View();
        }
        public ActionResult LoginViewMobile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginView(UserVO user, String ReturnUrl)
        {
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                ViewBag.errorMsg = "아이디나 비밀번호를 입력하셔야 합니다.";
                return View();
            }
            if (!UserDAO.IsUserInfo(user.Username, user.Password))
            {
                ViewBag.errorMsg = "아이디와 비밀번호를 다시 확인해주십시오.";
                return View();
            }
            SecurityVO.Username = user.Username;

            ActionResult action = null;

            if (ReturnUrl == null)
            {
                action = RedirectToAction("Index", "Main/Index");
            }
            else
            {
                action = Redirect(ReturnUrl);
            }

            return action;
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Main/Index");
        }


        [HttpPost]
        public ActionResult LoginViewMobile(UserVO user, String ReturnUrl)
        {
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                ViewBag.errorMsg = "아이디나 비밀번호를 입력하셔야 합니다.";
                return View();
            }
            if (!UserDAO.IsUserInfo(user.Username, user.Password))
            {
                ViewBag.errorMsg = "아이디와 비밀번호를 다시 확인해주십시오.";
                return View();
            }
            SecurityVO.Username = user.Username;

            ActionResult action = null;

            if (ReturnUrl == null)
            {
                action = RedirectToActionPermanent("Index", "Mobile/Index");
            }
            else
            {
                action = Redirect(ReturnUrl);
            }

            return action;
        }

        public ActionResult LogoutMobile()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login/LoginViewMobile");
        }

    }
}
