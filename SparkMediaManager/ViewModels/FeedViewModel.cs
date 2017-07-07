// Developed by: Gabriel Duarte
// 
// Created at: 14/05/2016 21:21

using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Autofac;
using GalaSoft.MvvmLight.CommandWpf;
using MahApps.Metro.Controls.Dialogs;
using SparkMediaManager.Helpers;
using SparkMediaManager.Localization;
using SparkMediaManager.Models;
using SparkMediaManager.Services;

namespace SparkMediaManager.ViewModels
{
    public class FeedViewModel : BaseViewModel
    {
        private bool _blnIsSelecionado;

        private Feed _objFeed;

        public FeedViewModel()
        {
            ConfigurarCommands();
        }

        public FeedViewModel(Feed objFeed)
        {
            ObjFeed = objFeed;
            ConfigurarCommands();
        }

        public bool BlnIsSelecionado { get { return _blnIsSelecionado; } set { Set(ref _blnIsSelecionado, value); } }

        public ICommand CommandAdicionarFeed { get; set; }

        public ICommand CommandAumentarPrioridade { get; set; }

        public ICommand CommandDiminuirPrioridade { get; set; }

        public ICommand CommandRemoverFeed { get; set; }

        public Feed ObjFeed { get { return _objFeed; } set { Set(ref _objFeed, value); } }

        private void ConfigurarCommands()
        {
            CommandAdicionarFeed = new RelayCommand<ListaFeedsViewModel>(AdicionarFeed);
            CommandAumentarPrioridade = new RelayCommand<ListaFeedsViewModel>(AumentarPrioridade, vm => vm.LstFeedsVm.Any(x => x.BlnIsSelecionado));
            CommandDiminuirPrioridade = new RelayCommand<ListaFeedsViewModel>(DiminuirPrioridade, vm => vm.LstFeedsVm.Any(x => x.BlnIsSelecionado));
            CommandRemoverFeed = new RelayCommand<ListaFeedsViewModel>(RemoverFeed, vm => vm.LstFeedsVm.Any(x => x.BlnIsSelecionado));
        }

        private async void RemoverFeed(ListaFeedsViewModel feedsVm)
        {
            if (await Helper.MostrarMensagem(Mensagem.Você_realmente_deseja_remover_os_feeds_selecionados_, Enums.TipoMensagem.QuestionamentoSimNao, Mensagem.Remover_feeds) != MessageDialogResult.Affirmative)
            {
                return;
            }

            var feedsService = App.Container.Resolve<FeedService>();

            List<Enums.TipoConteudo> lstTipoConteudoSelecionado = feedsVm.LstFeedsVm.Where(x => x.BlnIsSelecionado).Select(x => x.ObjFeed.EnuTipoConteudo).Distinct().ToList();

            feedsService.Remover(feedsVm.LstFeedsVm.Where(x => x.BlnIsSelecionado).Select(x => x.ObjFeed).ToArray());

            List<Feed> lstFeeds = feedsService.GetLista().Where(x => lstTipoConteudoSelecionado.Contains(x.EnuTipoConteudo) && !x.BlnIsFeedPesquisa).ToList();

            // Arrumar a prioridade
            for (var i = 0; i < lstFeeds.Count - 1; i++)
            {
                if (i == 0 && lstFeeds[i].IntPrioridade != 1)
                {
                    lstFeeds[i].IntPrioridade = 1;
                }

                if (lstFeeds[i].IntPrioridade + 1 != lstFeeds[i + 1].IntPrioridade)
                {
                    lstFeeds[i + 1].IntPrioridade = lstFeeds[i].IntPrioridade + 1;
                    feedsService.Update(lstFeeds[i + 1]);
                }
            }

            feedsVm.AtualizarListaFeeds();
        }

        private async void DiminuirPrioridade(ListaFeedsViewModel feedsVm)
        {
            var feedsService = App.Container.Resolve<FeedService>();

            List<Feed> lstFeedsSelecionados = feedsVm.LstFeedsVm.Where(x => x.BlnIsSelecionado).Select(x => x.ObjFeed).OrderByDescending(x => x.IntPrioridade).ToList();

            foreach (Feed item in lstFeedsSelecionados)
            {
                Feed oFeedAbaixo = feedsVm.LstFeedsVm.FirstOrDefault(x => x.ObjFeed.EnuTipoConteudo == item.EnuTipoConteudo && !x.BlnIsSelecionado && x.ObjFeed.IntPrioridade == item.IntPrioridade + 1)?.ObjFeed;

                if (oFeedAbaixo == null)
                {
                    continue;
                }

                item.IntPrioridade++;
                oFeedAbaixo.IntPrioridade--;

                if (!feedsService.Update(item, oFeedAbaixo))
                {
                    await Helper.MostrarMensagem(Mensagem.Ocorreu_um_erro_ao_alterar_a_prioridade_do_feed__0_ + item.StrNome, Enums.TipoMensagem.Erro);
                }
            }
        }

        private async void AumentarPrioridade(ListaFeedsViewModel feedsVm)
        {
            var feedsService = App.Container.Resolve<FeedService>();

            List<Feed> lstFeedsSelecionados = feedsVm.LstFeedsVm.Where(x => x.BlnIsSelecionado).Select(x => x.ObjFeed).OrderBy(x => x.IntPrioridade).ToList();

            foreach (Feed item in lstFeedsSelecionados)
            {
                Feed oFeedAcima = feedsVm.LstFeedsVm.FirstOrDefault(x => x.ObjFeed.EnuTipoConteudo == item.EnuTipoConteudo && !x.BlnIsSelecionado && x.ObjFeed.IntPrioridade == item.IntPrioridade - 1)?.ObjFeed;

                if (oFeedAcima == null)
                {
                    continue;
                }

                item.IntPrioridade--;
                oFeedAcima.IntPrioridade++;

                if (!feedsService.Update(item, oFeedAcima))
                {
                    await Helper.MostrarMensagem(string.Format(Mensagem.Ocorreu_um_erro_ao_alterar_a_prioridade_do_feed__0_, item.StrNome), Enums.TipoMensagem.Erro);
                }
            }
        }

        private void AdicionarFeed(ListaFeedsViewModel obj)
        {
            // TODO: Chamar tela de inclusão de feeds.
        }
    }
}
