using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkMediaManager.Helpers
{
    public interface ILogger
    {
        bool LogMessage(string message, Enums.TipoMensagem enuTipoMensagemLog);
    }
}
