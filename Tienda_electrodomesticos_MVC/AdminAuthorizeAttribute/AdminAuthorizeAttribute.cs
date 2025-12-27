using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class AdminAuthorizeAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var rol = context.HttpContext.Session.GetString("UsuarioRol");

        if (rol == null || rol.ToUpper() != "ROLE_ADMIN")
        {
            // Si no es admin, redirige al home o login
            context.Result = new RedirectToActionResult("Index", "Home", null);
        }

        base.OnActionExecuting(context);
    }
}
