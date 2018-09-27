using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ModelBinders
{
    public class BirthDateModelBinderProvider : IModelBinderProvider
    {
        private readonly IModelBinder binder =
           new BirthDateModelBinder(new SimpleTypeModelBinder(typeof(DateTime)));

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            return context.Metadata.ModelType == typeof(DateTime) ? binder : null;
        }
    }
}
