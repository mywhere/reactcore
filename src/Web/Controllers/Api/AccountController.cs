using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Models;
using Web.Services;
using Web.Setting;

namespace Web.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService, AppSettings settings) : base(settings)
        {
            this._accountService = accountService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody]LoginModel model)
        {
            var result = await this._accountService.LoginAsync(HttpContext, model.Login, model.Password, model.RememberMe);
            return Json(result);
        }

        [HttpPost("[action]")]
        public IActionResult Logout()
        {
            var result = _accountService.Logout(HttpContext);
            return Json(result);
        }
    }
}
