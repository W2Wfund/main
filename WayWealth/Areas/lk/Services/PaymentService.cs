using Common.Web.Mail;
using KBTech.Integration.Merchant.CoinBase;
using KBTech.Integration.Merchant.Ripple;
using KBTech.Integration.Merchant.Ripple.Enums;
using KBTech.Integration.Merchant.Ripple.Request;
using KBTech.Integration.Merchant.Ripple.Response;
using KBTech.Lib.Client.Payment;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using W2W.ModelKBT;
using ripple = KBTech.Integration.Merchant.Ripple;

namespace WayWealth.Areas.lk.Services
{
    public class PaymentService
    {
        IDataService DataService;
        IMailService MailService;
        ILogger Logger;
        public PaymentService(IDataService ds, IMailService ms, ILogger logger)
        {
            DataService = ds;
            MailService = ms;
            Logger = logger;
        }
        public byte[] hmacSHA256(string data, string key)
        {
            using (HMACSHA256 hmac = new HMACSHA256(Encoding.ASCII.GetBytes(key)))
            {
                //return hmac.ComputeHash(Encoding.ASCII.GetBytes(data));
                return hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
            }
        }



        public void ParseCoinBaseResponse(string json, string signature, string secret)
        {
            if (!string.IsNullOrWhiteSpace(json))
            {
                var hash = BitConverter.ToString(hmacSHA256(json, secret)).Replace("-", "").ToLower();
                this.Logger.Debug(string.Format("{0} {1} {2}", signature, hash, json));

                if (signature == hash)
                {
                    var obj = JsonConvert.DeserializeObject<WebHook<MetaDataReplenish>>(json);

                    // Если есть платежи
                    if (obj.Event.Data.Payments.Length > 0)
                    {
                        string code = obj.Event.Data.Code;
                        var metadata = obj.Event.Data.Metadata;
                        foreach (var payment in obj.Event.Data.Payments)
                        {
                            if (payment.status == "CONFIRMED")
                                Pay(currencySum: payment.value[Moneys.crypto].amount, id_order: metadata.order);
                        }
                    }
                    #region Не актуально
                    //if (obj.Event.type == "charge:created")
                    //{
                    //    //{"attempt_number":6,"event":{"api_version":"2018-03-22","created_at":"2018-05-11T11:31:38Z","data":{"code":"Z84X3Y8R","name":"OBG","metadata":{},"payments":[],"timeline":[{"time":"2018-05-11T11:31:38Z","status":"NEW"}],"addresses":{"ethereum":"0xe4c3ff33bc2b05697711a9b4b82a862549d6b859"},"created_at":"2018-05-11T11:31:38Z","expires_at":"2018-05-11T11:46:38Z","hosted_url":"https://commerce.coinbase.com/charges/Z84X3Y8R","description":"Investment 1","pricing_type":"no_price"},"id":"0731d45b-928d-4ecc-ac3f-77ead49ea1d2","type":"charge:created"},"id":"add3d27f-8dab-4119-839a-2d82ef11de1d","scheduled_for":"2018-05-11T11:41:29Z"}
                    //    //{"attempt_number":9,"event":{"api_version":"2018-03-22","created_at":"2018-05-11T11:27:19Z","data":{"code":"9GMXVJCY","name":"OBG","metadata":{},"payments":[],"timeline":[{"time":"2018-05-11T11:27:19Z","status":"NEW"}],"addresses":{"ethereum":"0xc8a06669cb27b9172e655312e9622f640e13a9f4"},"created_at":"2018-05-11T11:27:19Z","expires_at":"2018-05-11T11:42:19Z","hosted_url":"https://commerce.coinbase.com/charges/9GMXVJCY","description":"Investment 1","pricing_type":"no_price"},"id":"82e30109-1744-4f4d-9564-e340324fe9bb","type":"charge:created"},"id":"32615c4d-a9fc-4ce1-b607-e6ce11f87f60","scheduled_for":"2018-05-11T12:33:35Z"}
                    //    //{"attempt_number":9,"event":{"api_version":"2018-03-22","created_at":"2018-05-11T11:31:38Z","data":{"code":"Z84X3Y8R","name":"OBG","metadata":{},"payments":[],"timeline":[{"time":"2018-05-11T11:31:38Z","status":"NEW"}],"addresses":{"ethereum":"0xe4c3ff33bc2b05697711a9b4b82a862549d6b859"},"created_at":"2018-05-11T11:31:38Z","expires_at":"2018-05-11T11:46:38Z","hosted_url":"https://commerce.coinbase.com/charges/Z84X3Y8R","description":"Investment 1","pricing_type":"no_price"},"id":"0731d45b-928d-4ecc-ac3f-77ead49ea1d2","type":"charge:created"},"id":"d13a653b-e6be-4db6-9b5e-763ac6470acc","scheduled_for":"2018-05-11T12:38:48Z"}



                    //}
                    //else if (obj.Event.type == "charge:confirmed")
                    //{
                    //    //{"attempt_number":1,"event":{"api_version":"2018-03-22","created_at":"2018-05-11T11:41:49Z","data":{"code":"NCVRVT2H","name":"OBG","metadata":{},"payments":[{"block":{"hash":"0x639e50a75e39d28f5877b34432909263f979bc74103bd550d4b7160ad46a30e1","height":5594754,"confirmations":7,"confirmations_required":8},"value":{"local":{"amount":"27.29","currency":"USD"},"crypto":{"amount":"0.039689200","currency":"ETH"}},"status":"CONFIRMED","network":"ethereum","transaction_id":"0x06a9685f79e6c55d857f734c0185c0598cbedc0c1b668298e576e2477acec1cd"}],"timeline":[{"time":"2018-05-11T11:34:05Z","status":"NEW"},{"time":"2018-05-11T11:39:48Z","status":"PENDING","payment":{"network":"ethereum","transaction_id":"0x06a9685f79e6c55d857f734c0185c0598cbedc0c1b668298e576e2477acec1cd"}},{"time":"2018-05-11T11:41:49Z","status":"COMPLETED","payment":{"network":"ethereum","transaction_id":"0x06a9685f79e6c55d857f734c0185c0598cbedc0c1b668298e576e2477acec1cd"}}],"addresses":{"ethereum":"0x4b97984e576d3e3872fc50deb020529e343949ca"},"created_at":"2018-05-11T11:34:05Z","expires_at":"2018-05-11T11:49:05Z","hosted_url":"https://commerce.coinbase.com/charges/NCVRVT2H","description":"Investment 1","confirmed_at":"2018-05-11T11:41:49Z","pricing_type":"no_price"},"id":"405a34a6-2cc3-4006-9339-8d93c5e79e34","type":"charge:confirmed"},"id":"eac66e13-1e46-41c1-8e80-6061a9a227f2","scheduled_for":"2018-05-11T11:41:49Z"}
                    //    //{"attempt_number":2,"event":{"api_version":"2018-03-22","created_at":"2018-05-11T11:41:49Z","data":{"code":"NCVRVT2H","name":"OBG","metadata":{},"payments":[{"block":{"hash":"0x639e50a75e39d28f5877b34432909263f979bc74103bd550d4b7160ad46a30e1","height":5594754,"confirmations":7,"confirmations_required":8},"value":{"local":{"amount":"27.29","currency":"USD"},"crypto":{"amount":"0.039689200","currency":"ETH"}},"status":"CONFIRMED","network":"ethereum","transaction_id":"0x06a9685f79e6c55d857f734c0185c0598cbedc0c1b668298e576e2477acec1cd"}],"timeline":[{"time":"2018-05-11T11:34:05Z","status":"NEW"},{"time":"2018-05-11T11:39:48Z","status":"PENDING","payment":{"network":"ethereum","transaction_id":"0x06a9685f79e6c55d857f734c0185c0598cbedc0c1b668298e576e2477acec1cd"}},{"time":"2018-05-11T11:41:49Z","status":"COMPLETED","payment":{"network":"ethereum","transaction_id":"0x06a9685f79e6c55d857f734c0185c0598cbedc0c1b668298e576e2477acec1cd"}}],"addresses":{"ethereum":"0x4b97984e576d3e3872fc50deb020529e343949ca"},"created_at":"2018-05-11T11:34:05Z","expires_at":"2018-05-11T11:49:05Z","hosted_url":"https://commerce.coinbase.com/charges/NCVRVT2H","description":"Investment 1","confirmed_at":"2018-05-11T11:41:49Z","pricing_type":"no_price"},"id":"405a34a6-2cc3-4006-9339-8d93c5e79e34","type":"charge:confirmed"},"id":"16492f20-eec9-475a-9360-2abd8ecab19f","scheduled_for":"2018-05-11T11:42:14Z"}
                    //    //{"attempt_number":3,"event":{"api_version":"2018-03-22","created_at":"2018-05-11T11:41:49Z","data":{"code":"NCVRVT2H","name":"OBG","metadata":{},"payments":[{"block":{"hash":"0x639e50a75e39d28f5877b34432909263f979bc74103bd550d4b7160ad46a30e1","height":5594754,"confirmations":7,"confirmations_required":8},"value":{"local":{"amount":"27.29","currency":"USD"},"crypto":{"amount":"0.039689200","currency":"ETH"}},"status":"CONFIRMED","network":"ethereum","transaction_id":"0x06a9685f79e6c55d857f734c0185c0598cbedc0c1b668298e576e2477acec1cd"}],"timeline":[{"time":"2018-05-11T11:34:05Z","status":"NEW"},{"time":"2018-05-11T11:39:48Z","status":"PENDING","payment":{"network":"ethereum","transaction_id":"0x06a9685f79e6c55d857f734c0185c0598cbedc0c1b668298e576e2477acec1cd"}},{"time":"2018-05-11T11:41:49Z","status":"COMPLETED","payment":{"network":"ethereum","transaction_id":"0x06a9685f79e6c55d857f734c0185c0598cbedc0c1b668298e576e2477acec1cd"}}],"addresses":{"ethereum":"0x4b97984e576d3e3872fc50deb020529e343949ca"},"created_at":"2018-05-11T11:34:05Z","expires_at":"2018-05-11T11:49:05Z","hosted_url":"https://commerce.coinbase.com/charges/NCVRVT2H","description":"Investment 1","confirmed_at":"2018-05-11T11:41:49Z","pricing_type":"no_price"},"id":"405a34a6-2cc3-4006-9339-8d93c5e79e34","type":"charge:confirmed"},"id":"a0ac2c14-3574-4188-8b8c-e37b62ea06e1","scheduled_for":"2018-05-11T11:43:09Z"}
                    //    //{"attempt_number":9,"event":{"api_version":"2018-03-22","created_at":"2018-05-11T11:41:49Z","data":{"code":"NCVRVT2H","name":"OBG","metadata":{},"payments":[{"block":{"hash":"0x639e50a75e39d28f5877b34432909263f979bc74103bd550d4b7160ad46a30e1","height":5594754,"confirmations":7,"confirmations_required":8},"value":{"local":{"amount":"27.29","currency":"USD"},"crypto":{"amount":"0.039689200","currency":"ETH"}},"status":"CONFIRMED","network":"ethereum","transaction_id":"0x06a9685f79e6c55d857f734c0185c0598cbedc0c1b668298e576e2477acec1cd"}],"timeline":[{"time":"2018-05-11T11:34:05Z","status":"NEW"},{"time":"2018-05-11T11:39:48Z","status":"PENDING","payment":{"network":"ethereum","transaction_id":"0x06a9685f79e6c55d857f734c0185c0598cbedc0c1b668298e576e2477acec1cd"}},{"time":"2018-05-11T11:41:49Z","status":"COMPLETED","payment":{"network":"ethereum","transaction_id":"0x06a9685f79e6c55d857f734c0185c0598cbedc0c1b668298e576e2477acec1cd"}}],"addresses":{"ethereum":"0x4b97984e576d3e3872fc50deb020529e343949ca"},"created_at":"2018-05-11T11:34:05Z","expires_at":"2018-05-11T11:49:05Z","hosted_url":"https://commerce.coinbase.com/charges/NCVRVT2H","description":"Investment 1","confirmed_at":"2018-05-11T11:41:49Z","pricing_type":"no_price"},"id":"405a34a6-2cc3-4006-9339-8d93c5e79e34","type":"charge:confirmed"},"id":"42700127-1d2f-4f92-973f-0a4f9d62e5ec","scheduled_for":"2018-05-11T12:48:22Z"}
                    //    //
                    //    // Unexpected character ecountered while parsing value {. Path 'event.data.payments[0].value.local',line 1, position 324

                    //    //{"attempt_number":10,"event":{"api_version":"2018-03-22","created_at":"2018-05-11T11:41:49Z","data":{"code":"NCVRVT2H","name":"OBG","metadata":{},"payments":[{"block":{"hash":"0x639e50a75e39d28f5877b34432909263f979bc74103bd550d4b7160ad46a30e1","height":5594754,"confirmations":7,"confirmations_required":8},"value":{"local":{"amount":"27.29","currency":"USD"},"crypto":{"amount":"0.039689200","currency":"ETH"}},"status":"CONFIRMED","network":"ethereum","transaction_id":"0x06a9685f79e6c55d857f734c0185c0598cbedc0c1b668298e576e2477acec1cd"}],"timeline":[{"time":"2018-05-11T11:34:05Z","status":"NEW"},{"time":"2018-05-11T11:39:48Z","status":"PENDING","payment":{"network":"ethereum","transaction_id":"0x06a9685f79e6c55d857f734c0185c0598cbedc0c1b668298e576e2477acec1cd"}},{"time":"2018-05-11T11:41:49Z","status":"COMPLETED","payment":{"network":"ethereum","transaction_id":"0x06a9685f79e6c55d857f734c0185c0598cbedc0c1b668298e576e2477acec1cd"}}],"addresses":{"ethereum":"0x4b97984e576d3e3872fc50deb020529e343949ca"},"created_at":"2018-05-11T11:34:05Z","expires_at":"2018-05-11T11:49:05Z","hosted_url":"https://commerce.coinbase.com/charges/NCVRVT2H","description":"Investment 1","confirmed_at":"2018-05-11T11:41:49Z","pricing_type":"no_price"},"id":"405a34a6-2cc3-4006-9339-8d93c5e79e34","type":"charge:confirmed"},"id":"fc261852-89f2-4450-ab4b-72913bf7bf62","scheduled_for":"2018-05-11T13:48:52Z"}
                    //    // could not convert string 'crypto' to dictionary key type OBG.Cabinet.Models.CoinBase.Moneys' Path 'event.data.payments[0].value.crypto' line 1 position 369

                    //    /*
                    //     * Алгоритм:
                    //     * 
                    //     * Ищем платеж с идентификатором,
                    //     * если его нет, то получаем информацию из платежа:
                    //     * 
                    //     * - счет пополнения
                    //     * - ссылка на контрагента
                    //     * - ссылка на заказ
                    //     */



                    //}
                    //else if (obj.Event.type == "charge:failed")
                    //{
                    //    //{"attempt_number":8,"event":{"api_version":"2018-03-22","created_at":"2018-05-11T11:01:36Z","data":{"code":"RRNF67WB","name":"OBG","metadata":{},"payments":[],"timeline":[{"time":"2018-05-11T11:01:36Z","status":"NEW"}],"addresses":{"ethereum":"0x4bc604e4d8d938dd9c0046b8c870c4c0129de7e0"},"created_at":"2018-05-11T11:01:36Z","expires_at":"2018-05-11T11:16:36Z","hosted_url":"https://commerce.coinbase.com/charges/RRNF67WB","description":"Investment 1","pricing_type":"no_price"},"id":"f190bbd2-65ac-4a83-8dd7-0f1f38195ea4","type":"charge:created"},"id":"4da3a370-8f2a-4e28-a719-7c5f59e3fb9e","scheduled_for":"2018-05-11T11:35:18Z"}
                    //    //{"attempt_number":8,"event":{"api_version":"2018-03-22","created_at":"2018-05-11T11:05:09Z","data":{"code":"WTJX2PYQ","name":"OBG","metadata":{},"payments":[],"timeline":[{"time":"2018-05-11T10:50:01Z","status":"NEW"},{"time":"2018-05-11T11:05:09Z","status":"EXPIRED"}],"addresses":{"ethereum":"0x680e3372c4065d52d2dc42d62fb1a68cf9ce1a6a"},"created_at":"2018-05-11T10:50:01Z","expires_at":"2018-05-11T11:05:01Z","hosted_url":"https://commerce.coinbase.com/charges/WTJX2PYQ","description":"Investment 1","pricing_type":"no_price"},"id":"83e6e107-81c8-4c0e-b65a-49289556061f","type":"charge:failed"},"id":"7af84a77-daa0-47d9-b997-2cd7e64288e7","scheduled_for":"2018-05-11T11:38:34Z"}
                    //    //{"attempt_number":1,"event":{"api_version":"2018-03-22","created_at":"2018-05-11T11:39:09Z","data":{"code":"EHLBAMFB","name":"OBG","metadata":{},"payments":[],"timeline":[{"time":"2018-05-11T11:24:01Z","status":"NEW"},{"time":"2018-05-11T11:39:09Z","status":"EXPIRED"}],"addresses":{"ethereum":"0x660efe967735b2816d0e2070517b69c5ec719caf"},"created_at":"2018-05-11T11:24:01Z","expires_at":"2018-05-11T11:39:01Z","hosted_url":"https://commerce.coinbase.com/charges/EHLBAMFB","description":"Investment 1","pricing_type":"no_price"},"id":"95baafd7-9c12-4bf3-bc59-665466833a27","type":"charge:failed"},"id":"a8aaec41-35de-4ca7-b763-862bcb2add76","scheduled_for":"2018-05-11T11:39:09Z"}
                    //    //{"attempt_number":2,"event":{"api_version":"2018-03-22","created_at":"2018-05-11T11:39:09Z","data":{"code":"EHLBAMFB","name":"OBG","metadata":{},"payments":[],"timeline":[{"time":"2018-05-11T11:24:01Z","status":"NEW"},{"time":"2018-05-11T11:39:09Z","status":"EXPIRED"}],"addresses":{"ethereum":"0x660efe967735b2816d0e2070517b69c5ec719caf"},"created_at":"2018-05-11T11:24:01Z","expires_at":"2018-05-11T11:39:01Z","hosted_url":"https://commerce.coinbase.com/charges/EHLBAMFB","description":"Investment 1","pricing_type":"no_price"},"id":"95baafd7-9c12-4bf3-bc59-665466833a27","type":"charge:failed"},"id":"cfc837eb-330e-4d26-8209-f5eee57a9041","scheduled_for":"2018-05-11T11:39:34Z"}
                    //    //{"attempt_number":3,"event":{"api_version":"2018-03-22","created_at":"2018-05-11T11:39:09Z","data":{"code":"EHLBAMFB","name":"OBG","metadata":{},"payments":[],"timeline":[{"time":"2018-05-11T11:24:01Z","status":"NEW"},{"time":"2018-05-11T11:39:09Z","status":"EXPIRED"}],"addresses":{"ethereum":"0x660efe967735b2816d0e2070517b69c5ec719caf"},"created_at":"2018-05-11T11:24:01Z","expires_at":"2018-05-11T11:39:01Z","hosted_url":"https://commerce.coinbase.com/charges/EHLBAMFB","description":"Investment 1","pricing_type":"no_price"},"id":"95baafd7-9c12-4bf3-bc59-665466833a27","type":"charge:failed"},"id":"9cf424f7-47d0-4c6b-9a97-acbcdcf532a3","scheduled_for":"2018-05-11T11:40:32Z"}
                    //    //{"attempt_number":1,"event":{"api_version":"2018-03-22","created_at":"2018-05-11T11:42:27Z","data":{"code":"9GMXVJCY","name":"OBG","metadata":{},"payments":[],"timeline":[{"time":"2018-05-11T11:27:19Z","status":"NEW"},{"time":"2018-05-11T11:42:27Z","status":"EXPIRED"}],"addresses":{"ethereum":"0xc8a06669cb27b9172e655312e9622f640e13a9f4"},"created_at":"2018-05-11T11:27:19Z","expires_at":"2018-05-11T11:42:19Z","hosted_url":"https://commerce.coinbase.com/charges/9GMXVJCY","description":"Investment 1","pricing_type":"no_price"},"id":"40a812d3-3e2c-4945-ba9f-8da4b40220bb","type":"charge:failed"},"id":"b92ea934-62b7-4ebf-aca3-7226b1213ae9","scheduled_for":"2018-05-11T11:42:27Z"}
                    //    //{"attempt_number":8,"event":{"api_version":"2018-03-22","created_at":"2018-05-11T11:09:39Z","data":{"code":"VQLC8J8T","name":"OBG","metadata":{},"payments":[],"timeline":[{"time":"2018-05-11T10:54:32Z","status":"NEW"},{"time":"2018-05-11T11:09:39Z","status":"EXPIRED"}],"addresses":{"ethereum":"0x1124047122ebd7652edf7d12e3e841543d5ae9a9"},"created_at":"2018-05-11T10:54:32Z","expires_at":"2018-05-11T11:09:32Z","hosted_url":"https://commerce.coinbase.com/charges/VQLC8J8T","description":"Investment 1","pricing_type":"no_price"},"id":"e7af3d41-7656-4951-b223-ec24493abda5","type":"charge:failed"},"id":"45387a72-8494-4a76-b935-b7d7c87fdb7f","scheduled_for":"2018-05-11T11:43:37Z"}
                    //    //{"attempt_number":8,"event":{"api_version":"2018-03-22","created_at":"2018-05-11T11:08:51Z","data":{"code":"BDNRTX6W","name":"OBG","metadata":{},"payments":[],"timeline":[{"time":"2018-05-11T10:53:43Z","status":"NEW"},{"time":"2018-05-11T11:08:51Z","status":"EXPIRED"}],"addresses":{"ethereum":"0x4351de0a977f588261c54b79176c2455fe9c7076"},"created_at":"2018-05-11T10:53:43Z","expires_at":"2018-05-11T11:08:43Z","hosted_url":"https://commerce.coinbase.com/charges/BDNRTX6W","description":"Investment 1","pricing_type":"no_price"},"id":"483975ce-96aa-4789-b14a-d4c11c7818ae","type":"charge:failed"},"id":"3c1ea040-42c2-4c10-b784-82e347902b1e","scheduled_for":"2018-05-11T11:42:29Z"}
                    //    //{"attempt_number":9,"event":{"api_version":"2018-03-22","created_at":"2018-05-11T11:39:09Z","data":{"code":"EHLBAMFB","name":"OBG","metadata":{},"payments":[],"timeline":[{"time":"2018-05-11T11:24:01Z","status":"NEW"},{"time":"2018-05-11T11:39:09Z","status":"EXPIRED"}],"addresses":{"ethereum":"0x660efe967735b2816d0e2070517b69c5ec719caf"},"created_at":"2018-05-11T11:24:01Z","expires_at":"2018-05-11T11:39:01Z","hosted_url":"https://commerce.coinbase.com/charges/EHLBAMFB","description":"Investment 1","pricing_type":"no_price"},"id":"95baafd7-9c12-4bf3-bc59-665466833a27","type":"charge:failed"},"id":"6b0f08b0-6339-4d30-b9f5-fc702ed4e083","scheduled_for":"2018-05-11T12:45:15Z"}

                    //    //{"attempt_number":1,"event":{"api_version":"2018-03-22","created_at":"2018-05-14T13:08:05Z","data":{"code":"BTDX93ZL","name":"Пополнение счета ETH (277)","pricing":{"local":{"amount":"25.00","currency":"USD"},"ethereum":{"amount":"0.035512000","currency":"ETH"}},"metadata":{"order":"0","account":"1","partner":"277"},"payments":[{"block":{"hash":"0x8ff4d31f060ad55719cca576db9c23da71ff295951eb19ef4ae5460e7374036c","height":5612334,"confirmations":7,"confirmations_required":8},"value":{"local":{"amount":"30.70","currency":"USD"},"crypto":{"amount":"0.043610070","currency":"ETH"}},"status":"CONFIRMED","network":"ethereum","transaction_id":"0x915679e511494965fd0f196485be2e4693fd8439af1dafd0785e0799a9c8aaa4"}],"timeline":[{"time":"2018-05-14T13:03:56Z","status":"NEW"},{"time":"2018-05-14T13:06:15Z","status":"PENDING","payment":{"network":"ethereum","transaction_id":"0x915679e511494965fd0f196485be2e4693fd8439af1dafd0785e0799a9c8aaa4"}},{"time":"2018-05-14T13:08:04Z","status":"UNRESOLVED","context":"OVERPAID","payment":{"network":"ethereum","transaction_id":"0x915679e511494965fd0f196485be2e4693fd8439af1dafd0785e0799a9c8aaa4"}}],"addresses":{"ethereum":"0x0b0304f158bef52ebced7040c7ddca330996e210"},"created_at":"2018-05-14T13:03:56Z","expires_at":"2018-05-14T13:18:56Z","hosted_url":"https://commerce.coinbase.com/charges/BTDX93ZL","description":"Пополнение счета ETH (277)","pricing_type":"fixed_price"},"id":"ed360c40-d107-4d57-a6ed-dc270f6897ff","type":"charge:failed"},"id":"922eaaad-ce46-487f-8de1-2f648efc2fda","scheduled_for":"2018-05-14T13:08:05Z"}
                    //}
                    //else
                    //{

                    //}
                    #endregion
                }
            }
        }
        public void CheckRipple(string wallet)
        {
            RippleService service = new RippleService(wallet);
            var result = service.Execute<AccountPaymentsResponse>(new AccountPaymentRequest
            {
                Type = AccountPaymentType.sent
            });
            
            foreach (var payment in result.Payments)
            {
                if (payment.Destination_tag > 0)
                    Pay(payment.Delivered_amount, Convert.ToUInt32(payment.Destination_tag));
            }
        }

