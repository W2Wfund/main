using KBTech.Integration.Merchant.CoinBase;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WayWealth.Areas.lk.Models.CoinBase
{
    public class CoinBaseService
    {
        string _ApiKey;
        public CoinBaseService(string apikey)
        {
            this._ApiKey = apikey;
        }

        public Response<MetaDataReplenish> CryptoInvoice(
            decimal amount,
            string currency,
            uint partner,
            uint order,
            string desc)
        {
            var client = new RestClient("https://api.commerce.coinbase.com");

            var request = new RestRequest("/charges/", Method.POST);
            request.RequestFormat = DataFormat.Json;

            // easily add HTTP Headers
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("X-CC-Api-Key", this._ApiKey);
            request.AddHeader("X-CC-Version", "2018-03-22");


            
            ////var currency = "USD";
            //if (account > 0)
            //{
            //    desc = "Пополнение счета ETH (" + this.User.id_object + ")";
            //    //currency = "ETH";
            //}


            //if (order > 0)
            //{
            //    desc = "";
            //}

            request.AddBody(
                new
                {
                    name = desc,
                    description = desc,
                    //pricing_type = "no_price"
                    pricing_type = "fixed_price",
                    local_price = new Balance
                    {
                        amount = amount,
                        currency = currency
                        //currency = "USD"
                    },
                    metadata = new MetaDataReplenish
                    {
                        partner = partner,
                        order = order,
                        //account = account,
                    }
                });

            // execute the request
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string

            // {"data":{"addresses":{"ethereum":"0x05c7ac9fe25d0b67327a111593a32b4e2a5a8db8"},"code":"NTZ5HJEP","created_at":"2018-05-14T07:38:33Z","description":"Пополнение счета USD","expires_at":"2018-05-14T07:53:33Z","hosted_url":"https://commerce.coinbase.com/charges/NTZ5HJEP","metadata":{"partner":"71","order":"0","account":"0"},"name":"Пополнение счета USD","payments":[],"pricing":{"local":{"amount":"120.05","currency":"USD"},"ethereum":{"amount":"0.172100000","currency":"ETH"}},"pricing_type":"fixed_price","timeline":[{"status":"NEW","time":"2018-05-14T07:38:33Z"}]}}
            // {"data":{"addresses":{"ethereum":"0xfbbe2db2d1c060efc93ed777feed8e0796625ff2"},"code":"PZ66466H","created_at":"2018-05-14T07:47:55Z","description":"Пополнение счета USD","expires_at":"2018-05-14T08:02:55Z","hosted_url":"https://commerce.coinbase.com/charges/PZ66466H","metadata":{"partner":"71","order":"0","account":"0"},"name":"Пополнение счета USD","payments":[],"pricing":{"local":{"amount":"120.05","currency":"USD"},"ethereum":{"amount":"0.171993000","currency":"ETH"}},"pricing_type":"fixed_price","timeline":[{"status":"NEW","time":"2018-05-14T07:47:55Z"}]}}
            // {"data":{"addresses":{"ethereum":"0x279d3fb4ee36164c70a577984024db32eed51132"},"code":"LWYDJW2J","created_at":"2018-05-14T08:01:57Z","description":"Пополнение счета USD","expires_at":"2018-05-14T08:16:57Z","hosted_url":"https://commerce.coinbase.com/charges/LWYDJW2J","metadata":{"partner":"71","order":"0","account":"0"},"name":"Пополнение счета USD","payments":[],"pricing":{"local":{"amount":"15.10","currency":"USD"},"ethereum":{"amount":"0.021497000","currency":"ETH"}},"pricing_type":"fixed_price","timeline":[{"status":"NEW","time":"2018-05-14T08:01:57Z"}]}}

            if (!string.IsNullOrWhiteSpace(content))
            {
                return JsonConvert.DeserializeObject<Response<MetaDataReplenish>>(content);
            }
            return null;
        }

        //public RateResponse GetRate(string currency)
        //{
        //    var client = new RestClient("https://api.coinbase.com");
        //    var request = new RestRequest("/v2/exchange-rates?currency=" + currency, Method.GET);
        //    request.RequestFormat = DataFormat.Json;

        //    // easily add HTTP Headers
        //    request.AddHeader("Content-Type", "application/json");

        //    // execute the request
        //    IRestResponse response = client.Execute(request);
        //    var content = response.Content; // raw content as string

        //    if (!string.IsNullOrWhiteSpace(content))
        //    {
        //        return JsonConvert.DeserializeObject<RateResponse>(content);
        //    }
        //    return null;
        //}

        public ApiResponse<Price> GetPrice(string pair, string operation)
        {
            var client = new RestClient("https://api.coinbase.com");
            var request = new RestRequest("/v2/prices/" + pair + "/" + operation + "/", Method.GET);
            request.RequestFormat = DataFormat.Json;

            // easily add HTTP Headers
            request.AddHeader("Content-Type", "application/json");

            // execute the request
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string

            if (!string.IsNullOrWhiteSpace(content))
            {
                return JsonConvert.DeserializeObject<ApiResponse<Price>>(content);
            }
            return null;
        }

        public ApiResponse<Price> GetSpotPrice(string pair) => this.GetPrice(pair, "spot");

        //public RateResponse GetCheckout(string id)
        //{
        //    var client = new RestClient("https://api.coinbase.com");
        //    var request = new RestRequest("/checkouts/" + id, Method.GET);
        //    request.RequestFormat = DataFormat.Json;

        //    // easily add HTTP Headers
        //    request.AddHeader("Content-Type", "application/json");

        //    // execute the request
        //    IRestResponse response = client.Execute(request);
        //    var content = response.Content; // raw content as string

        //    if (!string.IsNullOrWhiteSpace(content))
        //    {
        //        return JsonConvert.DeserializeObject<RateResponse>(content);
        //    }
        //    return null;
        //}
    }
}