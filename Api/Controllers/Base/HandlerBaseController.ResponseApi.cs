
using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;


namespace Api.Base
{
    //[Authorize]
    public abstract partial class HandlerBaseController<T, TNeogcio> : HandlerBaseLiteController<T>
    {

    }   
}
