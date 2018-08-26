using Microsoft.AspNetCore.Razor.TagHelpers;
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

        public LangugeTagHelper(IConfiguration config)
        {
            _config = config;
        }

        [HtmlAttributeName("asp-language")]
        public Language Language { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Language == GlobalizationHelper.GetCurrentLanguage(_config))
            {
                output.Attributes.Add("class", "white");
            }
        }
    }
}
