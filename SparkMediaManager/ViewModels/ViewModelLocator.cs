// Developed by: Gabriel Duarte
// 
// Created at: 30/04/2016 16:03

using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace SparkMediaManager.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<PreferenciasViewModel>();
        }

        public MainViewModel MainViewModel => ServiceLocator.Current.GetInstance<MainViewModel>();

        public PreferenciasViewModel PreferenciasViewModel => ServiceLocator.Current.GetInstance<PreferenciasViewModel>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
