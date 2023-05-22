
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Util.Common;

namespace Api.Base
{
    [AllowAnonymous]
    public class HandlerBaseLiteController<DTO> : Controller
    {
        public HandlerBaseLiteController()
        {

        }
        protected IActionResult HandlerResponse<DTO>(DTO dato)
        {
            return this.Ok(new ResponseApi<DTO> { Data = dato, Status = true, Message = "Operation carried out successfully." });
        }
    }
}
