// Developed by: Gabriel Duarte
// 
// Created at: 14/05/2016 21:26

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using Autofac;
using GalaSoft.MvvmLight.CommandWpf;
using SparkMediaManager.Models;
using SparkMediaManager.Services;

namespace SparkMediaManager.ViewModels
{
    public class ListaFeedsViewModel : BaseViewModel
    {
        private bool? _blnIsSelecionarTodos;

        private CollectionViewSource _cvsFeedsVm;

        private List<FeedViewModel> _lstFeedsVm;

        public ListaFeedsViewModel()
        {
            CommandSelecionar = new RelayCommand<ListaFeedsViewModel>(Selecionar);
            CommandSelecionarTodos = new RelayCommand<ListaFeedsViewModel>(SelecionarTodos);
        }

        private void SelecionarTodos(ListaFeedsViewModel feedsVm)
        {
            if (feedsVm.BlnIsSelecionarTodos == true)
            {
                foreach (FeedViewModel feed in feedsVm.LstFeedsVm)
                {
                    feed.BlnIsSelecionado = true;
                }
            }
            else
            {
                feedsVm.BlnIsSelecionarTodos = false;
                foreach (FeedViewModel feed in feedsVm.LstFeedsVm)
                {
                    feed.BlnIsSelecionado = false;
                }
            }
        }

        public bool? BlnIsSelecionarTodos { get { return _blnIsSelecionarTodos; } set { Set(ref _blnIsSelecionarTodos, value); } }

        public ICommand CommandSelecionar { get; set; }

        public ICommand CommandSelecionarTodos { get; set; }

        public CollectionViewSource CvsFeedsVm { get { return _cvsFeedsVm; } set { Set(ref _cvsFeedsVm, value); } }

        public List<FeedViewModel> LstFeedsVm { get { return _lstFeedsVm; } set { Set(ref _lstFeedsVm, value); } }

        private void Selecionar(ListaFeedsViewModel feedsVm)
        {
            int feedsSelecionadosCount = feedsVm.LstFeedsVm.Count(x => x.BlnIsSelecionado);

            if (feedsSelecionadosCount == feedsVm.LstFeedsVm.Count && feedsVm.LstFeedsVm.Count > 0)
            {
                feedsVm.BlnIsSelecionarTodos = true;
            }
            else if (feedsSelecionadosCount == 0)
            {
                feedsVm.BlnIsSelecionarTodos = false;
            }
            else if (feedsSelecionadosCount > 0)
            {
                feedsVm.BlnIsSelecionarTodos = null;
            }
        }

        public void AtualizarListaFeeds(List<FeedViewModel> lstFeeds = null)
        {
            var feedsService = App.Container.Resolve<FeedService>();

            LstFeedsVm = lstFeeds ?? feedsService.GetLista().Where(x => !x.BlnIsFeedPesquisa).Select(x => new FeedViewModel(x)).ToList();
            CvsFeedsVm = new CollectionViewSource {Source = LstFeedsVm};
            CvsFeedsVm.SortDescriptions.Add(new SortDescription(nameof(FeedViewModel.ObjFeed) + '.' + nameof(Feed.IntPrioridade), ListSortDirection.Ascending));
            CvsFeedsVm.IsLiveSortingRequested = true;
            CvsFeedsVm.GroupDescriptions.Add(new PropertyGroupDescription(nameof(FeedViewModel.ObjFeed) + '.' + nameof(Feed.IntPrioridade)));
            CommandSelecionar.Execute(this);
        }
    }
}
