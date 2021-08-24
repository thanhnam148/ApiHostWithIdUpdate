namespace Volo.Abp.Account.Settings
{
    public class CaptchaSettings
    {
        public string ClientKey { get; set; }
        public string ServerKey { get; set; }
    }
    public class GResponseModel
    {
        public bool success { get; set; }
        public string challenge_ts { get; set; }
        public string hostname { get; set; }
        public string[] error_codes { get; set; }
    }
}
