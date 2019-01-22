using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WayWealth.Helpers
{
    public class JwtHelper
    {
        public string Encode(object payload, string secret)
        {
            //var payload = new Dictionary<string, object>
            //    {
            //        { "claim1", 0 },
            //        { "claim2", "claim2-value" }
            //    };
            // var secret = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            return encoder.Encode(payload, secret);
        }

        public T Decode<T>(string token, string secret)
        {
            //var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJjbGFpbTEiOjAsImNsYWltMiI6ImNsYWltMi12YWx1ZSJ9.8pwBI_HtXqI3UgQHQ_rDRnSQRxFL1SR8fbQoS-5kM5s";

            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

            return decoder.DecodeToObject<T>(token, secret, verify: true);

            //    try
            //    {


            //    }
            //    catch (TokenExpiredException)
            //    {
            //        //Console.WriteLine("Token has expired");
            //    }
            //    catch (SignatureVerificationException)
            //    {
            //        Console.WriteLine("Token has invalid signature");
            //    }
            //}
        }
    }
}