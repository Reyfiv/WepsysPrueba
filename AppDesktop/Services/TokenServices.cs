using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppDesktop.Services
{
    public class TokenServices
    {
        public async Task<bool> Token()
        {
            try
            {
                string url = $"api/Token";
                using (HttpResponseMessage respose = await ApiHelper.ApiClient.GetAsync(url))
                {
                    if (respose.IsSuccessStatusCode)
                    {
                        var token = await respose.Content.ReadAsAsync<Token>();
                        TokenSTC.Token = token.TokenG;
                        TokenSTC.Expiration = token.Expiration;
                        return true;
                    }
                    else
                    {
                        return false;

                    }
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

    }
}
