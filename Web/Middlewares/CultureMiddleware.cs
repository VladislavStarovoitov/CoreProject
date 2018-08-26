using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Middlewares
{
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;
        private const string LangCookieName = "Language";

        public CultureMiddleware(RequestDelegate next, IConfiguration config)
        {
            this._next = next;
            AppConfiguration = config;
        }

        public IConfiguration AppConfiguration { get; set; }

        public async Task Invoke(HttpContext context)
        {
            IConfigurationSection cultures = AppConfiguration.GetSection("Cultures");
            string lang = context.Request.Query["language"].ToString();
            string culture = string.Empty;

            if (!string.IsNullOrEmpty(lang))
            {
                culture = cultures[lang];
                if (!string.IsNullOrEmpty(culture))
                {
                    try
                    {
                        CultureInfo.CurrentCulture = new CultureInfo(culture);
                        CultureInfo.CurrentUICulture = new CultureInfo(culture);

                        SetLangCookie(context, lang);
                    }
                    catch (CultureNotFoundException) { }
                }
            }
            else
            {
                lang = context.Request.Cookies[LangCookieName];
                culture = !string.IsNullOrEmpty(lang) 
                    ? cultures[lang]
                    : cultures["DefaulyCulture"];

                if (!string.IsNullOrEmpty(culture))
                {
                    try
                    {
                        CultureInfo.CurrentCulture = new CultureInfo(culture);
                        CultureInfo.CurrentUICulture = new CultureInfo(culture);
                    }
                    catch (CultureNotFoundException) { }
                }
            }
            await _next.Invoke(context);
        }

        private void SetLangCookie(HttpContext context, string lang)
        {
            context.Response.Cookies.Append(LangCookieName, lang, new CookieOptions
            {
                Expires = DateTime.Now.AddYears(1)
            });
        }
    }
}
