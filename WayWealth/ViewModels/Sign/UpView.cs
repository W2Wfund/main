using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace WayWealth.ViewModels.Sign
{
    public class UpView
    {
        [Display(Name = "InviterTxt", ResourceType = typeof(Resources.Resource))]
        public string InviterTxt { get; set; }

        [Display(Name = "FirstName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                 ErrorMessageResourceName = "FirstNameRequired")]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                 ErrorMessageResourceName = "ValidRequired")]
        public string LastName { get; set; }

        [Display(Name = "MiddleName", ResourceType = typeof(Resources.Resource))]
        public string MiddleName { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                 ErrorMessageResourceName = "EmailRequired")]
        [EmailAddress(ErrorMessageResourceType = typeof(Resources.Resource),
                 ErrorMessageResourceName = "EmailRequired2")]
        public string Email { get; set; }

        [Display(Name = "Login", ResourceType = typeof(Resources.Resource))]
        public string Login { get; set; }

        [Display(Name = "Password", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                 ErrorMessageResourceName = "PasswordRequired")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$", 
            ErrorMessageResourceName = "PassErrMess",
            ErrorMessageResourceType = typeof(Resources.Resource))]
        public string Password { get; set; }

        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                 ErrorMessageResourceName = "ConfirmPasswordRequired")]
        public string ConfirmPassword { get; set; }
    }
}