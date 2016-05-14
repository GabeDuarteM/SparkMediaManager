// Developed by: Gabriel Duarte
// 
// Created at: 03/05/2016 01:07

using System.Collections.Generic;
using Newtonsoft.Json;

namespace SparkMediaManager.Models
{
    public class SerieData
    {
        [JsonProperty("errors")]
        public Errors ObjErros { get; set; }
        [JsonProperty("data")]
        public Serie ObjSerie { get; set; }
    }

    public class Errors
    {
        [JsonProperty("invalidFilters")]
        public IList<string> invalidFilters { get; set; }
        [JsonProperty("invalidLanguage")]
        public string invalidLanguage { get; set; }
        [JsonProperty("invalidQueryParams")]
        public IList<string> invalidQueryParams { get; set; }
    }
}
