// Developed by: Gabriel Duarte
// 
// Created at: 14/05/2016 14:33

using System.ComponentModel.DataAnnotations;
using SparkMediaManager.Atributos;
using SparkMediaManager.Helpers;
using SparkMediaManager.ViewModels;

namespace SparkMediaManager.Models
{
    public class Feed : BaseModel
    {
        private bool _blnIsFeedPesquisa;

        private Enums.TipoConteudo _enuTipoConteudo;

        private int _intPrioridade;

        private string _strLink;

        private string _strNome;

        private string _strTagPesquisa;

        public bool BlnIsFeedPesquisa { get { return _blnIsFeedPesquisa; } set { Set(ref _blnIsFeedPesquisa, value); } }

        [Required]
        public Enums.TipoConteudo EnuTipoConteudo { get { return _enuTipoConteudo; } set { Set(ref _enuTipoConteudo, value); } }

        [Key]
        public int IntCodigo { get; set; }

        [Required]
        public int IntPrioridade { get { return _intPrioridade; } set { Set(ref _intPrioridade, value); } }

        [Required, MaxLength(1000)]
        public string StrLink { get { return _strLink; } set { Set(ref _strLink, value); } }

        [Required, MaxLength(150)]
        public string StrNome { get { return _strNome; } set { Set(ref _strNome, value); } }

        [RequiredSoNaTela]
        public string StrTagPesquisa { get { return _strTagPesquisa; } set { Set(ref _strTagPesquisa, value); } }
    }
}
