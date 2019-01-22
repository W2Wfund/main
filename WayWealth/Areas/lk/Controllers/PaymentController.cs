using KBTech.Integration.Merchant.CoinBase;
using KBTech.Integration.Merchant.Ripple;
using KBTech.Integration.Merchant.Ripple.Enums;
using KBTech.Integration.Merchant.Ripple.Request;
using KBTech.Integration.Merchant.Ripple.Response;
using KBTech.Lib.Client.Payment;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WayWealth.Areas.lk.Services;
using WayWealth.Controllers;


namespace WayWealth.Areas.lk.Controllers
{
    public class PaymentController : BaseController
    {
        
        public HttpStatusCodeResult CoinBaseStatus()
        {
            Stream req = Request.InputStream;
            req.Seek(0, System.IO.SeekOrigin.Begin);
            string json = new StreamReader(req).ReadToEnd();
            //{"attempt_number":5,"event":{"api_version":"2018-03-22","created_at":"2018-05-14T11:00:08Z","data":{"code":"HAK47JWH","name":"Пополнение счета ETH (277)","pricing":{"local":{"amount":"0.043500000","currency":"ETH"},"ethereum":{"amount":"0.043500000","currency":"ETH"}},"metadata":{"order":"0","account":"1","partner":"277"},"payments":[],"timeline":[{"time":"2018-05-14T11:00:08Z","status":"NEW"}],"addresses":{"ethereum":"0xe3709a95c57dcab1cb18f739f244324b31401fe3"},"created_at":"2018-05-14T11:00:08Z","expires_at":"2018-05-14T11:15:08Z","hosted_url":"https://commerce.coinbase.com/charges/HAK47JWH","description":"Пополнение счета ETH (277)","pricing_type":"fixed_price"},"id":"34b3fc13-b383-4c12-be7e-f0ba28d05d90","type":"charge:created"},"id":"c54defb2-5499-4700-a0fa-eb4948ab8de8","scheduled_for":"2018-05-14T11:05:13Z"}
            // 042a138df81a88fd014c8981da22651e7f9ee8bbb709489f2273c44da7785a13
            // 7698c35688bf4670733fc37f870dc85c260bf368609e1f37bf31d9baa508473a

            PaymentService ps = new PaymentService(this.DataService, this.MailService, this.Logger);
            ps.ParseCoinBaseResponse(
                json: json,
                signature: Request.Headers["X-CC-Webhook-Signature"],
                secret: ConfigurationManager.AppSettings["coinbase.sharedsecret"]);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public HttpStatusCodeResult Test()
        {
            //var json = "{\"attempt_number\":39,\"event\":{\"api_version\":\"2018-03-22\",\"created_at\":\"2018-12-05T14:17:06Z\",\"data\":{\"id\":\"09646dc9-0256-43b9-8351-0ef1e83d1d27\",\"code\":\"VQML3DJV\",\"name\":\"Пополнение счета USD (75)\",\"pricing\":{\"local\":{\"amount\":\"2.00\",\"currency\":\"USD\"},\"bitcoin\":{\"amount\":\"0.00052029\",\"currency\":\"BTC\"},\"ethereum\":{\"amount\":\"0.018738000\",\"currency\":\"ETH\"},\"litecoin\":{\"amount\":\"0.06573541\",\"currency\":\"LTC\"},\"bitcoincash\":{\"amount\":\"0.01424857\",\"currency\":\"BCH\"}},\"logo_url\":\"https://res.cloudinary.com/commerce/image/upload/v1542712380/wgyuk9otir4ymbklzaht.png\",\"metadata\":{\"order\":\"0\",\"account\":\"0\",\"partner\":\"75\"},\"payments\":[{\"block\":{\"hash\":null,\"height\":null,\"confirmations\":0,\"confirmations_required\":8},\"value\":{\"local\":{\"amount\":\"1.07\",\"currency\":\"USD\"},\"crypto\":{\"amount\":\"0.010000000\",\"currency\":\"ETH\"}},\"status\":\"PENDING\",\"network\":\"ethereum\",\"detected_at\":\"2018-12-05T14:17:06Z\",\"transaction_id\":\"0xc0f15ae406166d9badf49bcdff686f1d18896ddea69ca7ce42767c330e76164b\"}],\"resource\":\"charge\",\"timeline\":[{\"time\":\"2018-12-05T13:58:13Z\",\"status\":\"NEW\"},{\"time\":\"2018-12-05T14:17:06Z\",\"status\":\"PENDING\",\"payment\":{\"network\":\"ethereum\",\"transaction_id\":\"0xc0f15ae406166d9badf49bcdff686f1d18896ddea69ca7ce42767c330e76164b\"}}],\"addresses\":{\"bitcoin\":\"1BQ2fMoHWZeZk44AvTdBqP1JiwMwDkpaJm\",\"ethereum\":\"0x5eabd0b6dd992d80f2b2c1a15758ddbf63df1c97\",\"litecoin\":\"LMQjd9cUvzXSfwVJ2escfnrTuudnMsC2Zs\",\"bitcoincash\":\"qqwdhrgkckq7pcd2ewrm6jx4gjz4janmvsjqmldsq4\"},\"created_at\":\"2018-12-05T13:58:13Z\",\"expires_at\":\"2018-12-05T14:58:12Z\",\"hosted_url\":\"https://commerce.coinbase.com/charges/VQML3DJV\",\"description\":\"Пополнение счета USD (75)\",\"pricing_type\":\"fixed_price\"},\"id\":\"31d5e351-bb24-428f-8054-b89da3fa2a48\",\"resource\":\"event\",\"type\":\"charge:pending\"},\"id\":\"834168b4-ca36-47d9-ad82-c7d0b5b5cb05\",\"scheduled_for\":\"2018-12-06T21:33:00Z\"}";
            //ParseCoinBaseResponse(json);
         
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

       


    }
}