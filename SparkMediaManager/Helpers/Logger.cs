// Developed by: Gabriel Duarte
// 
// Created at: 14/05/2016 17:18

using System;
using System.IO;
using System.Linq;
using System.Text;
using SparkMediaManager.Localization;
using SparkMediaManager.Properties;

namespace SparkMediaManager.Helpers
{
    public class Logger : ILogger
    {
        #region Implementation of ILogger

        public bool LogMessage(string message, Enums.TipoMensagem enuTipoMensagemLog)
        {
            string strLogPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), Settings.Default.AppName + ".log");
            string strData = $"## {DateTime.Now.ToString("G")} ## {enuTipoMensagemLog.GetDescricao()}";
            string strEspacamento = "## " + new string(' ', strData.Length - 3);
            string logLine;

            message = message.Trim();

            if (message.LastOrDefault() != '.')
            {
                message += '.';
            }
            if (!File.Exists(strLogPath))
            {
                logLine = $@"####################################################################################################
{new string('#', 49 - Settings.Default.AppName.Length)} {Settings.Default.AppName} {new string('#', 49 - Settings.Default.AppName.Length)}
####################################################################################################
##
{strData}{message.Replace(Environment.NewLine, Environment.NewLine + strEspacamento)}";
            }
            else
            {
                logLine = "## " + Environment.NewLine + strData + message.Replace(Environment.NewLine, Environment.NewLine + strEspacamento);
            }

            try
            {
                using (var sw = new StreamWriter(new FileStream(strLogPath, FileMode.Append, FileAccess.Write), Encoding.UTF8))
                {
                    sw.WriteLine(logLine);
                }
                return true;
            }
            catch (Exception e)
            {
                //new SparkException(e).TratarException(Mensagem.Ocorreu_um_erro_ao_registrar_a_mensagem_no_log, Enums.TipoMensagem.Erro, false);
                return false;
            }
        }

        #endregion
    }
}
