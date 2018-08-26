using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Helpers
{
    public enum Language
    {
        English = 0,
        Russian = 1
    }

    public static class GlobalizationHelper
    {
        public static Language GetCurrentLanguage(IConfiguration config)
        {
            string name = CultureInfo.CurrentCulture.Name;

            if (!string.IsNullOrEmpty(name) && config != null)
            {
                string language = config[$"Languages:{name}"];

                if (!string.IsNullOrEmpty(language))
                {
                    return language.Equals(Language.English.ToString(), StringComparison.InvariantCultureIgnoreCase)
                        ? Language.English
                        : Language.Russian;
                }
            }

            return Language.English;
        }
    }
}

