// Developed by: Gabriel Duarte
// 
// Created at: 29/04/2016 23:49

using System.Windows;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using Microsoft.Practices.ServiceLocation;
using SparkMediaManager.Helpers;
using SparkMediaManager.Models;
using SparkMediaManager.Services;
using SparkMediaManager.ViewModels;

namespace SparkMediaManager
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IContainer Container;

        public App()
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            //FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(
            //            XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        }

        #region Overrides of Application

        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Context>().As<IContext>();
            builder.RegisterType<MainViewModel>().SingleInstance();
            builder.RegisterType<PreferenciasViewModel>().SingleInstance();
            builder.RegisterType<Logger>().As<ILogger>();
            builder.RegisterType<FeedService>();

            Container = builder.Build();

            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(Container));

            base.OnStartup(e);
        }

        #endregion
    }
}
