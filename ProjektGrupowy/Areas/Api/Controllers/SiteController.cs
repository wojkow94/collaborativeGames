using ProjektGrupowy.Models.Core;
using ProjektGrupowy.Models.Database.DAO;
using ProjektGrupowy.Models.Platform;
using ProjektGrupowy.Models.Site.FormModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace ProjektGrupowy.Areas.Api.Controllers
{
    public class SiteController : Controller
    {
        DBContext db = new DBContext();

        public JsonResult Login(LoginModel loginModel)
        {
            try
            {
                loginModel.Validate();

                using (var dao = new UserDAO())
                {
                    dao.CheckUser(loginModel.Email, loginModel.Pass);
                }

                var identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, loginModel.Email)
                    }, "ApplicationCookie");

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;
                authManager.SignIn(identity);

                return Json(Result.Succes);
            }
            catch(Error.ValidationException validation)
            {
                return Json(validation.Result);
            }
            catch(Error.AppError ex)
            {
                return Json(new Result(ex.Message).AsFailure());
            }
        }

        public JsonResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;
            authManager.SignOut();

            return Json(Result.Succes);
        }

        public JsonResult Register(RegisterModel registerModel)
        {
            try
            {
                registerModel.Validate();

                using (var dao = new UserDAO())
                {
                    dao.AddUser(registerModel.Email, registerModel.Pass);
                }

                Login(new LoginModel
                {
                    Email = registerModel.Email,
                    Pass = registerModel.Pass
                });

                return Json(Result.Succes);
            }
            catch (Error.ValidationException validation)
            {
                return Json(validation.Result);
            }
            catch (Error.AppError ex)
            {
                return Json(new Result(ex.Message).AsFailure());
            }
        }

        public JsonResult SetLanguage(string cultureName)
        {
            if (ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("_culture"))
            {
                if (ControllerContext.HttpContext.Request.Cookies["_culture"].Value == cultureName)
                {
                    return Json(Result.Failure);
                }
            }

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            var userCookie = new HttpCookie("_culture", cultureName);
            userCookie.Expires = DateTime.MaxValue;
            ControllerContext.HttpContext.Response.Cookies.Set(userCookie);

            return Json(Result.Succes);
        }

        public JsonResult GetLanguage()
        {
            if (ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("_culture"))
            {
                return Json(new Result(new {
                    culture = ControllerContext.HttpContext.Request.Cookies["_culture"].Value
                }).AsSuccess());
            }

            return Json(Result.Failure);
        }
    }
}