        void Pay(decimal currencySum, uint id_order)
        {
            var request = this.DataService.GetWebPaymentRequest(id_order);
            if (request != null && request.PaymentStatus == "Не оплачен")
            {
                var sum = request.PaymentSum.Value;
                if (request.CurrencySum.Value != currencySum)
                {
                    sum = Math.Round(currencySum * request.PaymentSum.Value / request.CurrencySum.Value, 2);
                    this.DataService.UpdateWebPaymentRequest(id_order, sum, currencySum);
                }
                this.DataService.PayPayment(id_order, DateTime.Now);

                var id_payment = this.DataService.AddPayment(
                  TransferDirection.Input,
                  5,
                  request.PartnerId.Value,
                  id_order,
                  "Пополнение счета",
                  DateTime.Now,
                  sum,
                  PaymentMethod.Crypto,
                  RateOfNDS.БезНДС,
                  "Пополнение счета",
                  0,
                  request.Editor);

                #region sending message
                StringBuilder message = new StringBuilder();
                var culture = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
                if (culture == "ru-RU")
                {
                    message.Append("<h3>Здравствуйте!</h3>");
                    message.Append($"<p>Поступил платеж в размере ${sum:N2} ({currencySum:N6} {request.Currency}).</p>");
                }
                else
                {
                    message.Append("<h3>Hello!</h3>");
                    message.Append($"<p>Payment has been received in the amount of ${sum:N2} ({currencySum:N6} {request.Currency}).</p>");
                }
                var partner = this.DataService.GetPartner(request.PartnerId.Value);
                this.MailService.SendMessage(partner.Email, Resources.Resource.MoneyIncome, message.ToString());
                #endregion
            }
        }
    }
}