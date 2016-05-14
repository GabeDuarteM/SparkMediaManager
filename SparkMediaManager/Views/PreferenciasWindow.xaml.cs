// Developed by: Gabriel Duarte
// 
// Created at: 10/05/2016 00:02

using System.Windows;
using GalaSoft.MvvmLight.Ioc;
using SparkMediaManager.Helpers;
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
            SimpleIoc.Default.GetInstance<MainViewModel>().blnChildWindowAberta = true;
        }

        private void PreferenciasWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            this.AplicarVisualAtributosDataContext();
        }

        private void PreferenciasWindow_OnClosingFinished(object sender, RoutedEventArgs e)
        {
            SimpleIoc.Default.GetInstance<MainViewModel>().blnChildWindowAberta = false;
        }
    }
}
