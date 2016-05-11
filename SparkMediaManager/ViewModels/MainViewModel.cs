// Developed by: Gabriel Duarte
// 
// Created at: 30/04/2016 16:03

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MahApps.Metro.SimpleChildWindow;
using SparkMediaManager.Models;
using SparkMediaManager.Views;

namespace SparkMediaManager.ViewModels
{
    /// <summary>
    ///     This class contains properties that the main View can data bind to.
    ///     <para>
    ///         Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    ///     </para>
    ///     <para>
    ///         You can also use Blend to data bind with the tool's support.
    ///     </para>
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<Serie> _lstAnimes;

        private ObservableCollection<Filme> _lstFilmes;

        private ObservableCollection<Serie> _lstSeries;

        public ICommand AbrirPreferenciasCommand { get; set; }
        public ICommand AbrirProcurarConteudoCommand { get; set; }
        public ICommand AbrirAdicionarCommand { get; set; }

        public Window ObjWindow { get; set; }

        public bool blnChildWindowAberta { get; set; }

        /// <summary>
        ///     Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                LstSeries = new ObservableCollection<Serie>
                {
                    new Serie
                    {
                        StrTitulo = "Game of Thrones",
                        BytCachePoster = File.ReadAllBytes(@"D:\Documentos\Projetos\SparkMediaManager\SparkMediaManager\DesignTime\Imagens\Poster_GOT.jpg")
                    },
                    new Serie
                    {
                        StrTitulo = "Game of Thrones 2",
                        BytCachePoster = File.ReadAllBytes(@"D:\Documentos\Projetos\SparkMediaManager\SparkMediaManager\DesignTime\Imagens\Poster_GOT.jpg")
                    },
                    new Serie
                    {
                        StrTitulo = "Game of Thrones 3",
                        BytCachePoster = File.ReadAllBytes(@"D:\Documentos\Projetos\SparkMediaManager\SparkMediaManager\DesignTime\Imagens\Poster_GOT.jpg")
                    },
                    new Serie
                    {
                        StrTitulo = "Game of Thrones 4",
                        BytCachePoster = File.ReadAllBytes(@"D:\Documentos\Projetos\SparkMediaManager\SparkMediaManager\DesignTime\Imagens\Poster_GOT.jpg")
                    },
                    new Serie
                    {
                        StrTitulo = "Game of Thrones 5",
                        BytCachePoster = File.ReadAllBytes(@"D:\Documentos\Projetos\SparkMediaManager\SparkMediaManager\DesignTime\Imagens\Poster_GOT.jpg")
                    },
                    new Serie
                    {
                        StrTitulo = "Game of Thrones 6",
                        BytCachePoster = File.ReadAllBytes(@"D:\Documentos\Projetos\SparkMediaManager\SparkMediaManager\DesignTime\Imagens\Poster_GOT.jpg")
                    },
                    new Serie
                    {
                        StrTitulo = "Game of Thrones 7",
                        BytCachePoster = File.ReadAllBytes(@"D:\Documentos\Projetos\SparkMediaManager\SparkMediaManager\DesignTime\Imagens\Poster_GOT.jpg")
                    },
                    new Serie
                    {
                        StrTitulo = "Game of Thrones 8",
                        BytCachePoster = File.ReadAllBytes(@"D:\Documentos\Projetos\SparkMediaManager\SparkMediaManager\DesignTime\Imagens\Poster_GOT.jpg")
                    },
                    new Serie
                    {
                        StrTitulo = "Game of Thrones 9",
                        BytCachePoster = File.ReadAllBytes(@"D:\Documentos\Projetos\SparkMediaManager\SparkMediaManager\DesignTime\Imagens\Poster_GOT.jpg")
                    },
                    new Serie
                    {
                        StrTitulo = "Game of Thrones 10",
                        BytCachePoster = File.ReadAllBytes(@"D:\Documentos\Projetos\SparkMediaManager\SparkMediaManager\DesignTime\Imagens\Poster_GOT.jpg")
                    }
                };
            }
            else
            {
                AbrirPreferenciasCommand = new RelayCommand(AbrirPreferencias, CanAbrirJanela);
                AbrirProcurarConteudoCommand = new RelayCommand(AbrirProcurarConteudo, CanAbrirJanela);
                AbrirAdicionarCommand = new RelayCommand(AbrirAdicionar, CanAbrirJanela);
            }
        }

        private void AbrirProcurarConteudo()
        {
        }

        private void AbrirAdicionar()
        {
        }

        private bool CanAbrirJanela()
        {
            return !blnChildWindowAberta;
        }

        private async void AbrirPreferencias()
        {
            await ObjWindow.ShowChildWindowAsync(new PreferenciasWindow(), (Panel)ObjWindow.Content);
        }

        public ObservableCollection<Serie> LstSeries
        {
            get { return _lstSeries; }
            set { Set(ref _lstSeries, value); }
        }

        public ObservableCollection<Filme> LstFilmes
        {
            get { return _lstFilmes; }
            set { Set(ref _lstFilmes, value); }
        }

        public ObservableCollection<Serie> LstAnimes
        {
            get { return _lstAnimes; }
            set { Set(ref _lstAnimes, value); }
        }
    }
}
