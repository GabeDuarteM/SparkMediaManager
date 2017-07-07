using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using SparkMediaManager.Localization;

namespace SparkMediaManager.Helpers
{
    public class SparkException
    {
        public SparkException(Exception e)
        {
            Exception = e;
        }

        public SparkException(string e)
        {
            Exception = new Exception(e);
        }

        public Exception Exception { get; set; }

        public void TratarException(string strErro, Enums.TipoMensagem enuTipoMensagem)
        {
            var sDetalhes = "";

            if (strErro.Last() != '.')
            {
                strErro += '.';
            }

            sDetalhes += $"Detalhes: {Exception}";

            string sMensagem = strErro + Environment.NewLine + sDetalhes;

            SimpleIoc.Default.GetInstance<ILogger>().LogMessage(sMensagem, enuTipoMensagem);

            //if (!bIsSilencioso)
            //{
            //    Helper.MostrarMensagem(strErro + Environment.NewLine + Environment.NewLine +
            //                           Mensagem.Verifique_mais_detalhes_no_log,
            //                           enuTipoMensagem);
            //}
        }
    }
}
