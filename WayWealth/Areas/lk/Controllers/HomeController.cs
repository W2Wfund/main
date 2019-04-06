using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Common.Web;
using W2W.ModelKBT.Entities;
using WayWealth.Areas.lk.ViewModels;
using WayWealth.Areas.lk.ViewModels.Home;
using WayWealth.Controllers;
using System.Configuration;
using Org.BouncyCastle.Asn1.Crmf;
using WayWealth.Infrastructure.Auth;
using KBTech.Lib.Client.Payment;
using RestSharp;
using Newtonsoft.Json;
using KBTech.Integration.Merchant.CoinBase;
using KBTech.Integration.Merchant;
using WayWealth.Areas.lk.Models.CoinBase;
using System.Text;
using WayWealth.Areas.lk.Helpers;
using JWT;
using Binance.Net;
using System.Text.RegularExpressions;
using Google.Authenticator;

namespace WayWealth.Areas.lk.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult ProfileBlock()
        {
            // todo: проверка баланса
            var value = DataService.GetOwnAndRefsValue(this.User.id_object);
            ViewBag.Rank = GetRankFull(value);
            ViewBag.CamulativePlaceCount = DataService.GetPlaceCount(93, this.User.id_object);


            ViewBag.StructSavingsBalance = DataService.GetStructSavingsBalance(this.User.id_object);

            // Уведомления
            var notices = DataService.GetUnreadedNotices(this.User.id_object);
            if (notices.Count() > 0)
            {
                foreach (var item in notices)
                {
                    Session.AddNotification(new Notification(true, Resources.Resource.Notification, item.ObjectName));
                    DataService.SetNoticeAsReaded(item.id_object);
                }
            }
            return PartialView("_ProfileBlock", this.User);
        }

        [ChildActionOnly]
        public PartialViewResult News() => PartialView("_News", DataService.GetNews());

        #region голосование
        [ChildActionOnly]
        public PartialViewResult Polls()
        {
            return PartialView(DataService.GetPolls());
        }

        [ChildActionOnly]
        public PartialViewResult QuestionBlock(uint pollId)
        {
            var variants = DataService.GetPollVariants(pollId);
            var choise = DataService.GetPollChoise(pollId, this.User.id_object);
            ViewBag.Choise = choise;

            // для каждого варианта ответа надо определить процентное соотношение

            if (choise != null)
                return PartialView("_QuestionBlock", variants);

            return PartialView("_NoQuestionBlock", variants);
        }

        public void SelectQuestion(uint pollVariantId)
        {
            DataService.SelectPollVariant(pollVariantId: pollVariantId, userId: User.id_object);
            AuthenticationService.UpdateCookies(User.id_object);
        }
        #endregion

        public PartialViewResult _PaymentArticles(HistoryView model)
        {
            return PartialView(model);
        }

        public ViewResult History(DateTime? begin, DateTime? end, string article, string account = "Остаток.ВнутреннийСчет",
            int page = 1)
        {
            var items = DataService.GetInnerTransfers(
                partnerId: (uint) User.id_object,
                account: account == "all" ? null : account,
                begin: begin,
                end: end,
                article: article,
                orderId: null,
                filter: null).OrderByDescending(x => x.PaymentDate);

            var model = new HistoryView();
            for (var i = 0; i < items.Count(); i++)
            {
                items.ElementAt(i).PaymentNumber = i + 1;
                model.TotalSumPrihod += items.ElementAt(i).DebetSum ?? 0;
                model.TotalSumRashod += items.ElementAt(i).CreditSum ?? 0;
            }

            model.Items = GetPage<InnerTransfer>(items, page, 10);
            model.account = account;
            model.begin = begin;
            model.end = end;
            model.article = article;

            model.PageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = 10,
                TotalItems = items.Count()
            };
            return View(model);

        }

        [HttpGet]
        public ViewResult Transfer()
        {
            TempData["Остаток.ВнутреннийСчет"] = this.User.BalanceInner;
            TempData["Остаток.Проценты"] = this.User.BalancePercents;
            TempData["Остаток.ИнвестиционныйСчет"] = this.User.BalanceInvestments;
            TempData["Остаток.Вознаграждения"] = this.User.BalanceRewards;
            return View();
        }

        [HttpGet]
        public ActionResult Transfer2()
        {
            TempData["Остаток.ВнутреннийСчет"] = this.User.BalanceInner;
            return View();
        }

        public ActionResult GenerateTransactionPassword()
        {
            var tp = new TransactionPassword
            {
                Password = RandomString(6),
                GenerateDate = DateTime.Now
            };
            Session["TansactionPassword"] = tp;
            // не помню шифруются ли сессионные куки, надо будет потом в конце проверить

            this.MailService.SendMessage(
                  to: this.User.Email,
                  subject: Resources.Resource.FinancialPassword,
                  body: string.Format(Resources.Resource.LetterFinancialPassword, tp.Password));

            //StringBuilder message = new StringBuilder();
            //var culture = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            //if (culture == "ru-RU")
            //{
            //    this.MailService.SendMessage(
            //       to: this.User.Email,
            //       subject: Resources.Resource.FinancialPassword,
            //       body: $"Пароль для совершения операции {tp.Password}. Пароль действителен в течении 20 минут.");
            //}
            //else
            //{
            //    this.MailService.SendMessage(
            //       to: this.User.Email,
            //       subject: Resources.Resource.FinancialPassword,
            //       body: $"Password to perform the operation {tp.Password}. The password is valid for 20 minutes.");
            //}

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }




        [HttpPost]
        public ActionResult Transfer(TransferView model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var tp = Session["TansactionPassword"] as TransactionPassword;
                    if (tp == null || tp.Password != model.Password)
                    {
                        throw new Exception(Resources.Resource.ErrPasswordIsIncorrect);
                        // в принципе, можно сбрасывать пароль если указан неверно
                        //Session["TansactionPassword"] = null;
                    }

                    if ((DateTime.Now - tp.GenerateDate).TotalMinutes > 20)
                        throw new Exception(Resources.Resource.ErrPasswordIsExpired);

                    if (model.Sum < 0)
                        throw new Exception(Resources.Resource.ErrSumIsIncorrect);

                    #region Проверка правильности введенного счета
                    string account = "";
                    if (model.Account == "Остаток.Проценты")
                        account = "Остаток.Проценты";
                    else if (model.Account == "Остаток.Вознаграждения")
                        account = "Остаток.Вознаграждения";
                    if (string.IsNullOrWhiteSpace(account))
                        throw new Exception(Resources.Resource.ErrAccountIsIncorrect);
                    #endregion

                    var partner = this.DataService.GetPartner(this.User.id_object);
                    if (account == "Остаток.Проценты" && partner.BalancePercents < model.Sum)
                        throw new Exception(Resources.Resource.ErrInsufficientFun);

                    if (account == "Остаток.Вознаграждения" && (partner.BalanceRewards ?? 0) < model.Sum)
                        throw new Exception(Resources.Resource.ErrInsufficientFun);

                    #region Перевод
                    this.DataService.PayPayment(this.DataService.CreateInnerTransfer(
                        accountName: account,
                        direction: TransferDirection.Output,
                        companyId: 5,
                        partnerId: this.User.id_object,
                        documentId: this.User.id_object,
                        article: "Перевод на лицевой счет",
                        date: DateTime.Now,
                        sum: model.Sum,
                        paymentMethod: PaymentMethod.Inner,
                        comment: string.IsNullOrWhiteSpace(model.Desctiption) ? "Перевод на лицевой счет" : $"Перевод на лицевой счет ({model.Desctiption})",
                        //comment: "Перевод на лицевой счет",
                        documentType: null,
                        user: ConfigurationManager.AppSettings["login"]), DateTime.Now);

                    this.DataService.PayPayment(this.DataService.CreateInnerTransfer(
                       accountName: "Остаток.ВнутреннийСчет",
                       direction: TransferDirection.Input,
                       companyId: 5,
                       partnerId: this.User.id_object,
                       documentId: this.User.id_object,
                       article: "Перевод на лицевой счет",
                       date: DateTime.Now,
                       sum: model.Sum,
                       paymentMethod: PaymentMethod.Inner,
                       comment: string.IsNullOrWhiteSpace(model.Desctiption) ? "Перевод на лицевой счет" : $"Перевод на лицевой счет ({model.Desctiption})",
                       //comment: "Перевод на лицевой счет",
                       documentType: null,
                       user: ConfigurationManager.AppSettings["login"]), DateTime.Now);
                    #endregion

                    Session.AddNotification(new Notification(true, Resources.Resource.MoneyTransactionTitle, 
                        Resources.Resource.OperationCompleted));
                    this.AuthenticationService.UpdateCookies(this.User.id_object);
                    return RedirectToAction("history");
                }
                catch (Exception exc)
                {
                    //ModelState.AddModelError(string.Empty, exc.Message);
                    Session.AddNotification(new Notification(false, Resources.Resource.MoneyTransactionTitle,
                        exc.Message));
                }
            }
            else
            {
                GetModelErrors(Resources.Resource.MoneyTransactionTitle);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Transfer2(Transfer2View model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var tp = Session["TansactionPassword"] as TransactionPassword;
                    if (tp == null || tp.Password != model.Password)
                    {
                        throw new Exception(Resources.Resource.ErrPasswordIsIncorrect);
                        // в принципе, можно сбрасывать пароль если указан неверно
                        //Session["TansactionPassword"] = null;
                    }

                    if ((DateTime.Now - tp.GenerateDate).TotalMinutes > 20)
                        throw new Exception(Resources.Resource.ErrPasswordIsExpired);

                    if (model.Sum < 0)
                        throw new Exception(Resources.Resource.ErrSumIsIncorrect);

                    var partner = this.DataService.GetPartner(this.User.id_object);
                    if ((partner.BalanceInner ?? 0) < model.Sum)
                        throw new Exception(Resources.Resource.ErrInsufficientFun);

                    var recepient = this.DataService.GetPartner(model.Login);
                    if (recepient == null)
                        throw new Exception(Resources.Resource.ErrRecipientNotFound);

                    if (recepient.id_object == partner.id_object)
                        throw new Exception(Resources.Resource.ErrRecipientMatchesTheSender);

                    var senderlogin = string.IsNullOrWhiteSpace(partner.Login) ? partner.PersonalId.ToString() : partner.Login;
                    var recipientlogin = string.IsNullOrWhiteSpace(recepient.Login) ? recepient.PersonalId.ToString() : recepient.Login;

                    #region Перевод
                    this.DataService.PayPayment(this.DataService.CreateInnerTransfer(
                        accountName: "Остаток.ВнутреннийСчет",
                        direction: TransferDirection.Output,
                        companyId: 5,
                        partnerId: partner.id_object,
                        documentId: recepient.id_object, 
                        article: "Перевод партнеру",
                        date: DateTime.Now,
                        sum: model.Sum,
                        paymentMethod: PaymentMethod.Inner,
                        comment: string.IsNullOrWhiteSpace(model.Desctiption) ? $"Перевод партнеру {recipientlogin}" : $"Перевод партнеру {recipientlogin} ({model.Desctiption})",
                        //comment: "Перевод партнеру",
                        documentType: null,
                        user: ConfigurationManager.AppSettings["login"]), DateTime.Now);

                    this.DataService.PayPayment(this.DataService.CreateInnerTransfer(
                       accountName: "Остаток.ВнутреннийСчет",
                       direction: TransferDirection.Input,
                       companyId: 5,
                       partnerId: recepient.id_object,
                       documentId: partner.id_object,
                       article: "Перевод партнеру",
                       date: DateTime.Now,
                       sum: model.Sum,
                       paymentMethod: PaymentMethod.Inner,
                       comment: string.IsNullOrWhiteSpace(model.Desctiption) ? $"Перевод от партнера {senderlogin}" : $"Перевод от партнера {senderlogin} ({model.Desctiption})",
                       documentType: null,
                       user: ConfigurationManager.AppSettings["login"]), DateTime.Now);
                    #endregion

                    Session.AddNotification(new Notification(true, Resources.Resource.MoneyTransactionTitle,
                        Resources.Resource.OperationCompleted));

                    this.AuthenticationService.UpdateCookies(this.User.id_object);
                    return RedirectToAction("history");
                }
                catch (Exception exc)
                {
                    //ModelState.AddModelError(string.Empty, exc.Message);
                    Session.AddNotification(new Notification(false, Resources.Resource.MoneyTransactionTitle,
                        exc.Message));
                }
            }
            else
            {
                GetModelErrors(Resources.Resource.MoneyTransactionTitle);

            }
                
                

            return View(model);
        }

        [HttpGet]
        public ActionResult Withdraw()
        {
            // Запрещаем открытие страницы с формой заявки на вывод средств если сегодня вывод запрещен
            if (!PagingHelpers.WithdrawTime())
            {
                Session.AddNotification(new Notification(false, Resources.Resource.WithdrawalError, Resources.Resource.WithdrawalNotFriday));
                return RedirectToAction("index");
            }
            
            CoinBaseService service = new CoinBaseService(ConfigurationManager.AppSettings["coinbase.apikey"]);
            GetPrice(service, GetWithdrawPercent(), "BTC", "ETH", "LTC", "BCH", "XRP");

            TempData["w_btc"] = User.AccountBitcoin;
            TempData["w_eth"] = User.AccountEthereum;
            TempData["w_ltc"] = User.AccountLitecoin;
            TempData["w_bch"] = User.AccountBitcoinCash;
            TempData["w_xrp"] = User.AccountRipple;

            TempData["Остаток.ВнутреннийСчет"] = User.BalanceInner;

            var model = new WithdrawView();
            if (!string.IsNullOrWhiteSpace(this.User.AccountBitcoin))
            {
                model.Currency = "BTC";
                model.WalletAddress = this.User.AccountBitcoin;
            }
            else if (!string.IsNullOrWhiteSpace(this.User.AccountEthereum))
            {
                model.Currency = "ETH";
                model.WalletAddress = this.User.AccountEthereum;
            }
            else if (!string.IsNullOrWhiteSpace(this.User.AccountLitecoin))
            {
                model.Currency = "LTC";
                model.WalletAddress = this.User.AccountLitecoin;
            }
            else if (!string.IsNullOrWhiteSpace(this.User.AccountBitcoinCash))
            {
                model.Currency = "BCH";
                model.WalletAddress = this.User.AccountBitcoinCash;
            }
            else if (!string.IsNullOrWhiteSpace(this.User.AccountRipple))
            {
                model.Currency = "XRP";
                model.WalletAddress = this.User.AccountRipple;
            }
            else
            {
                model.Currency = "BTC";
                model.WalletAddress = this.User.AccountBitcoin;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Withdraw(WithdrawView model)
        {

            // Запрещаем открытие страницы с формой заявки на вывод средств если сегодня вывод запрещен
            if (!PagingHelpers.WithdrawTime())
            {
                Session.AddNotification(new Notification(false, Resources.Resource.WithdrawalError, Resources.Resource.WithdrawalNotFriday));
                return RedirectToAction("index");
            }
            

            if (ModelState.IsValid)
            {
                try
                {
                    var tp = Session["TansactionPassword"] as TransactionPassword;
                    if (tp == null || tp.Password != model.Password)
                    {
                        throw new Exception(Resources.Resource.ErrPasswordIsIncorrect);
                        // в принципе, можно сбрасывать пароль если указан неверно
                        //Session["TansactionPassword"] = null;
                    }

                    if ((DateTime.Now - tp.GenerateDate).TotalMinutes > 20)
                        throw new Exception(Resources.Resource.ErrPasswordIsExpired);

                    if (model.Sum < 0 || model.Sum < 100)
                        throw new Exception(Resources.Resource.ErrSumIsIncorrect);

                    var partner = DataService.GetPartner(this.User.id_object);
                    if ((partner.BalanceInner ?? 0) < model.Sum)
                        throw new Exception(Resources.Resource.ErrInsufficientFun);

                    #region Action

                    #region currencySum
                    CoinBaseService service = new CoinBaseService(ConfigurationManager.AppSettings["coinbase.apikey"]);
                    decimal price = 0;
                    if (model.Currency != "XRP")
                    {
                        price = service.GetSpotPrice($"{model.Currency}-USD").Data.Amount;
                    }
                    else
                    {
                        price = GetXrpUsdRate(service);
                    }

                    var percent = GetWithdrawPercent();
                    var currencySum = model.Sum * (1 + percent) / price;
                    #endregion


                    var id_order = DataService.CreateWithdrawalRequest(
                        accountName: "Остаток.ВнутреннийСчет",
                        companyId: 5,
                        partnerId: this.User.id_object,
                        article: "Вывод средств",
                        date: DateTime.Now,
                        sum: model.Sum,
                        currencySum: currencySum,
                        currency: model.Currency,
                        wallet: model.WalletAddress,
                        tag: model.DestTag,
                        paymentSystem: "Binance",
                        paymentMethod: PaymentMethod.Crypto,
                        comment: "Вывод средств",
                        documentType: "ЗаявкаНаВыводСредств",
                        user: ConfigurationManager.AppSettings["login"]);

                    DataService.PayPayment(id_order, DateTime.Now);

                    #endregion

                    Session.AddNotification(new Notification(true, Resources.Resource.WithdrawalTitle, Resources.Resource.OperationCompleted));
                    this.AuthenticationService.UpdateCookies(this.User.id_object);
                    return RedirectToAction("history");
                }
                catch (Exception exc)
                {
                    ModelState.AddModelError(string.Empty, exc.Message);
                }
            }
            return View(model);
        }


        decimal GetXrpUsdRate(CoinBaseService service)
        {
            using (var client = new BinanceClient())
            {
                var XRP_BTC = client.GetPrice("XRPBTC").Data.Price;
                var BTC_USD = service.GetSpotPrice("BTC-USD").Data.Amount;
                return XRP_BTC * BTC_USD;
            }
            //var m= Servic
        }

        void GetPrice(CoinBaseService service, decimal percent, params string[] currencies)
        {
            foreach (var curr in currencies)
            {
                if (curr == "XRP")
                {
                    var xrp_usd = GetXrpUsdRate(service);
                    TempData[$"xrp_usd"] = xrp_usd / (1 + percent);
                    TempData[$"usd_xrp"] = (1 + percent) / xrp_usd;
                }
                else
                {
                    var curr_usd = service.GetSpotPrice($"{curr.ToUpper()}-USD");
                    if (curr_usd.Data != null)
                    {
                        TempData[$"{curr.ToLower()}_usd"] = curr_usd.Data.Amount / (1 + percent);
                        TempData[$"usd_{curr.ToLower()}"] = (1 + percent) / curr_usd.Data.Amount;
                    }
                    else
                    {
                        TempData[$"{curr.ToLower()}_usd"] = 0;
                        TempData[$"usd_{curr.ToLower()}"] = 0;
                    }
                }
            }
        }

        decimal GetReplenishmentPercent()
        {
            decimal percent = 0m;
            var company = this.DataService.GetPartner(5);
            if (company != null)
                percent = company.Settings_ReplenishmentPercent ?? 0m;
            return percent;
        }

        decimal GetWithdrawPercent()
        {
            decimal percent = 0m;
            var company = this.DataService.GetPartner(5);
            if (company != null)
                percent = company.Settings_WithdrawalPercent ?? 0m;
            return percent;
        }




        [HttpGet]
        public ActionResult Replenish()
        {
            CoinBaseService service = new CoinBaseService(ConfigurationManager.AppSettings["coinbase.apikey"]);
            GetPrice(service, GetReplenishmentPercent(), "BTC", "ETH", "LTC", "BCH", "XRP");
            return View();
        }

        [HttpPost]
        public ActionResult Replenish(ReplenishView model)
        {
            if (ModelState.IsValid)
            {
                #region currencySum
                CoinBaseService service = new CoinBaseService(ConfigurationManager.AppSettings["coinbase.apikey"]);
                decimal price = 0;
                if (model.Currency != "XRP")
                {
                    price = service.GetSpotPrice($"{model.Currency}-USD").Data.Amount;
                }
                else
                {
                    price = GetXrpUsdRate(service);
                }

                var percent = GetReplenishmentPercent();
                var currencySum = model.Sum * (1 + percent) / price;
                #endregion

                var id_order = this.DataService.CreateReplenishRequest(
                    accountName: "Остаток.ВнутреннийСчет",
                    companyId: 5,
                    partnerId: this.User.id_object,
                    article: "Пополнение счета",
                    date: DateTime.Now,
                    sum: model.Sum,
                    currencySum: currencySum,
                    currency: model.Currency,
                    paymentSystem: "CoinBase",
                    paymentMethod: PaymentMethod.Crypto,
                    comment: "Пополнение счета",
                    documentType: "ЗапросНаВебПлатеж",
                    user: ConfigurationManager.AppSettings["login"]);

                string tempAddress = "";
                string transactionId = "";

                if (model.Currency != "XRP")
                {
                    // coin base
                    var resp = service.CryptoInvoice(
                      amount: currencySum,
                      currency: model.Currency,
                      partner: this.User.id_object,
                      order: id_order,
                      desc: $"Пополнение счета {model.Currency} (" + this.User.id_object + ")");

                    if (resp != null)
                    {
                        if (model.Currency == "BTC")
                            tempAddress = resp.Data.Addresses[ChargeAddresses.bitcoin];
                        else if (model.Currency == "ETH")
                            tempAddress = resp.Data.Addresses[ChargeAddresses.ethereum];
                        else if (model.Currency == "LTC")
                            tempAddress = resp.Data.Addresses[ChargeAddresses.litecoin];
                        else if (model.Currency == "BCH")
                            tempAddress = resp.Data.Addresses[ChargeAddresses.ethereum];
                        else
                            tempAddress = resp.Data.Addresses[ChargeAddresses.ethereum];

                        transactionId = resp.Data.Code;
                    }
                }
                else
                {
                    //direct
                    tempAddress = "rLVCR7Y8yiuAQgB83Ek8JN3EL219B5Vr24";
                }



                if (!string.IsNullOrWhiteSpace(tempAddress))
                {
                    this.DataService.UpdateWebPaymentRequest(
                            id_object: id_order,
                            transactionId: transactionId,
                            address: tempAddress);
                    return RedirectToAction("CryptoInvoice", new { id = id_order });
                }
                else
                {
                    DataService.RemovePayment(id_order);
                }
            }
            return View(model);
        }
        
        public ActionResult CryptoInvoice(uint id)
        {
            var model = DataService.GetWebPaymentRequest(id);
            return View(model);
        }


        [HttpGet]
        public ViewResult Charity()
        {
            TempData["Reminder"] = User.BalanceInner;
            TempData["Funds"] = DataService.GetCharity();
            return View();
        }

        
        public PartialViewResult _CharityDetails(uint foundId = 0, int page = 1)
        {
            int pageSize = 10;
            var payments = DataService.GetInnerTransfers(
              partnerId: null,
              account: "Остаток.ВнутреннийСчет",
              article: "Благотворительность",
              begin: null,
              end: null,
              orderId: foundId,
              filter: null);

            if (payments != null)
                ViewBag.Payments = GetPage<InnerTransfer>(payments, page, pageSize);

            // благодарственные письма
            ViewBag.Letters = DataService.GetFiles(foundId);


            ViewBag.PageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = payments.Count()
            };

            return PartialView(foundId);
        }

        [HttpPost]
        public ActionResult Charity(CharityView model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var tp = Session["TansactionPassword"] as TransactionPassword;
                    if (tp == null || tp.Password != model.Password)
                    {
                        throw new Exception(Resources.Resource.ErrPasswordIsIncorrect);
                        // в принципе, можно сбрасывать пароль если указан неверно
                        //Session["TansactionPassword"] = null;
                    }

                    if ((DateTime.Now - tp.GenerateDate).TotalMinutes > 20)
                        throw new Exception(Resources.Resource.ErrPasswordIsExpired);

                    if (model.Sum < 0)
                        throw new Exception(Resources.Resource.ErrSumIsIncorrect);

                    var partner = DataService.GetPartner(this.User.id_object);
                    if ((partner.BalanceInner ?? 0) < model.Sum)
                        throw new Exception(Resources.Resource.ErrInsufficientFun);

                    var fund = this.DataService.GetCharity(model.FondId);
                    if (fund == null)
                        throw new Exception(Resources.Resource.ErrFoundationNotFound);

                    #region Action

                    var id_order = DataService.CreateInnerTransfer(
                        accountName: "Остаток.ВнутреннийСчет",
                        direction: TransferDirection.Output,
                        companyId: 5,
                        partnerId: this.User.id_object,
                        documentId: fund.id_object,
                        article: "Благотворительность",
                        date: DateTime.Now,
                        sum: model.Sum,
                        paymentMethod: PaymentMethod.Inner,
                        comment: $"Благотворительность в {fund.ObjectName}",
                        documentType: null,
                        user: ConfigurationManager.AppSettings["login"]);

                    DataService.PayPayment(id_order, DateTime.Now);

                    #endregion

                    Session.AddNotification(new Notification(true, Resources.Resource.AticleCharity, Resources.Resource.OperationCompleted));
                    this.AuthenticationService.UpdateCookies(this.User.id_object);
                    return RedirectToAction("history");

                }
                catch (Exception exc)
                {
                    ModelState.AddModelError(string.Empty, exc.Message);
                    //Session.AddNotification(new Notification(false, "Благотворительность", exc.Message));
                }
            }
            
            return View(model);
        }


        public ActionResult Promo() => View(DataService.GetPromos());

        [HttpGet]
        public ActionResult Settings()
        {
            var partner = DataService.GetPartner(User.id_object);     

            var culture = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            if (culture == "ru-RU")
            {
                TempData["Countries"] = DataService.GetDictItems("СтраныРусс");
            }
            else
            {
                TempData["Countries"] = DataService.GetDictItems("СтраныАнгл");
            }

            return View(Mapper.Map<SettingsView>(partner));
        }

        [HttpPost]
        public ActionResult TwoFactorAuthDisable()
        {
            var partner = DataService.GetPartner(User.id_object);

            if ((partner.GoogleAuthEnabled ?? false))
            {
                string GoogleAuthUserUniqueKey = (partner.Login + PagingHelpers.GetUniqueKey(10));
                Session["GoogleAuthUserUniqueKey"] = GoogleAuthUserUniqueKey;
                TwoFactorAuthenticator TwoFacAuth = new TwoFactorAuthenticator();
                var setupInfo = TwoFacAuth.GenerateSetupCode(ConfigurationManager.AppSettings["googleAuthIssuer"], partner.Email, GoogleAuthUserUniqueKey, 300, 300, true);
                ViewBag.BarcodeImageUrl = setupInfo.QrCodeSetupImageUrl;
                ViewBag.SetupCode = setupInfo.ManualEntryKey;
            }

            ViewBag.GoogleAuthEnabled = (partner.GoogleAuthEnabled ?? false);

            return View();
        }


        [HttpGet]
        public ActionResult TwoFactorAuth()
        {
            var partner = DataService.GetPartner((uint) User.id_object);

            if (!(partner.GoogleAuthEnabled ?? false))
            {
                string GoogleAuthUserUniqueKey = (partner.Login + PagingHelpers.GetUniqueKey(10));
                Session["GoogleAuthUserUniqueKey"] = GoogleAuthUserUniqueKey;
                TwoFactorAuthenticator TwoFacAuth = new TwoFactorAuthenticator();
                var setupInfo = TwoFacAuth.GenerateSetupCode(ConfigurationManager.AppSettings["googleAuthIssuer"], partner.Email, GoogleAuthUserUniqueKey, 300, 300, true);
                ViewBag.BarcodeImageUrl = setupInfo.QrCodeSetupImageUrl;
                ViewBag.SetupCode = setupInfo.ManualEntryKey;
                
            }

            ViewBag.GoogleAuthEnabled = (partner.GoogleAuthEnabled ?? false);

            return View();
        }

        [HttpPost]
        public ActionResult TwoFactorAuth(TwoFactorAuthView model)
        {
            var partner = DataService.GetPartner((uint)User.id_object);
            ViewBag.GoogleAuthEnabled = (partner.GoogleAuthEnabled ?? false);
            TwoFactorAuthenticator TwoFacAuth = new TwoFactorAuthenticator();

            string GoogleAuthUserUniqueKey = partner.GoogleAuthKey;
            bool AuthToStatus = false;

            if (!(partner.GoogleAuthEnabled ?? false))
            {
                GoogleAuthUserUniqueKey = Session["GoogleAuthUserUniqueKey"].ToString();
                var setupInfo = TwoFacAuth.GenerateSetupCode(ConfigurationManager.AppSettings["googleAuthIssuer"], partner.Email, GoogleAuthUserUniqueKey, 300, 300, true);
                ViewBag.BarcodeImageUrl = setupInfo.QrCodeSetupImageUrl;
                ViewBag.SetupCode = setupInfo.ManualEntryKey;
                AuthToStatus = true;
            }

            bool isValid = TwoFacAuth.ValidateTwoFactorPIN(GoogleAuthUserUniqueKey, model.CodeDigit);

            if (ModelState.IsValid)
            {
                try
                {
                    if (isValid)
                    {
                        DataService.UpdateGoogleAuthKey(
                           id_client: User.id_object,
                           GoogleAuthKey: GoogleAuthUserUniqueKey,
                           GoogleAuthEnabled: AuthToStatus
                       );

                        this.AuthenticationService.UpdateCookies(User.id_object);

                        if (AuthToStatus)
                        {
                            Session.Remove("GoogleAuthUserUniqueKey");
                        }

                        Session.AddNotification(new Notification(true, Resources.Resource.ProfileTitle, Resources.Resource.FAChanged));
                        return RedirectToAction("TwoFactorAuth", "home", new { area = "lk" });
                    }
                    else
                    {
                        throw new Exception(Resources.Resource.GoogleAuthIncorrectCode);
                    }
                }
                catch (Exception exc)
                {
                    Session.AddNotification(new Notification(false, Resources.Resource.ProfileTitle, exc.Message));
                }
            }
            else
            {
                GetModelErrors(Resources.Resource.ProfileTitle);
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult Settings(SettingsView model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = DataService.GetPartner(model.Login.Trim());

                    if (user != null && user.id_object != User.id_object)
                        throw new Exception(Resources.Resource.ErrLoginIsAlreadyUsed);

                    DataService.UpdatePartnerProfile(
                        id_client: User.id_object,
                        login: model.Login,
                        lastname: model.LastName,
                        firstname: model.FirstName,
                        middlename: model.MiddleName,
                        gender: model.Gender,
                        country: model.Country,
                        city: model.City,
                        birthdate: model.BirthDate,
                        phonenumber: model.PhoneNumber,
                        soclink: model.SocNetworkLink);

                    this.AuthenticationService.UpdateCookies(User.id_object);
                    user = base.DataService.GetPartner(User.id_object);

                    var usm = Mapper.Map<UserSerializeModel>(user);
                    HttpContext.User = new CustomPrincipal(usm);

                    Session.AddNotification(new Notification(true, Resources.Resource.ProfileTitle, Resources.Resource.ProfileIsChanged));
                }
                catch (Exception exc)
                {
                    //ModelState.AddModelError(string.Empty, exc.Message);
                    Session.AddNotification(new Notification(false, Resources.Resource.ProfileTitle, exc.Message));
                }
            }
            else
            {
                GetModelErrors(Resources.Resource.ProfileTitle);
            }
            return View(model);
        }

        void GetModelErrors(string title)
        {
            StringBuilder errors = new StringBuilder();
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    errors.AppendFormat("<p>{0}<p>", error.ErrorMessage);
                }
            }
            if (errors.Length > 0)
            {
                Session.AddNotification(new Notification(false, title,
                        errors.ToString()));
            }

        }

        

        [HttpPost]
        public ActionResult ChangePassword(string newPassword, string passwordRepeat)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(newPassword))
                {
                    if (newPassword != passwordRepeat)
                        throw new Exception(Resources.Resource.ErrPasswordsDoNotMatch);

                    DataService.UpdatePartnerPassword(
                        id_client: User.id_object,
                        password: newPassword);

                    Session.AddNotification(new Notification(true, Resources.Resource.ChangePassword, Resources.Resource.PasswordСhanged));
                }
            }
            catch (Exception exc)
            {
                //ModelState.AddModelError(string.Empty, exc.Message);
                Session.AddNotification(new Notification(false, Resources.Resource.ChangePassword, exc.Message));
            }

            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult ChangeEmail(string password, string email)
        {
            try
            {
                var user = DataService.GetPartnerByEmail(email.Trim());

                if (user != null && user.id_object != User.id_object)
                    throw new Exception(Resources.Resource.ErrEmailIsAlreadyRegistred);

                if (password != user.Password)
                    throw new Exception(Resources.Resource.ErrPasswordIsIncorrect);

                if (user == null)
                {
                    DataService.UpdatePartnerEmail(
                        id_client: User.id_object,
                        email: email.Trim());

                    this.AuthenticationService.UpdateCookies(this.User.id_object);
                }
            }
            catch (Exception exc)
            {
                ModelState.AddModelError(string.Empty, exc.Message);
            }

            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult Verification(HttpPostedFileBase passport1, HttpPostedFileBase passport2,
            HttpPostedFileBase passport3)
        {
            if (ModelState.IsValid)
            {
                var passport = new List<string>();
                var localPathes = new List<string>();
                try
                {
                    if (CheckImg(passport1) && CheckImg(passport2) && CheckImg(passport3))
                    {
                        var partner = this.DataService.GetPartner(this.User.id_object);
                        foreach (var file in new HttpPostedFileBase[] { passport1, passport2, passport3 })
                        {
                            var path = LocalUploading(file);
                            passport.Add(DataService.WriteFile(path));
                            localPathes.Add(path);
                        }
                        #region Удалить предыдущие
                        foreach (var p in new[] { partner.Passport1, partner.Passport2, partner.Passport3 })
                        {
                            if (!string.IsNullOrWhiteSpace(p))
                            {
                                try
                                {
                                    this.DataService.DeleteFile(p);
                                }
                                catch { }
                            }
                        }
                        #endregion

                        // установить статус верификации
                        DataService.UpdatePartnerPassport(this.User.id_object, passport[0], passport[1], passport[2]);
                        DataService.UpdateParnterVerificationStatus(this.User.id_object, "На проверке");
                        Session.AddNotification(new Notification(true, Resources.Resource.UploadingOfDocuments, Resources.Resource.DocumentsAreUploaded));
                    }
                }
                catch (Exception exc)
                {
                    Session.AddNotification(new Notification(false, Resources.Resource.UploadingOfDocuments, Resources.Resource.ErrUploading));
                }
                finally
                {
                    foreach (var path in localPathes)
                    {
                        try
                        {
                            System.IO.File.Delete(path);
                        }
                        catch { }
                    }
                }
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
        public string LocalUploading(HttpPostedFileBase file)
        {
            string guild = Guid.NewGuid().ToString();
            string path = string.Empty;


            if (!Directory.Exists(Server.MapPath("~/Content/temp")))
                Directory.CreateDirectory(Server.MapPath("~/Content/temp"));

            var stream = file.InputStream;
            var fileName = Path.GetFileName(file.FileName);
            path = Path.Combine(Server.MapPath("~/Content/temp"), guild + fileName);

            using (var fileStream = System.IO.File.Create(path))
            {
                stream.CopyTo(fileStream);
            }
            return path;
        }
        public bool CheckImg(HttpPostedFileBase img)
        {
            if (img == null || img.ContentLength == 0)
                return false;

            if (img.ContentLength > 2097152)
            {
                Session.AddNotification(new Notification(false, Resources.Resource.UploadingOfDocuments,
                    Resources.Resource.ErrImgSize));

                return false;
            }

            if (img.ContentType.ToLower() != "image/jpg" &&
                img.ContentType.ToLower() != "image/jpeg" &&
                img.ContentType.ToLower() != "image/pjpeg" &&
                img.ContentType.ToLower() != "image/x-png" &&
                img.ContentType.ToLower() != "image/png")
            {
                Session.AddNotification(new Notification(false, Resources.Resource.UploadingOfDocuments,
                    Resources.Resource.OnlyImagesAllowed));
                return false;
            }

            return true;
        }

        public ActionResult UploadAvatar()
        {
            string guild = Guid.NewGuid().ToString();
            string path = string.Empty;

            var upload = Request.Files[0];

            try
            {
                if (CheckImg(upload))
                {
                    path = LocalUploading(upload);
                    if (!string.IsNullOrWhiteSpace(User.Image))
                    {
                        try
                        {
                            DataService.DeleteFile(this.User.Image);
                        }
                        catch { }
                    }
                    var avatarPath = DataService.WriteFile(path);
                    DataService.UpdatePartnerAvatar(avatarPath, User.id_object);
                    AuthenticationService.UpdateCookies(User.id_object);
                }
            }
            catch (Exception exc)
            {
                Session.AddNotification(new Notification(false, Resources.Resource.UploadingOfDocuments, Resources.Resource.CouldNotUpdateAvatar));
                return Redirect(Request.UrlReferrer.ToString());
            }
            finally
            {
                try
                {
                    System.IO.File.Delete(path);
                }
                catch (Exception e)
                {
                    // ignored
                }
            }
            Session.AddNotification(new Notification(true, Resources.Resource.UploadingOfDocuments, Resources.Resource.AvatarSuccessfullyUpdated));
            return Redirect(Request.UrlReferrer.ToString());
        }

        public void DeleteAvatar()
        {
            if (!string.IsNullOrWhiteSpace(User.Image))
            {
                DataService.DeleteFile(User.Image);
                AuthenticationService.UpdateCookies(User.id_object);
                DataService.UpdatePartnerAvatar(null, User.id_object);
            }
        }



        [HttpPost]
        public ActionResult SaveWallets(string AccountBitcoin, 
            string AccountEthereum, string AccountLitecoin,string AccountBitcoinCash, string AccountRipple)
        {
            this.DataService.UpdatePartnerWallets(
                id_client: this.User.id_object,
                walletBitcoin: AccountBitcoin,
                walletEthereum: AccountEthereum,
                walletLitecoin: AccountLitecoin,
                walletBitcoinCash: AccountBitcoinCash,
                walletRipple: AccountRipple);
            this.AuthenticationService.UpdateCookies(this.User.id_object);
            return Redirect(Request.UrlReferrer.ToString());
        }


        public ActionResult Partners(PartnersView model, int page = 1)
        {
            var partnersList = DataService.GetPartners(User.id_object, model.SearchText);
            ViewBag.Investments = this.DataService.GetReferInvestments(this.User.id_object);
            ViewBag.Partners = GetPage<Partner>(partnersList, page, 10);
            ViewBag.PageInfo = new PageInfo()
            {
                PageNumber = page,
                PageSize = 10,
                TotalItems = partnersList.Count()
            };

            return View(model);
        }

        #region Struct 
        string GetRank(decimal value)
        {
            if (value >= 100e3m)
                return Resources.Resource.RankPP;
            else if (value >= 65e3m)
                return Resources.Resource.RankLP;
            else if (value >= 35e3m)
                return Resources.Resource.RankSP;
            else if (value >= 15e3m)
                return Resources.Resource.RankP;
            else if (value >= 1e3m)
                return Resources.Resource.RankJP;
            return null;
        }
        string GetRankFull(decimal value)
        {
            if (value >= 100e3m)
                return Resources.Resource.RankPPfull;
            else if (value >= 65e3m)
                return Resources.Resource.RankLPfull;
            else if (value >= 35e3m)
                return Resources.Resource.RankSPfull;
            else if (value >= 15e3m)
                return Resources.Resource.RankPfull;
            else if (value >= 1e3m)
                return Resources.Resource.RankJPfull;
            return null;
        }
        public PartialViewResult _SearchMarketingPlaces(string searchText, int page = 1)
        {
            var partners = this.DataService.GetPartners(this.User.id_object);
            int pageSize = 10;
            IEnumerable<Partner> items = partners;
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                items = partners.Where(p => p.ObjectName.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) != -1);
            }
               
            ViewBag.Items = GetPage<Partner>(items, page, pageSize);

            ViewBag.PageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = items.Count()
            };


            return PartialView("_SearchMarketingPlaces", searchText);
        }

        //public ActionResult Structure(StructureView model)
        //{
        //    var plans = this.DataService.GetMarketingPlans();
        //    ViewBag.MarketingPlans = plans;
        //    //model.id_client = this.User.id_object;

        //    if (model.MarketingId == 0)
        //        model.MarketingId = plans.ElementAt(0).id_object;

        //    IEnumerable<MarketingPlace> items = null;
        //    if (model.RootId > 0)
        //    {
        //        items = this.DataService.GetStructure(model.MarketingId, model.RootId.Value);
        //    }
        //    else
        //    {
        //        var partnerPlaces = this.DataService.GetPlaces(model.MarketingId, this.User.id_object);
        //        if (partnerPlaces.Count() > 0)
        //            items = this.DataService.GetStructure(model.MarketingId,
        //                partnerPlaces.OrderBy(x => x.hash.Length).First().id_object);
        //    }
        //    ViewBag.Items = items;
        //    //var treeBaseInfo = this.DataService.GetTreeBaseInfo(this.User.id_object);
        //    //if (items != null)
        //    //{
        //    //    model.StructurePlaces = items.OrderByDescending(p => p.Deep).ToList();

        //    //    // todo:скрыть ФИО если не собственный
        //    //    foreach (var place in model.StructurePlaces)
        //    //    {
        //    //        place.Deep = place.Deep - root.Deep;

        //    //        //if (!treeBaseInfo.Any(x => x.id_object == item.RefToPartner.Value))
        //    //        //    item.ObjectName = item.id_object.ToString();
        //    //        place.ObjectName = place.ObjectName + " " + place.PlaceId;

        //    //        //calc left and right
        //    //        var parent = model.StructurePlaces.SingleOrDefault(p => p.id_object == place.RefToParent);
        //    //        if (parent != null)
        //    //        {
        //    //            parent.ChildCount.Add(place.Sort_Position ?? 0, place.ChildCount.Sum(c => c.Value) + (place.IsActive.Value ? 1 : 0));
        //    //            //parent.ChildCount.Add(place.Position, place.ChildCount.Sum(c => c.Value) + 1);
        //    //        }
        //    //    }
        //    //}

        //    return View(model);
        //}

        public ActionResult Structure(StructureView model)
        {
            uint rootId = model.RootId ?? this.User.id_object;
            var root = this.DataService.GetPartner(rootId);

            var items = new List<MarketingPlaceView>();

            #region Get places

            if (model.MarketingType == MarketingTypeView.Camulative)
            {
                var rootPlaces = this.DataService.GetPlaces(93, rootId);
                if (rootPlaces.Count() > 0)
                {
                    var places = this.DataService.GetStructure(93,
                        rootPlaces.OrderBy(x => x.hash.Length).First().id_object);


                    foreach (var place in places)
                    {
                        items.Add(Mapper.Map<MarketingPlaceView>(place));
                    }
                }
                    
            }
            else
            {
                var partners = this.DataService.GetPartners(rootId);
                foreach (var p in partners)
                {
                    items.Add(Mapper.Map<MarketingPlaceView>(p));
                }
            }
            ViewBag.Items = items;
            #endregion
            #region Investments
            var investments = new Dictionary<uint, decimal>();
            var levelvalue = new Dictionary<int, decimal>();
            var structvalue = new Dictionary<uint, decimal>();
            var ownandflvalue = new Dictionary<uint, decimal>();

            //var allpartners = this.DataService.GetPartners(rootId);
            foreach (var item in items)
            {
                var partnerId = item.PartnerId ?? 0;

                if (!investments.ContainsKey(partnerId))
                    investments.Add(partnerId, item.PartnerBalanceInvestments ?? 0);


                var level = (item.hash.Length - root.hash.Length) / 5;
                if (levelvalue.ContainsKey(level))
                    levelvalue[level] += item.PartnerBalanceInvestments ?? 0;
                else
                    levelvalue.Add(level, item.PartnerBalanceInvestments ?? 0);


                // в программе camulative возможно что будет 2 и более мест
                // для этого нужна проверка
                if (!ownandflvalue.ContainsKey(partnerId))
                {
                    //if (item.PartnerInviterId == 68)
                    //{
                    //}

                    ownandflvalue.Add(partnerId, item.PartnerBalanceInvestments ?? 0);
                    if (item.PartnerInviterId != null)
                    {
                        if (ownandflvalue.ContainsKey(item.PartnerInviterId.Value))
                            ownandflvalue[item.PartnerInviterId.Value] += item.PartnerBalanceInvestments ?? 0;
                        else
                            ownandflvalue.Add(item.PartnerInviterId.Value, item.PartnerBalanceInvestments ?? 0);
                    }
                }

                //if (ownandflvalue.ContainsKey(partnerId))
                //    ownandflvalue[partnerId] += item.PartnerBalanceInvestments ?? 0;
                //else
                //    ownandflvalue.Add(partnerId, item.PartnerBalanceInvestments ?? 0);
                
                //// add to parent 
                //if (item.PartnerInviterId != null)
                //{
                //    if (ownandflvalue.ContainsKey(item.PartnerInviterId.Value))
                //        ownandflvalue[item.PartnerInviterId.Value] += item.PartnerBalanceInvestments ?? 0;
                //    else
                //        ownandflvalue.Add(item.PartnerInviterId.Value, item.PartnerBalanceInvestments ?? 0);
                //}
            }

            foreach (var item in items)
            {
                var partnerId = item.PartnerId ?? 0;
                if (!structvalue.ContainsKey(partnerId))
                    structvalue.Add(partnerId, items
                        .Where(x => x.hash.Contains(item.hash) && x.PartnerId.Value != partnerId)
                        .Sum(x => x.PartnerBalanceInvestments ?? 0));
            }

            ViewBag.Investments = investments;
            ViewBag.LevelValue = levelvalue;
            ViewBag.StructValue = structvalue;
            #endregion
            #region Rewards

            #region __old rewards
            //var transfers = this.DataService.GetInnerTransfers(
            //    //partnerId: this.User.id_object,
            //    partnerId: rootId,
            //    account: "Остаток.Вознаграждения",
            //    article: null,
            //    begin: null,
            //    end: null,
            //    orderId: null,
            //    filter: null);


            //var rewards = new Dictionary<uint, decimal>();
            //var structrewards = new Dictionary<uint, decimal>();
            //foreach (var transfer in transfers)
            //{
            //    var place = items.SingleOrDefault(x => x.id_object == (transfer.OrderId ?? 0));
            //    if (place != null)
            //    {
            //        if (rewards.ContainsKey(place.id_object))
            //            rewards[place.id_object] += transfer.DebetSum ?? 0;
            //        else
            //            rewards.Add(place.id_object, transfer.DebetSum ?? 0);
            //    }
            //}

            #endregion

            var transfers = this.DataService.GetInnerTransfers(
              partnerId: null,
              account: "Остаток.Вознаграждения",
              article: null,
              begin: null,
              end: null,
              orderId: null,
              filter: null);

            

            var rewards = new Dictionary<uint, decimal>();
            var structrewards = new Dictionary<uint, decimal>();
            foreach (var item in items)
            {
                var partnerId = item.PartnerId ?? 0;

                if (model.MarketingType == MarketingTypeView.Camulative)
                {
                    rewards.Add(item.id_object, transfers
                            .Where(x => (x.PartnerId ?? 0) == partnerId && (x.OrderMarketingId ?? 0) > 0)
                            .Sum(x => x.DebetSum ?? 0));
                }
                else
                {
                    //if (partnerId == 85)
                    //{
                    //    ;
                    //}
                    var s = transfers
                            .Where(x => (x.PartnerId ?? 0) == partnerId && (x.OrderMarketingId ?? 0) == 0)
                            .Sum(x => x.DebetSum ?? 0);



                    rewards.Add(item.id_object, transfers
                            .Where(x => (x.PartnerId ?? 0) == partnerId && (x.OrderMarketingId ?? 0) == 0)
                            .Sum(x => x.DebetSum ?? 0));
                }
            }
            foreach (var item in items)
            {
                structrewards.Add(item.id_object,
                    transfers.Where(x => x.hash.Contains(item.hash) && x.OrderId != item.id_object)
                        .Sum(x => x.DebetSum ?? 0));
            }

            ViewBag.Rewards = rewards;
            ViewBag.StructRewards = structrewards;

            #endregion
            #region ranks
            
            foreach (var item in items)
            {
                //if (item.PartnerId.Value == 68)
                //{
                //}

                if (item.PartnerId != null)
                {
                    var value = ownandflvalue[item.PartnerId.Value];
                    item.Rank = GetRank(value);
                }
            }
            #endregion

            if (model.MarketingType == MarketingTypeView.Camulative)
            {
                // посчитать сколько заполненно уровней
                foreach (var item in items)
                {
                    item.FilledLevels = items.Count(x =>
                        x.hash.Contains(item.hash) &&
                        x.id_object != item.id_object &&
                        x.hash.Length <= item.hash.Length + 5 * 5);
                }

            }
            
            // return view
            if (model.ViewType == StructureViewType.Tree)
                return View("StructTree", model);
            else
                return View("StructList", model);
        }

        #endregion

        [HttpGet]
        public ActionResult Invest(string program = "base")
        {
            ViewBag.IsAllowCreateNewPlace = base.IsAllowCreateNewPlace();
            ViewBag.Program = program;

            return View();
        }

        [HttpPost]
        public ActionResult Invest(InvestView model, string program)
        {
            ViewBag.IsAllowCreateNewPlace = base.IsAllowCreateNewPlace();
            ViewBag.Program = program;

            if (ModelState.IsValid)
            {
                try
                {
                    using (var marketing = new Marketing.Service1Client())
                    {
                        if (!ViewBag.IsAllowCreateNewPlace)
                            model.IsCreateNewPlace = false;

                        if (model.Sum == 300)
                        {
                            marketing.BuyCamulative(
                                companyId: 5,
                                marketingId: 93,
                                partnerId: this.User.id_object,
                                sum: model.Sum,
                                date: DateTime.Now,
                                user: ConfigurationManager.AppSettings["login"]);
                        }
                        else
                        {
                            marketing.BuyInvestment(
                                companyId: 5,
                                camulativeMarketingId: 93,
                                partnerId: this.User.id_object,
                                sum: model.Sum,
                                isProlonged: model.IsProlonged,
                                isCreateNewPlace: model.IsCreateNewPlace,
                                date: DateTime.Now,
                                user: ConfigurationManager.AppSettings["login"]);
                        }
                        this.AuthenticationService.UpdateCookies(this.User.id_object);
                        return RedirectToAction("investments", "home", new { area = "lk" });
                    }
                }
                catch (Exception exc)
                {
                    ModelState.AddModelError(string.Empty, exc.Message);
                }
            }
            return View(model);
        }

        public ActionResult Investments(InvestmentsView model)
        {
            var investments = this.DataService.GetInvestments(this.User.id_object, model.Status);
            var terminated = this.DataService.GetTerminatedInvestments(this.User.id_object);

            ViewBag.Investments = investments;
            ViewBag.Terminated = terminated;
            ViewBag.ExpectedPayments = this.DataService.GetExpectedPayments(this.User.id_object);
            return View(model);
        }

        public ActionResult UnionInvestments(string investments, decimal sum = 0)
        {
            if (!string.IsNullOrWhiteSpace(investments))
            {
                List<uint> items = new List<uint>();
                foreach (var inv in investments.Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries))
                {
                    try
                    {
                        var numb = Convert.ToUInt32(inv);
                        if (numb != 0 && !items.Contains(numb))
                            items.Add(numb);
                    }
                    catch
                    {
                        continue;
                    }
                }

                if (items.Count > 0)
                {
                    try
                    {
                        using (var marketing = new Marketing.Service1Client())
                        {
                            var inv = new Marketing.ArrayOfUnsignedInt();
                            inv.AddRange(items);

                            // Объединение инвест портфелей
                            marketing.UnionInvestments(
                                companyId: 5,
                                partnerId: this.User.id_object,
                                sum: sum,
                                investments: inv,
                                date: DateTime.Now,
                                user: ConfigurationManager.AppSettings["login"]);

                            Session.AddNotification(new Notification(true, Resources.Resource.UnionInvestments, Resources.Resource.InvestmentsAreUnited));
                            AuthenticationService.UpdateCookies(this.User.id_object);
                        }
                    }
                    catch (Exception exc)
                    {
                        // вывод всплывающего окна об ошибке
                        //ModelState.AddModelError(string.Empty, exc.Message);
                        Session.AddNotification(new Notification(false, Resources.Resource.UnionInvestments, exc.Message));
                    }
                }

            }

            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult ConfirmTerminating(uint investmentId)
        {
            try
            {
                var investment = this.DataService.GetInvestment(investmentId);

                using (var marketing = new Marketing.Service1Client())
                {
                    marketing.TerminateInvestment(
                        companyId: 5,
                        partnerId: this.User.id_object,
                        investmentId: investmentId,
                        returnSum: investment.RecalcSum ?? 0,
                        date: DateTime.Now,
                        user: ConfigurationManager.AppSettings["login"]);

                    Session.AddNotification(new Notification(true, Resources.Resource.TerminationAgreement,
                        Resources.Resource.TerminationConfirmation));
                    AuthenticationService.UpdateCookies(this.User.id_object);
                }
            }
            catch (Exception exc)
            {
                Session.AddNotification(new Notification(false, Resources.Resource.TerminationAgreement, exc.Message));
            }

            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult CancelTerminating(uint investmentId)
        {
            try
            {
                this.DataService.SetInvestmentStatus(
                    investmentId: investmentId,
                    status: "Активен",
                    changeDate: DateTime.Now);

                Session.AddNotification(new Notification(true, Resources.Resource.TerminationCancelation, Resources.Resource.TerminationCancelation));
            }
            catch (Exception exc)
            {
                Session.AddNotification(new Notification(false, Resources.Resource.TerminationCancelation, exc.Message));
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult SetProlonged(uint id, bool isProlonged)
        {
            var investment = this.DataService.GetInvestment(id);
            if (investment.PartnerId == this.User.id_object)
            {
                this.DataService.SetProlonged(id, isProlonged);
            }

            return Redirect(Request.UrlReferrer.ToString());
        }


        [HttpGet]
        public ActionResult Review() => View(DataService.GetPartner(this.User.id_object));

        [HttpPost]
        public ActionResult Review(string review)
        {
            // проверка на наличие в строке юникода, смайликов и тп
            review = Regex.Replace(review, @"\p{Cs}", "");
            if ( String.IsNullOrEmpty(review) )
            {
                Session.AddNotification(new Notification(true, Resources.Resource.ReviewIsEmpty,
                        Resources.Resource.ReviewIsEmpty));
                return RedirectToAction("review");
            }

            DataService.AddReview(User.id_object, review);
            AuthenticationService.UpdateCookies(User.id_object);

            Session.AddNotification(new Notification(true, Resources.Resource.ReviewAddedTitle,
                        Resources.Resource.ReviewAdded));
            return RedirectToAction("review");
        }

    }
}