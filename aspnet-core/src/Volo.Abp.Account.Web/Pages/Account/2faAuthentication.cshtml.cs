using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Volo.Abp.Account.Web.Pages.Account
{
    public class _2faAuthenticationModel : PageModel
    {
        public virtual async Task<IActionResult> OnGetAsync()
        {
            
            return Page();
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            //try
            //{
            //    if (!Request.Form.ContainsKey("g-recaptcha-response"))
            //        return Page();

            //    var captcha = Request.Form["g-recaptcha-response"].ToString();

            //    if (!await Captcha.IsValid(captcha, Configuration["reCAPTCHA:SecretKey"]))
            //        return Page();

            //    await CheckSelfRegistrationAsync();

            //    if (IsExternalLogin)
            //    {
            //        var externalLoginInfo = await SignInManager.GetExternalLoginInfoAsync();
            //        if (externalLoginInfo == null)
            //        {
            //            Logger.LogWarning("External login info is not available");
            //            return RedirectToPage("./Login");
            //        }

            //        await RegisterExternalUserAsync(externalLoginInfo, Input.EmailAddress);
            //    }
            //    else
            //    {
            //        await RegisterLocalUserAsync();
            //    }

            //    return Redirect(ReturnUrl ?? "~/"); //TODO: How to ensure safety? IdentityServer requires it however it should be checked somehow!
            //}
            //catch (BusinessException e)
            //{
            //    Alerts.Danger(GetLocalizeExceptionMessage(e));
            //    return Page();
            //}

            return Page();
        }
    }
}
