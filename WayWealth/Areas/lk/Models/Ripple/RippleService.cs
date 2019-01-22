using KBTech.Integration.Merchant.Ripple.Request;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace KBTech.Integration.Merchant.Ripple
{
    public class RippleService
    {
        const string ApiUrl = "https://data.ripple.com";
        string _Address;
        public RippleService(string address) => _Address = address;

        public T Execute<T>(IRequest request) where T : new()
        {
            var path = request.Path.Replace("{address}", _Address) + "?type=received";
            var client = new RestClient(ApiUrl);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            var req = new RestRequest(path, (Method)Enum.Parse(typeof(Method), request.Method));
           req.RequestFormat = DataFormat.Json;

            // execute the request
            IRestResponse response = client.Execute(req);
            var content = response.Content; // raw content as string
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(content);
            return obj;
        }
    }
}

/*
 * 
 *   // easily add HTTP Headers
            req.AddHeader("Content-Type", "application/json");
            req.AddHeader("X-CC-Api-Key", this._ApiKey);
            req.AddHeader("X-CC-Version", "2018-03-22");

 * 
 * request.AddBody(
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
*/
