// Developed by: Gabriel Duarte
// 
// Created at: 10/05/2016 00:02

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Autofac;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.SimpleChildWindow;
using SparkMediaManager.Helpers;
using SparkMediaManager.Models;
using SparkMediaManager.ViewModels;

namespace SparkMediaManager.Views
{
    /// <summary>
    ///     Interaction logic for Preferencias.xaml
    /// </summary>
    public partial class PreferenciasWindow
    {
        public PreferenciasWindow()
        {
            InitializeComponent();
            App.Container.Resolve<MainViewModel>().blnChildWindowAberta = true;
        }

        private void PreferenciasWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            this.AplicarVisualAtributosDataContext();
        }

        private void PreferenciasWindow_OnClosingFinished(object sender, RoutedEventArgs e)
        {
            App.Container.Resolve<MainViewModel>().blnChildWindowAberta = false;
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ((PreferenciasViewModel)DataContext).ObjListaFeedsViewModel = new ListaFeedsViewModel
            {
                LstFeedsVm = new List<FeedViewModel>
                {
                    new FeedViewModel(new Feed()
                    {
                        StrNome = "feed", IntCodigo = 1, IntPrioridade = 0, BlnIsFeedPesquisa = false, EnuTipoConteudo = Enums.TipoConteudo.Anime, StrLink = "http://www.bing.com", StrTagPesquisa = "tem?"
                    })
                }
            };
            //foreach (KeyValuePair<Enum, string> keyValuePair in Enums.GetListaValores(typeof(Enums.TipoMensagem)))
            //{
            //    if (Equals(keyValuePair.Key, Enums.TipoMensagem.Selecione))
            //    {
            //        continue;
            //    }
            //    await Helper.MostrarMensagem("Teste " + keyValuePair.Value, (Enums.TipoMensagem) keyValuePair.Key);
            //}
        }
    }
}
