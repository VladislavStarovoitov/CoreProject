using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ModelBinders
{
    public class BirthDateModelBinder : IModelBinder
    {
        private readonly IModelBinder fallbackBinder;

        public BirthDateModelBinder(IModelBinder fallbackBinder)
        {
            this.fallbackBinder = fallbackBinder;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var datePartValues = bindingContext.ValueProvider.GetValue("Date");
            var timePartValues = bindingContext.ValueProvider.GetValue("Time");

            if (datePartValues == ValueProviderResult.None || timePartValues == ValueProviderResult.None)
                return fallbackBinder.BindModelAsync(bindingContext);

            string date = datePartValues.FirstValue;
            string time = timePartValues.FirstValue;

            DateTime.TryParse(date, out DateTime parsedDateValue);
            DateTime.TryParse(time, out DateTime parsedTimeValue);

            var result = new DateTime(parsedDateValue.Year,
                            parsedDateValue.Month,
                            parsedDateValue.Day,
                            parsedTimeValue.Hour,
                            parsedTimeValue.Minute,
                            parsedTimeValue.Second);

            bindingContext.Result = ModelBindingResult.Success(result);
            return Task.CompletedTask;
        }
    }
}
