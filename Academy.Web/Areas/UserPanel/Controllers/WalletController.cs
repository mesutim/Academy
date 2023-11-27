using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Academy.Core.Services.Interfaces;
using Academy.Model.ViewModels;

namespace MyAcademy.Web.Areas.UserPanel.Controllers
{

    [Area("UserPanel")]
    [Authorize]
    public class WalletController : Controller
    {
        private IUserService _userService;

        public WalletController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("UserPanel/Wallet")]
        public IActionResult Index()
        {
           // ViewBag.ListWallet = _userService.GetWalletUser(User.Identity.Name);
            return View();
        }

        //[HttpPost]
        //[Route("UserPanel/Wallet")]
        //public IActionResult Index(ChargeWalletViewModel charge)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        // ViewBag.ListWallet = _userService.GetWalletUser(User.Identity.Name);
        //        return View(charge);
        //    }

        //    int walletId= _userService.ChargeWallet(User.Identity.Name, charge.Amount, "شارژ حساب");
        //    // ToDO: Online Payment

        //    #region Online Payment
          
        //    var payment = new ZarinpalSandbox.Payment(charge.Amount);

        //    var res = payment.PaymentRequest("شارژ کیف پول", "https://localhost:7287/OnlinePayment/" + walletId, "MyMail@gmail.Com", "09123456789");

        //    if (res.Result.Status == 100)
        //    {
        //        return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
        //    }

        //    #endregion
            
        //    return null;
        //}
    }
}
