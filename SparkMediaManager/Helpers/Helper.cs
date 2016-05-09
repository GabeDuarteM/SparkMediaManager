// Developed by: Gabriel Duarte
// 
// Created at: 08/05/2016 18:24

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MahApps.Metro.Controls;

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
    }
}
