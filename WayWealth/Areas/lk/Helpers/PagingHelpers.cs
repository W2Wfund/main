using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WayWealth.Areas.lk.ViewModels;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace WayWealth.Areas.lk.Helpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
        PageInfo pageInfo, string additionalClass, Func<int, string> pageUrl)
        {
            TagBuilder root = new TagBuilder("ul");
            root.AddCssClass("pagination");
            if (!string.IsNullOrWhiteSpace(additionalClass))
                root.AddCssClass(additionalClass);

            #region Стрелка влево
            var first = new TagBuilder("li");
            var firstTag = new TagBuilder("a");
            if (pageInfo.PageNumber > 1)
            {
                firstTag.MergeAttribute("href", pageUrl(pageInfo.PageNumber - 1));
            }
            firstTag.MergeAttribute("rel", "nofollow");
            var firstI = new TagBuilder("i");
            firstI.AddCssClass("ion-ios-arrow-back");
            firstTag.InnerHtml += firstI.ToString();
            first.InnerHtml += firstTag.ToString();
            root.InnerHtml += first.ToString();
            #endregion
            #region Первая
            root.InnerHtml += CreatePageTag(1, pageInfo.PageNumber, pageUrl(1)).ToString();
            #endregion

            #region Между 1й и последней

            var min = pageInfo.PageNumber - 2;
            var max = pageInfo.PageNumber + 2;
            if (min < 2)
                min = 2;
            if (max > pageInfo.TotalPages - 1)
                max = pageInfo.TotalPages - 1;

            // точки слева
            if (min > 2)
                root.InnerHtml += CreateDots();

            // цифры
            for (int i = min; i <= max; i++)
                root.InnerHtml += CreatePageTag(i, pageInfo.PageNumber, pageUrl(i)).ToString();

            // точки справа
            if (max < pageInfo.TotalPages - 1)
                root.InnerHtml += CreateDots();
            #endregion

            #region Последняя
            if (pageInfo.TotalPages > 1)
                root.InnerHtml += CreatePageTag(pageInfo.TotalPages, pageInfo.PageNumber, pageUrl(pageInfo.TotalPages)).ToString();
            #endregion
            #region Стрелка вправо
            var last = new TagBuilder("li");
            var lastTag = new TagBuilder("a");
            if (pageInfo.PageNumber < pageInfo.TotalPages)
            {
                lastTag.MergeAttribute("href", pageUrl(pageInfo.PageNumber + 1));
            }
            lastTag.MergeAttribute("rel", "nofollow");

            var lastI = new TagBuilder("i");
            lastI.AddCssClass("ion-ios-arrow-forward");
            lastTag.InnerHtml += lastI.ToString();
            last.InnerHtml += lastTag.ToString();
            root.InnerHtml += last.ToString();
            #endregion

            return MvcHtmlString.Create(root.ToString());
        }

        public static MvcHtmlString PageLinksAjax(this HtmlHelper html,
        PageInfo pageInfo, string additionalClass, Func<int, string> pageUrl, string container)
        {
            TagBuilder root = new TagBuilder("ul");
            root.AddCssClass("pagination");
            if (!string.IsNullOrWhiteSpace(additionalClass))
                root.AddCssClass(additionalClass);

            #region Стрелка влево
            var first = new TagBuilder("li");
            var firstTag = new TagBuilder("a");
            if (pageInfo.PageNumber > 1)
            {
                firstTag.MergeAttribute("href", pageUrl(pageInfo.PageNumber - 1));
                firstTag.MergeAttribute("data-ajax", "true");
                firstTag.MergeAttribute("data-ajax-mode", "replace");
                firstTag.MergeAttribute("data-ajax-update", container);
            }
            firstTag.MergeAttribute("rel", "nofollow");
            var firstI = new TagBuilder("i");
            firstI.AddCssClass("ion-ios-arrow-back");
            firstTag.InnerHtml += firstI.ToString();
            first.InnerHtml += firstTag.ToString();
            root.InnerHtml += first.ToString();
            #endregion
            #region Первая
            root.InnerHtml += CreatePageTagAjax(1, pageInfo.PageNumber, pageUrl(1), container).ToString();
            #endregion

            #region Между 1й и последней

            var min = pageInfo.PageNumber - 2;
            var max = pageInfo.PageNumber + 2;
            if (min < 2)
                min = 2;
            if (max > pageInfo.TotalPages - 1)
                max = pageInfo.TotalPages - 1;

            // точки слева
            if (min > 2)
                root.InnerHtml += CreateDots();

            // цифры
            for (int i = min; i <= max; i++)
                root.InnerHtml += CreatePageTagAjax(i, pageInfo.PageNumber, pageUrl(i), container).ToString();

            // точки справа
            if (max < pageInfo.TotalPages - 1)
                root.InnerHtml += CreateDots();
            #endregion

            #region Последняя
            if (pageInfo.TotalPages > 1)
                root.InnerHtml += CreatePageTagAjax(pageInfo.TotalPages, pageInfo.PageNumber, pageUrl(pageInfo.TotalPages), container).ToString();
            #endregion
            #region Стрелка вправо
            var last = new TagBuilder("li");
            var lastTag = new TagBuilder("a");
            if (pageInfo.PageNumber < pageInfo.TotalPages)
            {
                lastTag.MergeAttribute("href", pageUrl(pageInfo.PageNumber + 1));
                lastTag.MergeAttribute("data-ajax", "true");
                lastTag.MergeAttribute("data-ajax-mode", "replace");
                lastTag.MergeAttribute("data-ajax-update", container);
            }
            lastTag.MergeAttribute("rel", "nofollow");

            var lastI = new TagBuilder("i");
            lastI.AddCssClass("ion-ios-arrow-forward");
            lastTag.InnerHtml += lastI.ToString();
            last.InnerHtml += lastTag.ToString();
            root.InnerHtml += last.ToString();
            #endregion

            return MvcHtmlString.Create(root.ToString());
        }
        static TagBuilder CreatePageTagAjax(int number, int activePageNumber, string url, string container)
        {
            TagBuilder wrapper = new TagBuilder("li");
            if (number == activePageNumber)
            {
                wrapper.AddCssClass("active");
            }

            TagBuilder tag = new TagBuilder("a");
            tag.MergeAttribute("href", url);
            tag.MergeAttribute("data-ajax", "true");
            tag.MergeAttribute("data-ajax-mode", "replace");
            tag.MergeAttribute("data-ajax-update", container);
            tag.MergeAttribute("rel", "nofollow");
            tag.InnerHtml = "<span data-text=" + number.ToString() + ">" + number.ToString() + "</span>";


            wrapper.InnerHtml += tag.ToString();
            return wrapper;
        }
        static TagBuilder CreatePageTag(int number, int activePageNumber, string url)
        {
            TagBuilder wrapper = new TagBuilder("li");
            if (number == activePageNumber)
            {
                wrapper.AddCssClass("active");
            }

            TagBuilder tag = new TagBuilder("a");
            tag.MergeAttribute("href", url);
            tag.MergeAttribute("rel", "nofollow");
            tag.InnerHtml = "<span data-text=" + number.ToString() + ">" + number.ToString() + "</span>";


            wrapper.InnerHtml += tag.ToString();
            return wrapper;
        }
        static TagBuilder CreateDots()
        {
            TagBuilder wrapper = new TagBuilder("li");
            wrapper.InnerHtml = "...";
            return wrapper;
        }

        public static string UserIP()
        {
            string ipAddress = System.Web.HttpContext.Current.Request.Headers["CF-Connecting-IP"];
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = System.Web.HttpContext.Current.Request.UserHostAddress;
            }
            return ipAddress;
        }
        public static string UserCountry()
        {
            string country = System.Web.HttpContext.Current.Request.Headers["CF-IPCountry"];
            if (string.IsNullOrEmpty(country))
            {
                country = Resources.Resource.Undefined;
            }
            return country;
        }
        public static string UserBrowser()
        {
            string ua = System.Web.HttpContext.Current.Request.UserAgent;
            if (string.IsNullOrEmpty(ua))
            {
                ua = Resources.Resource.Undefined;
            }
            return ua;
        }

        public static bool WithdrawTime()
        {
            if ((DateTime.Now.DayOfWeek != DayOfWeek.Friday) || (DateTime.Now.TimeOfDay > System.TimeSpan.Parse("14:00:00")))
            {
                return false;
            }

            return true;
        }

        public static string GetAuthorReview(string name)
        {
            string[] words_name = name.Split(' ');

            if (!string.IsNullOrEmpty(words_name[1]))
            {
                return words_name[1] + " " + words_name[0].Substring(0, 1) + ".";
            }
            else
            {
                return name;
            }
        }

        public static string GetUniqueKey(int size)
        {
            char[] chars =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[size];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}