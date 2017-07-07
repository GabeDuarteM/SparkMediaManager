using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SparkMediaManager.Localization;
using SparkMediaManager.Models;

namespace SparkMediaManager.Services
{
    public class FeedService : RepositorioBase<Feed>
    {
        public FeedService(IContext ctx) : base(ctx, new []{nameof(Feed.StrNome), nameof(Feed.IntCodigo)},
            Mensagem.Ocorreu_um_erro_ao_adicionar_o_feed_0_,
            Mensagem.Ocorreu_um_erro_ao_pesquisar_o_feed_de_código_1_,
            Mensagem.Ocorreu_um_erro_ao_retornar_a_lista_de_feeds,
            Mensagem.Ocorreu_um_erro_ao_remover_o_feed_0_,
            Mensagem.Ocorreu_um_erro_ao_atualizar_o_feed_0_)
        {
            
        }
    }
}
