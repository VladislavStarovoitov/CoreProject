using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Helpers
{
    [HtmlTargetElement(Attributes = "asp-language")]
    public class LangugeTagHelper: TagHelper
    {
        private IConfiguration _config;
        private IHttpContextAccessor _accessor;

        public LangugeTagHelper(IConfiguration config, IHttpContextAccessor accessor)
        {
            _config = config;
            _accessor = accessor;
        }

        [HtmlAttributeName("asp-language")]
        public Language Language { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var qb = new QueryBuilder();

            foreach (var query in _accessor.HttpContext.Request.Query)
            {
                if (query.Key != "Language")
                {
                    qb.Add(query.Key, query.Value.ToList());
                }                
            }

            qb.Add("Language", Language.ToString());

            if (Language == GlobalizationHelper.GetCurrentLanguage(_config))
            {
                output.Attributes.Add("class", "white");
            }

            output.Attributes.Add("href", _accessor.HttpContext.Request.Path + qb.ToQueryString());
        }
    }
}
