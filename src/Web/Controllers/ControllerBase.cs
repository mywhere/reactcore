using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Web.Models;
using Web.Setting;

namespace Web.Controllers
{
    public class ControllerBase : Controller
    {
        protected ServiceUser ServiceUser { get; set; }

        protected AppSetting AppSetting { get; private set; }

        public ControllerBase(AppSetting appSetting)
        {
            this.AppSetting = appSetting;
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
            ViewBag.IsDevelopment = this.AppSetting.IsDevelopment;

            base.OnActionExecuting(context);
        }
    }
}
