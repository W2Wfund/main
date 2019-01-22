using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KBTech.Integration.Merchant.Ripple.Request
{
    public interface IRequest
    {
        string Path { get; }
        string Method { get; }
    }
}