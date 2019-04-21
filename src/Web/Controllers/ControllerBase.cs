using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Web.Models;
using Web.Setting;

namespace Web.Controllers
{
    public class ControllerBase : Controller
    {
        protected ServiceUser ServiceUser { get; set; }

        protected AppSettings AppSettings { get; private set; }

        public ControllerBase(AppSettings settings)
        {
            this.AppSettings = settings;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ControllerContext
                .HttpContext
                .Items
                .TryGetValue(
                    Constants.HttpContextServiceUserItemKey,
                    out object serviceUser);
            ServiceUser = serviceUser as ServiceUser;
            ViewBag.IsDevelopment = this.AppSettings.IsDevelopment;

            base.OnActionExecuting(context);
        }
    }
}
