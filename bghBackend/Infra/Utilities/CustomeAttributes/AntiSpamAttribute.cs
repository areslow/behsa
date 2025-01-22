using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Cryptography;
using System.Web;

namespace bghBackend.Infra.Utilities.CustomeAttributes
{
    public class AntiSpamAttribute : ActionFilterAttribute
    {
        public int DelayRequest = 5; // seconds
        public string ErrorMessage = "تعداد درخواست بیشتر از حد مجاز است";
        public bool AddAddress = true;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // request object
            var request = filterContext.HttpContext.Request;
            //var cache = filterContext.HttpContext.
            //user IP
            //var IP = request.Server
            base.OnActionExecuting(filterContext);
        }
    }
}
