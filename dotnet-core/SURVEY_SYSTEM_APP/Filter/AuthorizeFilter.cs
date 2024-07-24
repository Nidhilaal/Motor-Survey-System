using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SURVEY_SYSTEM_APP.Filter
{
    public class AuthorizeFilter:IAuthorizationFilter
    {
        private readonly ISession _session;
        public AuthorizeFilter(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {

            if (string.IsNullOrEmpty(_session.GetString("USER_ID")))
            {
                context.Result = new RedirectResult("/Login/Index");
            }

        }

        public sealed class AuthorizeAttribute : TypeFilterAttribute
        {
            public AuthorizeAttribute() : base(typeof(AuthorizeFilter))
            {
                Arguments = new object[] { };
            }

        }
    }
}
