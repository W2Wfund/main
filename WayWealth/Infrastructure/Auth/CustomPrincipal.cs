using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace WayWealth.Infrastructure.Auth
{
    public class CustomPrincipal : UserSerializeModel, IPrincipal
    {
        public IIdentity Identity { get; }
        public bool IsInRole(string role) { return false; }

        public CustomPrincipal(UserSerializeModel instance)
        {
            var properties = instance.GetType().GetProperties();
            foreach (var property in properties)
            {
                var thisProperty = this.GetType().GetProperty(property.Name);
                if (thisProperty != null)
                {
                    thisProperty.SetValue(this, property.GetValue(instance));
                }
            }

            this.Identity = new GenericIdentity(this.id_object.ToString());
        }
    }
}