using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork_Backend.Middleware
{
    public class RequestCultureProvider : IRequestCultureProvider
    {
        protected readonly Task<ProviderCultureResult> NullResult = Task.FromResult(default(ProviderCultureResult));

        public Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(HttpContext));

            if (!httpContext.Request.Headers.ContainsKey(Microsoft.Net.Http.Headers.HeaderNames.AcceptLanguage))
                return NullResult;

            var cultureValue = httpContext.Request.Headers[Microsoft.Net.Http.Headers.HeaderNames.AcceptLanguage];
            if (string.IsNullOrEmpty(cultureValue))
                return NullResult;
            
            var hasCulture = APP2.PK.Standard.Localization.LanguageSettings.SupportedCultures.Any(i => i.Key.StartsWith(cultureValue));
            if(!hasCulture)
                return NullResult;

            var culture = APP2.PK.Standard.Localization.LanguageSettings.SupportedCultures.FirstOrDefault(i => i.Key.StartsWith(cultureValue));
            return Task.FromResult(new ProviderCultureResult(new Microsoft.Extensions.Primitives.StringSegment(culture.Key)));
        }
    }
}
