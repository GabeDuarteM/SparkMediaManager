using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Ioc;
using MahApps.Metro.Controls;
using MahApps.Metro.SimpleChildWindow;
using SparkMediaManager.Helpers;
using SparkMediaManager.ViewModels;

namespace SparkMediaManager.Views
{
    /// <summary>
    /// Interaction logic for Preferencias.xaml
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
