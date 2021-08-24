using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Web;
using Volo.Abp.Account.Settings;

namespace Volo.Abp.Account
{
    public class ReCaptcha
    {
        private readonly HttpClient captchaClient;

        public ReCaptcha(HttpClient captchaClient)
        {
            this.captchaClient = captchaClient;
        }

        public async Task<bool> IsValid(string captcha, string secretKey)
        {
            try
            {
                var postTask = await captchaClient
                    .PostAsync($"?secret={secretKey}&response={captcha}", new StringContent(""));
                postTask.EnsureSuccessStatusCode();

                //read response
                byte[] res = await postTask.Content.ReadAsByteArrayAsync();
                //Serialize into GReponse type
                using (MemoryStream ms = new MemoryStream(res))
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(GResponseModel));
                    var Response = (GResponseModel)serializer.ReadObject(ms);
                    return Response.success;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
