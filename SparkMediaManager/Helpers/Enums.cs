// Developed by: Gabriel Duarte
// 
// Created at: 30/04/2016 01:54

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using SparkMediaManager.Localization;

namespace SparkMediaManager.Helpers
{
    public static class Enums
    {
        public enum IdiomaAplicacao
        {
            [Display(ResourceType = typeof(Label), Description = "Portugues")] Portugues,

            [Display(ResourceType = typeof(Label), Description = "Ingles")] Ingles
        }

        public enum MetodoDeProcessamento
        {
            HardLink = 0,

            Copiar = 1
        }

        public enum TipoConteudo
        {
            [Display(ResourceType = typeof(Label), Description = "Selecione")] Selecione = 0,

            [Display(ResourceType = typeof(Label), Description = "Filme")] Filme = 1,

            [Display(ResourceType = typeof(Label), Description = "Serie")] Série = 2,

            [Display(ResourceType = typeof(Label), Description = "Anime")] Anime = 3,

            [Display(ResourceType = typeof(Label), Description = "Episodio")] Episódio = 4,

            [Display(ResourceType = typeof(Label), Description = "Animes_filmes_e_series")] AnimeFilmeSérie = 7
        }

        public enum TipoMensagem
        {
            [Display(ResourceType = typeof(Label), Description = "Selecione")]
            Selecione = 0,
            [Display(ResourceType = typeof(Label), Description = "Alerta")]
            Alerta = 1,

            AlertaSimNao = 2,

            AlertaSimNaoCancela = 3,
            [Display(ResourceType = typeof(Label), Description = "Info")]
            Informativa = 4,

            QuestionamentoSimNao = 5,

            QuestionamentoSimNaoCancela = 6,

            [Display(ResourceType = typeof(Label), Description = "Erro")]
            Erro = 7
        }

        public static string GetDescricao(this Enum enuTipo)
        {
            Type type = enuTipo.GetType();
            string name = Enum.GetName(type, enuTipo);
            if (name == null)
            {
                return enuTipo.ToString();
            }

            FieldInfo field = type.GetField(name);

            if (field == null)
            {
                return enuTipo.ToString();
            }

            var attr =
                Attribute.GetCustomAttribute(field,
                                             typeof(DisplayAttribute)) as DisplayAttribute;
            return attr != null
                       ? attr.GetDescription()
                       : enuTipo.ToString();
        }

        public static Dictionary<Enum, string> GetListaValores(Type enuTipo)
        {
            Array arrValores = Enum.GetValues(enuTipo);

            return arrValores.Cast<Enum>().ToDictionary(objValor => objValor, objValor => objValor.GetDescricao());
        }
    }
}
