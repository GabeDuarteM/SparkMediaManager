// Developed by: Gabriel Duarte
// 
// Created at: 08/05/2016 18:24

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Autofac;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using SparkMediaManager.Properties;
using SparkMediaManager.ViewModels;
using Label = SparkMediaManager.Localization.Label;

namespace SparkMediaManager.Helpers
{
    public static class Helper
    {
        public static DateTime UnixTimeStampParaDateTime(int unixTimeStamp)
        {
            var dtmReturn = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtmReturn = dtmReturn.AddSeconds(unixTimeStamp).ToLocalTime();

            return dtmReturn;
        }

        public static IEnumerable<object> GetElementosVisuais(DependencyObject depObj)
        {
            if (depObj != null)
            {
                for (var i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null)
                    {
                        yield return child;
                    }

                    foreach (object childOfChild in GetElementosVisuais(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public static void AplicarVisualAtributosDataContext(Window objWindow)
        {
            foreach (TextBox oTextBox in objWindow.FindChildren<TextBox>())
            {
                AplicarVisualTextBox(oTextBox, objWindow.DataContext);
            }
        }

        private static void AplicarVisualTextBox(TextBox objTextBox, object objDataContext)
        {
            string strPropriedade = objTextBox.GetBindingExpression(TextBox.TextProperty)?.ParentBinding.Path.Path;

            if (string.IsNullOrWhiteSpace(strPropriedade))
            {
                return;
            }

            IEnumerable<CustomAttributeData> lstAtributos = objDataContext.GetType().GetProperties().First(x => x.Name == strPropriedade).CustomAttributes;

            foreach (CustomAttributeData objAtributo in lstAtributos)
            {
                switch (objAtributo.AttributeType.Name)
                {
                    case nameof(RequiredAttribute):
                        objTextBox.BorderThickness = new Thickness(3, 0, 0, 0);
                        objTextBox.BorderBrush = Brushes.Crimson;
                        break;
                    case nameof(MaxLengthAttribute):
                        objTextBox.MaxLength = (int) objAtributo.ConstructorArguments.First(x => x.ArgumentType == typeof(int)).Value;
                        break;
                }
            }
        }

        public static async Task<MessageDialogResult> MostrarMensagem(string mensagem, Enums.TipoMensagem enuTipoMensagem, string titulo = null)
        {
            var objWindow = (MetroWindow) App.Container.Resolve<MainViewModel>().ObjWindow;
            var objResource = new ResourceDictionary();

            foreach (ResourceDictionary resourceDictionary in Application.Current.Resources.MergedDictionaries)
            {
                objResource.MergedDictionaries.Add(resourceDictionary);
            }

            var objConfig = new MetroDialogSettings()
            {
                AffirmativeButtonText = Label.Sim,
                AnimateShow = true,
                CustomResourceDictionary = objResource,
                NegativeButtonText = Label.Nao,
                FirstAuxiliaryButtonText = Label.Cancelar
            };

            if (!string.IsNullOrWhiteSpace(titulo))
            {
                titulo = Settings.Default.AppName + " - " + titulo;
            }

            switch (enuTipoMensagem)
            {
                case Enums.TipoMensagem.Erro:
                case Enums.TipoMensagem.Informativa:
                case Enums.TipoMensagem.Alerta:
                    objConfig.AffirmativeButtonText = Label.Ok;
                    return await objWindow.ShowMessageAsync(titulo ?? Settings.Default.AppName, mensagem, settings: objConfig);
                case Enums.TipoMensagem.QuestionamentoSimNao:
                case Enums.TipoMensagem.AlertaSimNao:
                    return await objWindow.ShowMessageAsync(titulo ?? Settings.Default.AppName, mensagem, MessageDialogStyle.AffirmativeAndNegative, objConfig);
                case Enums.TipoMensagem.QuestionamentoSimNaoCancela:
                case Enums.TipoMensagem.AlertaSimNaoCancela:
                    return await objWindow.ShowMessageAsync(titulo ?? Settings.Default.AppName, mensagem, MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary, objConfig);
                default:
                    throw new ArgumentOutOfRangeException(nameof(enuTipoMensagem), enuTipoMensagem, null);
            }
        }
    }
}
