// Developed by: Gabriel Duarte
// 
// Created at: 30/04/2016 18:47

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SparkMediaManager.Models;
using SparkMediaManager.Properties;

namespace SparkMediaManager.Helpers.API
{
    public static class Tvdb
    {
        private static async Task<string> GetNovoTokenAsync()
        {
            var objBaseUrl = new Uri(Settings.Default.ApiTvdbBaseUrl);
            string strJson = JObject.FromObject(new {apikey = Settings.Default.ApiTvdbKey}).ToString(Formatting.None);

            try
            {
                int statusCode;
                bool blnSucesso;
                dynamic dynJson;

                using (var objHttpClient = new HttpClient {BaseAddress = objBaseUrl})
                {
                    objHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    using (var objContent = new StringContent(strJson, Encoding.UTF8, "application/json"))
                    {
                        using (HttpResponseMessage objResponse = await objHttpClient.PostAsync("login", objContent))
                        {
                            blnSucesso = objResponse.IsSuccessStatusCode;
                            statusCode = (int) objResponse.StatusCode;
                            string strResponseData = await objResponse.Content.ReadAsStringAsync();

                            dynJson = JsonConvert.DeserializeObject<dynamic>(strResponseData);
                        }
                    }
                }

                if (!blnSucesso)
                {
                    throw new HttpException(statusCode, string.Format("Status code: \"{0}\". Message: \"{1}\".", statusCode, dynJson.Error));
                }

                if (dynJson?.token == null)
                {
                    throw new NullReferenceException();
                }

                return dynJson.token;
            }
            catch (Exception e)
            {
                // TODO Tratar exception.
            }

            return null;
        }

        public static async Task<Dictionary<int, string>> GetBuscaSeriesAsync(string strNome)
        {
            var objBaseUrl = new Uri(Settings.Default.ApiTvdbBaseUrl);
            var dicRetorno = new Dictionary<int, string>();

            string strToken = await GetTokenAsync();

            try
            {
                int statusCode;
                bool blnSucesso;
                dynamic dynJson;

                using (var httpClient = new HttpClient {BaseAddress = objBaseUrl})
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {strToken}");
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Language", Settings.Default.prefIdiomaPesquisa);

                    using (HttpResponseMessage response = await httpClient.GetAsync($"search/series?name={strNome}"))
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        statusCode = (int) response.StatusCode;
                        blnSucesso = response.IsSuccessStatusCode;
                        dynJson = JsonConvert.DeserializeObject<dynamic>(responseData);
                    }
                }

                if (!blnSucesso)
                {
                    throw new HttpException(statusCode, string.Format("Status code: \"{0}\". Message: \"{1}\".", statusCode, dynJson.Error));
                }

                if (dynJson?.data == null)
                {
                    throw new NullReferenceException();
                }

                foreach (JObject dynResultado in dynJson.data)
                {
                    dicRetorno.Add(dynResultado["id"].Value<int>(), dynResultado["seriesName"].Value<string>());
                }
            }
            catch (Exception e)
            {
                // TODO Tratar exception
            }

            return dicRetorno;
        }

        public static async Task<Serie> GetSerieAsync(int intCodigoTvdb)
        {
            var objBaseUrl = new Uri(Settings.Default.ApiTvdbBaseUrl);

            string strToken = await GetTokenAsync();

            try
            {
                Serie objSerie;

                using (var httpClient = new HttpClient {BaseAddress = objBaseUrl})
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {strToken}");
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Language", Settings.Default.prefIdiomaPesquisa);

                    using (HttpResponseMessage response = await httpClient.GetAsync($"series/{intCodigoTvdb}"))
                    {
                        string responseData = await response.Content.ReadAsStringAsync();

                        if (response.IsSuccessStatusCode)
                        {
                            var objSerieData = JsonConvert.DeserializeObject<SerieData>(responseData);
                            objSerie = objSerieData.ObjSerie;
                        }
                        else
                        {
                            var dynErro = JsonConvert.DeserializeObject<dynamic>(responseData);
                            throw new HttpException((int) response.StatusCode, string.Format("Status code: \"{0}\". Message: \"{1}\".", response.StatusCode, dynErro.Error));
                        }
                    }
                }

                return objSerie;
            }
            catch (Exception e)
            {
                // TODO Tratar exception
            }

            return null;
        }

        private static async Task<string> GetTokenAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Settings.Default.ApiTvdbJwtToken) || VerificaNecessidadeRenovacaoJwt(Settings.Default.ApiTvdbJwtToken))
                {
                    Settings.Default.ApiTvdbJwtToken = await GetNovoTokenAsync();
                    Settings.Default.Save();
                }
            }
            catch (Exception e)
            {
                // TODO Tratar exception
            }

            return Settings.Default.ApiTvdbJwtToken;
        }

        /// <summary>
        ///     Verifica se é preciso renovar o token JWT.
        /// </summary>
        /// <param name="apiTvdbJwtToken">Token encodado.</param>
        /// <returns>True se precisa renovar, False se o token ainda é válido.</returns>
        private static bool VerificaNecessidadeRenovacaoJwt(string apiTvdbJwtToken)
        {
            var blnRetorno = false;

            try
            {
                var oJwt = new JwtSecurityToken(apiTvdbJwtToken);
                int intExpirationDateUnix = int.Parse(oJwt.Claims.First(x => x.Type == "exp").Value);
                blnRetorno = Helper.UnixTimeStampParaDateTime(intExpirationDateUnix) < DateTime.Now;
            }
            catch (Exception e)
            {
                // TODO Tratar exception
            }

            return blnRetorno;
        }
    }
}
