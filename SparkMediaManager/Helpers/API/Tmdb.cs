// Developed by: Gabriel Duarte
// 
// Created at: 30/04/2016 00:19

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SparkMediaManager.Models;
using SparkMediaManager.Properties;

namespace SparkMediaManager.Helpers.API
{
    public static class Tmdb
    {
        public static async Task<Dictionary<int, string>> GetBuscaFilmesAsync(string strQuery)
        {
            var objBaseUrl = new Uri(Settings.Default.ApiTmdbBaseUrl);
            string strApiKey = Settings.Default.ApiTmdbKey;
            string strLanguage = Settings.Default.prefIdiomaPesquisa;
            var dicRetorno = new Dictionary<int, string>();

            try
            {
                dynamic dynJson;

                using (var httpClient = new HttpClient {BaseAddress = objBaseUrl})
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");

                    using (HttpResponseMessage response = await httpClient.GetAsync($"search/movie?api_key={strApiKey}&query={strQuery}&language={strLanguage}"))
                    {
                        string responseData = await response.Content.ReadAsStringAsync();

                        dynJson = JsonConvert.DeserializeObject<dynamic>(responseData);
                    }
                }

                if (dynJson?.status_code != null)
                {
                    throw new HttpException(GetCodigoErroHttp(dynJson.status_code.ToString()), $"Status code: \"{dynJson.status_code}\". Message: \"{dynJson.status_message}\".");
                }

                if (dynJson?.results == null)
                {
                    throw new NullReferenceException();
                }

                JArray lstResultados = dynJson.results;

                foreach (JToken resultado in lstResultados)
                {
                    dicRetorno.Add(resultado["id"].Value<int>(), resultado["title"].Value<string>());
                }
            }
            catch
            {
                // TODO Tratar exceção
            }

            return dicRetorno;
        }

        public static async Task<Filme> GetFilmeAsync(int intCodigoTvdb)
        {
            var objBaseUrl = new Uri(Settings.Default.ApiTmdbBaseUrl);
            string strApiKey = Settings.Default.ApiTmdbKey;
            string strLanguage = Settings.Default.prefIdiomaPesquisa;

            try
            {
                Filme objFilme;

                using (var httpClient = new HttpClient {BaseAddress = objBaseUrl})
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");

                    using (HttpResponseMessage response = await httpClient.GetAsync($"movie/{intCodigoTvdb}?api_key={strApiKey}&language={strLanguage}"))
                    {
                        string responseData = await response.Content.ReadAsStringAsync();

                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            objFilme = JsonConvert.DeserializeObject<Filme>(responseData);
                        }
                        else
                        {
                            var dynErro = JsonConvert.DeserializeObject<dynamic>(responseData);
                            throw new HttpException((int) response.StatusCode, $"Status code: \"{(int) response.StatusCode}\". Message: \"{dynErro?.status_message}\".");
                        }
                    }
                }

                return objFilme;
            }
            catch (Exception e)
            {
                // TODO Tratar exceptions.
                return null;
            }
        }

        private static int GetCodigoErroHttp(string strStatusCode)
        {
            int intStatusCode;
            int.TryParse(strStatusCode, out intStatusCode);

            switch (intStatusCode)
            {
                case 1:
                case 13:
                case 21:
                    return 200;
                case 2:
                    return 501;
                case 3:
                case 7:
                case 10:
                case 14:
                case 16:
                case 17:
                case 30:
                case 31:
                case 32:
                case 33:
                case 34:
                    return 401;
                case 4:
                    return 405;
                case 5:
                case 20:
                    return 422;
                case 6:
                    return 404;
                case 8:
                    return 403;
                case 9:
                    return 503;
                case 11:
                case 15:
                    return 500;
                case 18:
                case 22:
                case 23:
                case 26:
                case 27:
                case 28:
                case 29:
                    return 400;
                case 19:
                    return 406;
                case 24:
                    return 504;
                case 25:
                    return 429;
                default:
                    throw new Exception(); // TODO Tratar Exception.
            }
        }
    }
}
