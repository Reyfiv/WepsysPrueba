using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;



namespace AppDesktop.Services
{
    public class PersonasServices
    {
        //obtiene una lista de Personas
        public async Task<List<Personas>> GetPersonas()
        {
            try
            {
                string url = $"api/Personas";
                ApiHelper.ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenSTC.Token);
                using (HttpResponseMessage respose = await ApiHelper.ApiClient.GetAsync(url))
                {
                    if (respose.IsSuccessStatusCode)
                    {
                        var Personas = await respose.Content.ReadAsAsync<List<Personas>>();
                        return Personas;
                    }
                    else
                    {
                        if (respose.StatusCode == System.Net.HttpStatusCode.NotFound)
                        {
                            throw new Exception("Servidor no encontrado");
                        }
                        else
                            throw new Exception(respose.ReasonPhrase);
                    }
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

       //Obtiene 1 Persona
        public async Task<Personas> GetPersona(int id)
        {
            try
            {
                string url = $"api/Personas/" + id;
                ApiHelper.ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenSTC.Token);
                using (HttpResponseMessage respose = await ApiHelper.ApiClient.GetAsync(url))
                {
                    if (respose.IsSuccessStatusCode)
                    {
                        var Persona = await respose.Content.ReadAsAsync<Personas>();
                        return Persona;
                    }
                    else
                    {
                        if (respose.StatusCode == System.Net.HttpStatusCode.NotFound)
                        {
                            throw new Exception("Servidor no encontrado");
                        }
                        else
                            throw new Exception(respose.ReasonPhrase);
                    }
                }
            }
            catch (Exception e)
            {

                throw e;
            }

        }

       // Crea un Persona
        public async Task<bool> PostPersona(Personas Personas)
        {
            try
            {
                string url = $"api/Personas";
                string a = JsonConvert.SerializeObject(Personas);
                ApiHelper.ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenSTC.Token);
                using (HttpResponseMessage respose = await ApiHelper.ApiClient.PostAsJsonAsync(url, Personas))
                {
                    if (respose.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        if (respose.StatusCode == System.Net.HttpStatusCode.NotFound)
                        {
                            throw new Exception("Servidor no encontrado");
                        }
                        else
                            throw new Exception(respose.ReasonPhrase);
                    }
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

       // Actualiza un Persona
        public async Task<bool> PutPersonas(Personas Personas)
        {
            try
            {
                string url = $"api/Personas/" + Personas.Id;
                string a = JsonConvert.SerializeObject(Personas);
                ApiHelper.ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenSTC.Token);
                using (HttpResponseMessage respose = await ApiHelper.ApiClient.PutAsJsonAsync(url, Personas))
                {
                    if (respose.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        if (respose.StatusCode == System.Net.HttpStatusCode.NotFound)
                        {
                            throw new Exception("Servidor no encontrado");
                        }
                        else
                            throw new Exception(respose.ReasonPhrase);
                    }
                }
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        //Elimina un Persona
        public async Task<bool> DeletePersonas(int id)
        {
            try
            {
                string url = $"api/Personas/" + id;
                string a = JsonConvert.SerializeObject(id);
                ApiHelper.ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenSTC.Token);
                using (HttpResponseMessage respose = await ApiHelper.ApiClient.DeleteAsync(url))
                {
                    if (respose.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        if (respose.StatusCode == System.Net.HttpStatusCode.NotFound)
                        {
                            throw new Exception("Servidor no encontrado");
                        }
                        else
                            throw new Exception(respose.ReasonPhrase);
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
