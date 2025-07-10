using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RupStoreAccessToken
{
  public  class AccessTokenService :IAccessTokenService
    {
        //IConfiguration _config;
        //public AccessTokenService()
        //    :this( new Configuration())
        //{

        //}
        //public AccessTokenService(IConfiguration config)
        //{
        //    _config = config;
        //}

        //IConfiguration IAccessTokenService.Config { get { return _config; } } 


        //public IConfiguration Accessconfiguration()
        //{
        //    return _config;
        //}

        public  async Task<AccessToken> GetToken(string UserName, string Password, string GrantType, string ClientId, string ClientSecret)
        {
            
            
            
            string credentials = String.Format("{0}:{1}", ClientId, ClientSecret);

            var form = new Dictionary<string, string>
                {
                {"grant_type", GrantType},
                {"client_id", ClientId},
                {"client_secret", ClientSecret},
                {"username" , UserName },
                {"password", Password },

                };

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("cache-control", "no-cache");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials)));


                FormUrlEncodedContent requestBody = new FormUrlEncodedContent(form);

                var request = await client.PostAsync("http://192.168.2.3:8785/services/oauth/token", requestBody);
                var response = await request.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<AccessToken>(response);

            }
        }
    }

    public interface IAccessTokenService
    {
        //IConfiguration Config { get; }
        //IConfiguration Accessconfiguration();
        Task<AccessToken> GetToken(string UserName, string Password, string GrantType, string ClientId, string ClientSecret);
    }
}
