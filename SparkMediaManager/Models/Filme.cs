// Developed by: Gabriel Duarte
// 
// Created at: 30/04/2016 14:56

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace SparkMediaManager.Models
{
    public class Filme : BaseModel, IMidia
    {
        [JsonProperty("adult")]
        public bool blnAdulto { get; set; }

        [JsonProperty("genres")]
        public IList<Genre> LstGeneros { get; set; }

        public string StrGeneros => string.Join("|", LstGeneros);

        [JsonProperty("id")]
        public int IntCodigoTmdb { get; set; }

        [JsonProperty("imdb_id")]
        public string StrCodigoImdb { get; set; }

        [JsonProperty("release_date")]
        public DateTime? DtmLancamento { get; set; }

        [JsonProperty("status")]
        public string StrStatus { get; set; }

        [JsonProperty("tagline")]
        public string StrTagline { get; set; }

        #region Implementation of IMidia

        [Key]
        public int IntCodigo { get; set; }

        [JsonProperty("title"), MaxLength(200)]
        public string StrTitulo { get; set; }

        [JsonProperty("overview"), MaxLength(4000)]
        public string StrSinopse { get; set; }

        [JsonProperty("backdrop_path"), MaxLength(200)]
        public string StrBackground { get; set; }

        [JsonProperty("poster_path")]
        public string StrPoster { get; set; }

        public byte[] BytCacheBackground { get; set; }

        public byte[] BytCachePoster { get; set; }

        public string StrPasta { get; set; }

        public string StrMetadata { get; set; }

        public string StrFormatoRenomeioPersonalizado { get; set; }

        public void SetPastaPadrao()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    [NotMapped]
    public class Genre
    {
        [JsonProperty("id")]
        public int IntCodigo { get; set; }
        [JsonProperty("name")]
        public string StrNome { get; set; }

        #region Overrides of Object

        public override string ToString()
        {
            return StrNome;
        }

        #endregion
    }
}
