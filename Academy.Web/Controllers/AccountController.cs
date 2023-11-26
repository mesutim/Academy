using System.Security.Claims;
using Academy.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Academy.Core.Convertors;
using Academy.Model.ViewModels;
using Academy.Model.Models.IdentityModels;
using Academy.Core.Security;
using Academy.Core.Generator;

namespace MyAcademy.Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private IUserService _userService;
        IViewRenderService _viewRenderService;

        public AccountController(IUserService userService, IViewRenderService viewRenderService)
        {
            _userService = userService;
            _viewRenderService = viewRenderService;
        }

        #region Register

        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
                return View(register);
            if (_userService.IsExistUserName(register.UserName))
            {
                ModelState.AddModelError("UserName","نام کاربری معتبر نمی باشد");
                return View(register);
            }
            if (_userService.IsExistEmail(FixedText.FixEmail(register.Email)))
            {
                ModelState.AddModelError("Email", "ایمیل معتبر نمی باشد");
                return View(register);
            }

            User user = new User()
            {
                UserName = register.UserName,
                Email = FixedText.FixEmail(register.Email),
                Password = PasswordHelper.EncodePasswordMd5(register.Password),
                ActiveCode = NameGenerator.GenerateUniqCode(),
                IsActive = false,
                RegisterDate = DateTime.Now,
                UserAvatar = "Default.jpg"
            };
            _userService.AddUser(user);

            #region Send Activation Code
            //ToDo:   ----------------
            //string body = _viewRenderService.RenderToStringAsync("ActiveEmail", user);
           // SendEmail.Send(user.Email,"ایمیل فعالسازی",body);
            #endregion

            return View("SuccessRegister",user);
        }

        public IActionResult VerifyEmail(string email)
        {
            if (_userService.IsExistEmail(email.Trim().ToLower()))
            {
                return Json($"ایمیل {email} تکراری است");
            }

            return Json(true);
        }

        public IActionResult VerifyUserName(string userName)
        {
            if (_userService.IsExistUserName(userName))
            {
                return Json($"نام کاربری {userName} تکراری است");
            }

            return Json(true);
        }
        #endregion

        #region Active Account

        public IActionResult ActiveAccount(string id)
        {
            ViewBag.IsActive = _userService.ActiveAccount(id);
            return View();
        }

        #endregion

        #region Login
        [Route(("Login"))]
        public IActionResult Login()
        {
           return View();
        }

        [HttpPost]
        [Route(("Login"))]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
                return View(login);
            var user = _userService.LoginUser(login);
            if (user!=null)
            {
                if (user.IsActive)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName)
                    };
                    var identity=new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal=new ClaimsPrincipal(identity);
                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = login.RememberMe
                    };
                    HttpContext.SignInAsync(principal, properties);
                    ViewBag.IsSuccess=true;
                    return View();
                }
                else
                {
                    ModelState.AddModelError("Email", "حساب کاربری شما فعال نمی باشد");
                    return View(login);
                }
            }
            ModelState.AddModelError("Email", "کاربری با مشخصات وارد شده یافت نشد");
            return View(login);
        }

        #endregion

        #region Logout

        [Route(("Logout"))]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login");
        }


        #endregion

        #region Forgot Password
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword(ForgotPasswordViewModel forgot)
        {
            if (!ModelState.IsValid)
            {
                return View(forgot);
            }

            string email = FixedText.FixEmail(forgot.Email);
            var user = _userService.GetUserByEmail(email);
            if (user == null)
            {
                ModelState.AddModelError("Email","کاربری یافت نشد");
                return View(forgot);
            }

            //string bodyEmail = _viewRenderService.RenderToStringAsync("_ForgotPassword", user);
           // SendEmail.Send(user.Email,"بازیابی رمز عبور",bodyEmail);
            ViewBag.IsSuccess=true;
            return View();
        }

        #endregion

        #region Reset Password

        public IActionResult ResetPassword(string id)
        {
            return View(new ResetPasswordViewModel()
            {
                ActiveCode = id
            });
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel reset)
        {
            if (!ModelState.IsValid)
                return View(reset);
            var user = _userService.GetUserByActiveCode(reset.ActiveCode);
            if (user == null)
                return NotFound();
            string hashPassword = PasswordHelper.EncodePasswordMd5(reset.Password);
            user.Password=hashPassword;
            _userService.UpdateUser(user);

           return Redirect("/Login");
        }

        #endregion
    }
}
