using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Google.Authenticator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Volo.Abp.Account.Web.Pages.Account
{
    public class TwoStepModel : AccountPageModel
    {
        [BindProperty]
        public UserAuthen CurrentUser { get; set; }
        protected IConfiguration Configuration { get; private set; }
        public TwoStepModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public virtual async Task<IActionResult> OnGetAsync()
        {
            var identity = User.Identities.First();
            var emailClaim = identity.FindFirst(ClaimTypes.Email);

            if (emailClaim == null)
            {
                return Redirect("/Account/Login");
            }
            return Page();
        }

        private static string TwoFactorKey(string email)
        {
            return $"myverysecretkey+{email}";
        }

        public virtual async Task<IActionResult> OnPostAsync(string action)
        {
            try
            {
                if (action == "Cancel")
                {
                    await SignInManager.SignOutAsync();
                    return RedirectToPage("/Account/Login");
                }
                var identity = User.Identities.First();
                var emailClaim = identity.FindFirst(ClaimTypes.Email);
                TwoFactorAuthenticator twoFactor = new TwoFactorAuthenticator();
                bool isValid = twoFactor.ValidateTwoFactorPIN(TwoFactorKey(emailClaim.Value), CurrentUser.InputCode);
                if (!isValid)
                {
                    Alerts.Danger("Invalid InputCode!");
                    return Page();
                }
                return Redirect(Configuration["App:ClientUrl"]);
            }
            catch (BusinessException e)
            {
                return Page();
            }
        }
    }
}
