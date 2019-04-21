using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Setting;

namespace Web.Controllers
{
    public class MainController : ControllerBase
    {
        public MainController(AppSettings settings): base(settings)
        {
        }

        public IActionResult Index()
        {
            var nodeSession = new NodeSession
            {
                Private = new PrivateSession
                {
                    Cookie = string.Join(", ", Request.Cookies.Select(x => $"{x.Key}={x.Value};"))
                },
                Public = new PublicSession
                {
                    ServiceUser = ServiceUser
                }
            };

            return View(nodeSession);
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
