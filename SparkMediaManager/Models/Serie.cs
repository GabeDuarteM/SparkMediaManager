// Developed by: Gabriel Duarte
// 
// Created at: 30/04/2016 17:42

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SparkMediaManager.Models
{
    [JsonObject("data")]
    public class Serie : ModelBase, IMidia
    {
        private string _strTitulo;

        private byte[] _bytCachePoster;

        [JsonProperty("id")]
        public int IntCodigoTvdb { get; set; }

        [JsonProperty("aliases")]
        public IList<string> ListAliases { get; set; }

        //[JsonProperty("seriesId")]
        //public int seriesId { get; set; }

        [JsonProperty("status")]
        public string StrStatus { get; set; }

        //[JsonProperty("firstAired")]
        //public string firstAired { get; set; }

        //[JsonProperty("network")]
        //public string network { get; set; }

        //[JsonProperty("networkId")]
        //public string networkId { get; set; }

        //[JsonProperty("runtime")]
        //public string runtime { get; set; }

        [JsonProperty("genre")]
        public IList<string> LstGeneros { get; set; }

        public string StrGeneros => string.Join("|", LstGeneros);

        [JsonProperty("lastUpdated")]
        public int IntUltimaAtualizacao { get; set; }

        [JsonProperty("airsDayOfWeek")]
        public string StrDiaDaSemana { get; set; }

        [JsonProperty("airsTime")]
        public string StrHora { get; set; }

        //[JsonProperty("rating")]
        //public string rating { get; set; }

        [JsonProperty("imdbId")]
        public string StrCodigoImdb { get; set; }

        //[JsonProperty("zap2itId")]
        //public string StrCodigoZap2It { get; set; }

        [JsonProperty("added")]
        public DateTime? DtmAdicionadoTvdb { get; set; }

        [JsonProperty("siteRating")]
        public double DblRating { get; set; }

        [JsonProperty("siteRatingCount")]
        public int IntRatingCount { get; set; }

        #region Implementation of IMidia

        public int IntCodigo { get; set; }

        [JsonProperty("seriesName")]
        public string StrTitulo
        {
            get { return _strTitulo; }
            set { Set(ref _strTitulo, value); }
        }

        [JsonProperty("overview")]
        public string StrSinopse { get; set; }

        public string StrBackground { get; set; }

        public string StrPoster { get; set; }

        public byte[] BytCacheBackground { get; set; }

        public byte[] BytCachePoster
        {
            get { return _bytCachePoster; }
            set { Set(ref _bytCachePoster, value); }
        }

        public string StrPasta { get; set; }

        public string StrMetadata { get; set; }

        public string StrFormatoRenomeioPersonalizado { get; set; }

        public void SetPastaPadrao()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
