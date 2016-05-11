// Developed by: Gabriel Duarte
// 
// Created at: 10/05/2016 00:59

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SparkMediaManager.Helpers;
using SparkMediaManager.Localization;

namespace SparkMediaManager.ViewModels
{
    public class PreferenciasViewModel : BaseViewModel
    {
        private Dictionary<Enum, string> _dicIdiomasAplicacao;

        private Dictionary<string, string> _dicIdiomasPesquisa;

        private Dictionary<Enum, string> _dicMetodosProcessamento;

        private Enums.IdiomaAplicacao _enuIdiomaAplicacaoSelecionado;

        private Enums.MetodoDeProcessamento _enuMetodoDeProcessamentoSelecionado;

        private int _intTempoAtualizarConteudoNovo;

        private string _strIdiomaPesquisaSelecionado;

        private string _strPastaAnime;

        private string _strPastaFilme;

        private string _strPastaSerie;

        private string _strTorrentBlackhole;

        private string _strFormatoSerie;

        private string _strFormatoFilme;

        private string _strFormatoAnime;

        private string _strVisualizacaoFormatoSerie;

        private string _strVisualizacaoFormatoFilme;

        private string _strVisualizacaoFormatoAnime;

        public PreferenciasViewModel()
        {
            DicIdiomasPesquisa = GetIdiomas();
            DicMetodosProcessamento = Enums.GetListaValores(typeof(Enums.MetodoDeProcessamento));
            DicIdiomasAplicacao = Enums.GetListaValores(typeof(Enums.IdiomaAplicacao));

            if (!IsInDesignModeStatic)
            {
                return;
            }

            StrPastaSerie = @"C:\Series";
            IntTempoAtualizarConteudoNovo = 60;
            StrIdiomaPesquisaSelecionado = "pt";
            EnuMetodoDeProcessamentoSelecionado = Enums.MetodoDeProcessamento.HardLink;
            EnuIdiomaAplicacaoSelecionado = Enums.IdiomaAplicacao.Portugues;
            StrVisualizacaoFormatoSerie = "Game of Thrones - S06E03 - Oathbreaker";
            StrVisualizacaoFormatoFilme = "Capitão América Guerra Civil (2016)";
            StrVisualizacaoFormatoAnime = "My Hero Academia - 06 - Rage, You Damn Nerd";

        }

        public string StrFormatoSerie
        {
            get { return _strFormatoSerie; }
            set { Set(ref _strFormatoSerie, value); }
        }

        public string StrFormatoFilme
        {
            get { return _strFormatoFilme; }
            set { Set(ref _strFormatoFilme, value); }
        }

        public string StrFormatoAnime
        {
            get { return _strFormatoAnime; }
            set { Set(ref _strFormatoAnime, value); }
        }

        public string StrVisualizacaoFormatoSerie
        {
            get { return _strVisualizacaoFormatoSerie; }
            set { Set(ref _strVisualizacaoFormatoSerie, value); }
        }

        public string StrVisualizacaoFormatoFilme
        {
            get { return _strVisualizacaoFormatoFilme; }
            set { Set(ref _strVisualizacaoFormatoFilme, value); }
        }

        public string StrVisualizacaoFormatoAnime
        {
            get { return _strVisualizacaoFormatoAnime; }
            set { Set(ref _strVisualizacaoFormatoAnime, value); }
        }

        public Dictionary<Enum, string> DicIdiomasAplicacao
        {
            get { return _dicIdiomasAplicacao; }
            set { Set(ref _dicIdiomasAplicacao, value); }
        }

        public Dictionary<string, string> DicIdiomasPesquisa
        {
            get { return _dicIdiomasPesquisa; }
            set { Set(ref _dicIdiomasPesquisa, value); }
        }

        public Dictionary<Enum, string> DicMetodosProcessamento
        {
            get { return _dicMetodosProcessamento; }
            set { Set(ref _dicMetodosProcessamento, value); }
        }

        public Enums.IdiomaAplicacao EnuIdiomaAplicacaoSelecionado
        {
            get { return _enuIdiomaAplicacaoSelecionado; }
            set { Set(ref _enuIdiomaAplicacaoSelecionado, value); }
        }

        public Enums.MetodoDeProcessamento EnuMetodoDeProcessamentoSelecionado
        {
            get { return _enuMetodoDeProcessamentoSelecionado; }
            set { Set(ref _enuMetodoDeProcessamentoSelecionado, value); }
        }

        [Range(0, 999, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "Valor_fora_dos_limites_permitidos")]
        public int IntTempoAtualizarConteudoNovo
        {
            get { return _intTempoAtualizarConteudoNovo; }
            set { Set(ref _intTempoAtualizarConteudoNovo, value); }
        }

        public string StrIdiomaPesquisaSelecionado
        {
            get { return _strIdiomaPesquisaSelecionado; }
            set { Set(ref _strIdiomaPesquisaSelecionado, value); }
        }

        [MaxLength(260)]
        public string StrPastaAnime
        {
            get { return _strPastaAnime; }
            set { Set(ref _strPastaAnime, value); }
        }

        [MaxLength(260)]
        public string StrPastaFilme
        {
            get { return _strPastaFilme; }
            set { Set(ref _strPastaFilme, value); }
        }

        [MaxLength(260)]
        public string StrPastaSerie
        {
            get { return _strPastaSerie; }
            set { Set(ref _strPastaSerie, value); }
        }

        [MaxLength(260)]
        public string StrTorrentBlackhole
        {
            get { return _strTorrentBlackhole; }
            set { Set(ref _strTorrentBlackhole, value); }
        }

        /// <summary>
        ///     Idiomas do TheTVDB. Segundo eles isso é tão dificil de ser alterado que pode ser hardcoded. Para verificar o xml
        ///     com os idiomas a url é http://thetvdb.com/api/{APIKey}/languages.xml
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> GetIdiomas()
        {
            var idiomas = new Dictionary<string, string>
            {
                {"da", "Dansk"},
                {"fi", "Suomeksi"},
                {"nl", "Nederlands"},
                {"de", "Deutsch"},
                {"it", "Italiano"},
                {"es", "Español"},
                {"fr", "Français"},
                {"pl", "Polski"},
                {"hu", "Magyar"},
                {"el", "Ελληνικά"},
                {"tr", "Türkçe"},
                {"ru", "русский язык"},
                {"he", "עברית"},
                {"ja", "日本語"},
                {"pt", "Português"},
                {"zh", "中文"},
                {"cs", "čeština"},
                {"sl", "Slovenski"},
                {"hr", "Hrvatski"},
                {"ko", "한국어"},
                {"en", "English"},
                {"sv", "Svenska"},
                {"no", "Norsk"}
            };

            return idiomas;
        }
    }
}
