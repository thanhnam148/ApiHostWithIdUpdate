using System.Linq;
using System.Threading.Tasks;
using Google.Authenticator;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace Volo.Abp.Account.Web.Pages.Account
{
    public class TwoFactorEnabledModel : AccountPageModel
    {
        [BindProperty]
        public UserAuthen CurrentUser { get; set; }
        protected IConfiguration Configuration { get; private set; }
        public TwoFactorEnabledModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public virtual async Task<IActionResult> OnGetAsync()
        {
            //var user = User;// TODO: fetch signed in user from a database
            var identity = User.Identities.First();
            var emailClaim = identity.FindFirst(ClaimTypes.Email);

            if (emailClaim == null)
            {
                return Redirect("/Account/Login");
            }

            TwoFactorAuthenticator twoFactor = new TwoFactorAuthenticator();
            var setupInfo =
                twoFactor.GenerateSetupCode("myapp", emailClaim.Value, TwoFactorKey(emailClaim.Value), false, 3);

            CurrentUser = new UserAuthen
            {
                EmailAddress = emailClaim.Value,
                SetupCode = setupInfo.ManualEntryKey,
                BarcodeImageUrl = setupInfo.QrCodeSetupImageUrl
            };
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
                    return Redirect("~/");
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
                //var idClaim = identity.Claims[0];
                var user = await UserManager.GetUserAsync(User);
                Microsoft.AspNetCore.Identity.IdentityResult result = await UserManager.SetTwoFactorEnabledAsync(user, true);
                // TODO: store the updated user in database
                return Redirect(Configuration["App:ClientUrl"]);
            }
            catch (BusinessException e)
            {
                return Page();
            }
        }
    }

    public class UserAuthen
    {
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string SetupCode { get; set; }
        public string BarcodeImageUrl { get; set; }
        public string InputCode { get; set; }
    }
}
